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
        Gantry _gantry;
        IGantryService _gantryService;


        public MainWindow()
        {
            InitializeComponent();
            CreateComopnents();
            CreateServices();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _trolley.IsActive = true;
            _gantry.IsActive = true;
            //TestTrolleyService(); < om test te runnen
        }

        private void CreateComopnents()
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
            _trolleyService = new TrolleyService();
            _gantryService = new GantryService();
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
