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
        Shipcontainer _shipcontainer;
        ITrolleyService _trolleyService;
        IShipContainerService _shipContainerService;

        public MainWindow()
        {
            InitializeComponent();
            CreateComopnents();
            CreateServices();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _trolley.IsActive = true;
            //TestTrolleyService();
            _trolleyService.EstablishBrokerConnection();
            _shipContainerService.EstablishBrokerConnection();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _trolleyService.DisconnectBrokerConnection();
            _shipContainerService.DisconnectBrokerConnection();
        }

        private void CreateComopnents()
        {
            _trolley = new Trolley() 
            { 
                Id = 1,
                Name = "Trolley",
            };

            _shipcontainer = new Shipcontainer()
            {
                Id = 1,
                Name = "ShipContainer"
            };
            
        }

        private void CreateServices()
        {
            _trolleyService = new TrolleyService(_trolley);
            _shipContainerService = new ShipContainerServices(_shipcontainer);
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

        #endregion

        
    }
}
