using System;
using System.Collections.Generic;
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

namespace Controller.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is 'W'
            if (e.Key == Key.W)
            {
                // Update the label text or perform any action
                infoLabel.Content = "W key pressed!";
            }
            if (e.Key == Key.S)
            {
                // Update the label text or perform any action
                infoLabel.Content = "S key pressed!";
            }
            if (e.Key == Key.A) 
            {
                // Update the label text or perform any action
                infoLabel.Content = "A key pressed!";
            }
            if (e.Key == Key.D)
            {
                // Update the label text or perform any action
                infoLabel.Content = "D key pressed!";
            }
        }
    }
}
