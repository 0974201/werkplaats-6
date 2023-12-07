using CraneSim.Core.Entities;
using CraneSim.Core.Interfaces;
using CraneSim.Core.Services;
using CraneSim.Dtos.Trolley;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CraneSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Trolley _trolley;
        ITrolleyService _trolleyService;


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

        #region Testmethodes
        private async Task TestTrolleyService() 
        {
            TestTrolleyServiceTimer();
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

            long result = await _trolleyService.ReturnStopwatchvalue();

            //MessageBox.Show($"{result}");
        }

        private async void TestTrolleySpeed()
        {
            var speedBefore = _trolley.Speed;
            await _trolleyService.CalculateCurrentSpeed(_trolley);
            var speedAfter = _trolley.Speed;

            //MessageBox.Show($"startspeed: {speedBefore}, currentspeed{speedAfter}");
        }

        private async void TestTrolleyHorizontalePositiefMovement()
        {
            var oldPositionX = _trolley.PositionX;
            await _trolleyService.CalculateHorizontalePositiefMovement(_trolley);
            var newPositionX = _trolley.PositionX;

            MessageBox.Show($"startposition: {oldPositionX}, currentPosition{newPositionX}");
        }

        private async void TestTrolleyHorizontaleNegatiefMovement()
        {
            var oldPositionX = _trolley.PositionX;
            await _trolleyService.CalculateHorizontaleNegatiefMovement(_trolley);
            var newPositionX = _trolley.PositionX;

            MessageBox.Show($"startposition: {oldPositionX}, currentPosition{newPositionX}");
        }

        #endregion
    }
}
