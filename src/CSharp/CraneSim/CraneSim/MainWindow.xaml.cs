using CraneSim.Core.Entities;
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


        public MainWindow()
        {
            InitializeComponent();
            CreateComopnents();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _trolley.IsActive = true;
        }

        private void CreateComopnents()
        {
            _trolley = new Trolley() 
            { 
                Id = 1
            };
            
        }
    }
}
