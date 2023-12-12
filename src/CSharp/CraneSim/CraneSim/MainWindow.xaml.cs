using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using CraneSim.Core.Services;
using CraneSim.Infrastructure.MicroServices;
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
        ITrolleyMicroSevice _trolleyMicroSevice;


        public MainWindow()
        {
            InitializeComponent();
            CreateComopnents();
            CreateServices();
            CreateMicroServices();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _trolley.IsActive = true;
            //TestTrolleyService();
            _trolleyMicroSevice.EstablishBrokerConnection();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateComopnents()
        {
            _trolley = new Trolley() 
            { 
                Id = 1,
                Name = "Trolley",
            };
            
        }

        private void CreateServices()
        {
            _trolleyService = new TrolleyService();
        }

        private void CreateMicroServices()
        {
            _trolleyMicroSevice = new TrolleyMicroService(_trolley);
        }

        #region Testmethodes
        private async Task TestTrolleyService() 
        {
            TestTrolleyServiceTimer();
            _trolleyService.CalculateConstantAccelaration(_trolley);
            TestTrolleySpeed();
            TestTrolleyHorizontalePositiefMovement();
            await Task.Delay(500);
            TestTrolleyHorizontaleNegatiefMovement();
        } 

        private async void TestTrolleyServiceTimer()
        {
            _trolleyService.StartStopwatch();
            await Task.Delay(500); // wait for 1 second
            _trolleyService.StopStopwatch();

            double result = _trolleyService.ReturnStopwatchvalue();

            //MessageBox.Show($"{result}");
        }

        private void TestTrolleySpeed()
        {
            var speedBefore = _trolley.Speed;
            _trolleyService.CalculateCurrentSpeed(_trolley);
            var speedAfter = _trolley.Speed;

            //MessageBox.Show($"startspeed: {speedBefore}, currentspeed{speedAfter}");
        }

        private void TestTrolleyHorizontalePositiefMovement()
        {
            var oldPositionX = _trolley.PositionX;
            _trolleyService.CalculateHorizontalPositiveMovement(_trolley);
            var newPositionX = _trolley.PositionX;

            MessageBox.Show($"startposition: {oldPositionX}, currentPosition{newPositionX}");
        }

        private void TestTrolleyHorizontaleNegatiefMovement()
        {
            var oldPositionX = _trolley.PositionX;
            _trolleyService.CalculateHorizontalNegativeMovement(_trolley);
            var newPositionX = _trolley.PositionX;

            MessageBox.Show($"startposition: {oldPositionX}, currentPosition{newPositionX}");
        }

        #endregion

        
    }
}
