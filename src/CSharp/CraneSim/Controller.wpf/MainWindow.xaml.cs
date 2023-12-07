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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKeyRelease(e.Key);
        }
        private void HandleKeyPress(Key key)
        {
            if (key == Key.W)
            {
                infoLabel.Content = "Trolley forward!";
            }
            if (key == Key.S)
            {
                infoLabel.Content = "Trolley backwards!";
            }

            if (key == Key.A) 
            {
                infoLabel.Content = "Gantry left!";
            }
            if (key == Key.D)
            {
                infoLabel.Content = "Gantry right!";
            }

            if (key == Key.Up)
            {
                infoLabel.Content = "Hoist up!";
            }
            if (key == Key.Down)
            {
                infoLabel.Content = "Hoist down!";
            }


            if (key == Key.Q)
            {
                infoLabel.Content = "Boom Up!";
            }
            if (key == Key.E)
            {
                infoLabel.Content = "Boom Down!";
            }
        }
        private void HandleKeyRelease(Key key)
        {
            if (key == Key.W)
            {
                infoLabel.Content = "Trolley stopped!";
            }
            if (key == Key.S)
            {
                infoLabel.Content = "Trolley stopped!";
            }

            if (key == Key.A)
            {
                infoLabel.Content = "Gantry stopped!";
            }
            if (key == Key.D)
            {
                infoLabel.Content = "Gantry stopped!";
            }

            if (key == Key.Up)
            {
                infoLabel.Content = "Hoist stopped!";
            }
            if (key == Key.Down)
            {
                infoLabel.Content = "Hoist stopped!";
            }

            if (key == Key.Q)
            {
                infoLabel.Content = "Boom stopped!";
            }
            if (key == Key.E)
            {
                infoLabel.Content = "Boom stopped!";
            }
        }
    }
}
