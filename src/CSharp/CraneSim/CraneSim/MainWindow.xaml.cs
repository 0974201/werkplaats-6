using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using CraneSim.Core.Services;
using System.Threading.Tasks;
using System.Windows;

namespace CraneSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Trolley _trolley;
        ITrolleyService _trolleyService;

        Gantry _gantry;
        IGantryService _gantryService;

        public MainWindow()
        {
            InitializeComponent();
            CreateComponents();
            CreateServices();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _trolley.IsActive = true;
            _gantry.IsActive = true;
            //TestTrolleyService();
            //TestGantryService(); < om test te runnen
            _trolleyService.EstablishBrokerConnection();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _trolleyService.DisconnectBrokerConnection();
        }

        private void CreateComponents()
        {
            _trolley = new Trolley()
            {
                Id = 1,
                Name = "Trolley",
            };

            _gantry = new Gantry()
            {
                Id = 1,
                Name = "Gantry",
            };

        }

        private void CreateServices()
        {
            _trolleyService = new TrolleyService(_trolley);
            _gantryService = new GantryService(_gantry);
        }


        #region Testmethodes
        private async Task TestTrolleyService() 
        {
            TestTrolleyServiceTimer();
            _trolleyService.CalculateConstantAccelaration();
            TestTrolleySpeed();
            TestTrolleyHorizontalePositiefMovement();
            await Task.Delay(500);
            TestTrolleyHorizontaleNegatiefMovement();
        } 

        private async void TestTrolleyServiceTimer()
        {
            _trolleyService.StartStopwatch();
            await Task.Delay(500); // wait for 0.5 second
            _trolleyService.StopStopwatch();

            double result = _trolleyService.ReturnStopwatchvalue();

            //MessageBox.Show($"{result}");
        }

        private void TestTrolleySpeed()
        {
            var speedBefore = _trolley.Speed;
            _trolleyService.CalculateCurrentSpeed();
            var speedAfter = _trolley.Speed;

            //MessageBox.Show($"startspeed: {speedBefore}, currentspeed{speedAfter}");
        }

        private void TestTrolleyHorizontalePositiefMovement()
        {
            var oldPositionX = _trolley.PositionX;
            _trolleyService.CalculateHorizontalPositiveMovement();
            var newPositionX = _trolley.PositionX;

            MessageBox.Show($"startposition: {oldPositionX}, currentPosition{newPositionX}");
        }

        private void TestTrolleyHorizontaleNegatiefMovement()
        {
            var oldPositionX = _trolley.PositionX;
            _trolleyService.CalculateHorizontalNegativeMovement();
            var newPositionX = _trolley.PositionX;

            MessageBox.Show($"startposition: {oldPositionX}, currentPosition{newPositionX}");
        }

        private async Task TestGantryService()
        {
            TestGantryServiceTimer();
            _gantryService.CalculateAcceleration(_gantry);
            TestGantrySpeed();
            TestGantryPosMovement();
            await Task.Delay(500);
            TestGantryNegMovement();
        }

        private async void TestGantryServiceTimer()
        {
            _gantryService.StartStopwatch();
            await Task.Delay(500);
            _gantryService.StopStopwatch();

            double result = _gantryService.ReturnStopwatchvalue();

            MessageBox.Show($"{result}");
        }

        private void TestGantrySpeed()
        {
            var speedBefore = _gantry.Speed;
            _gantryService.CalculateCurrentSpeed(_gantry);
            var speedAfter = _gantry.Speed;

            MessageBox.Show($"startspeed: {speedBefore}, currentspeed{speedAfter}");
        }

        private void TestGantryPosMovement()
        {
            var oldPos = _gantry.PositionZ;
            _gantryService.CalculatePositiveMovement(_gantry);
            var newPos = _gantry.PositionZ;

            MessageBox.Show($"startposition: {oldPos}, currentPosition{newPos}");
        }

        private void TestGantryNegMovement()
        {
            var oldPos = _gantry.PositionZ;
            _gantryService.CalculateNegativeMovement(_gantry);
            var newPos = _gantry.PositionZ;

            MessageBox.Show($"startposition: {oldPos}, currentPosition{newPos}");
        }

        #endregion
    }
}