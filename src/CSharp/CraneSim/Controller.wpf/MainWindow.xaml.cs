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
using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;

namespace Controller.wpf
{
    public partial class MainWindow : Window
    {

        private bool _isStop = false;
        public MainWindow()
        {
            InitializeComponent();
            InitConnection();
        }
        private async void InitConnection()
        {
            HiveMQClientOptions options = new HiveMQClientOptions();
            options.Host = "c0bbe3829ad14fe3b24e5c51247f57c1.s2.eu.hivemq.cloud";
            options.Port = 8883;
            options.UseTLS = true;

            options.UserName = "cranemqtt";
            options.Password = "7va@tWTv2.Jw2yk";

            HiveMQClient client = new HiveMQClient(options);

            var connectResult = await client.ConnectAsync().ConfigureAwait(false);
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
            if (!_isStop)
            {
                if (key == Key.W)
                {
                    infoLabel.Content = "Trolley forward!";
                    SendTrolleyForward();
                }
                if (key == Key.S)
                {
                    infoLabel.Content = "Trolley backwards!";
                    SendTrolleyBackward();
                }

                if (key == Key.A)
                {
                    infoLabel.Content = "Gantry left!";
                    SendGantryLeft();
                }
                if (key == Key.D)
                {
                    infoLabel.Content = "Gantry right!";
                    SendGantryRight();
                }

                if (key == Key.Up)
                {
                    infoLabel.Content = "Hoist up!";
                    SendHoistUp();
                }
                if (key == Key.Down)
                {
                    infoLabel.Content = "Hoist down!";
                    SendHoistDown();
                }


                if (key == Key.Q)
                {
                    infoLabel.Content = "Boom Up!";
                    SendBoomUp();
                }
                if (key == Key.E)
                {
                    infoLabel.Content = "Boom Down!";
                    SendBoomDown();
                }


                if (key == Key.O)
                {
                    infoLabel.Content = "Noodstop has been pressed!!!!\nPress P to reset";
                    SendNoodstop();
                    _isStop = true;
                }
            }
            else
            {
                if (key == Key.P)
                {
                    infoLabel.Content = "Undid the Noodstop!";
                    SendUndoNoodstop();
                    _isStop = false;
                }
            }

        }
        private void HandleKeyRelease(Key key)
        {
            if (!_isStop)
            {
                if (key == Key.W)
                {
                    infoLabel.Content = "Trolley stopped!";
                    SendTrolleyForwardStop();
                }
                if (key == Key.S)
                {
                    infoLabel.Content = "Trolley stopped!";
                    SendTrolleyBackwardStop();
                }

                if (key == Key.A)
                {
                    infoLabel.Content = "Gantry stopped!";
                    SendGantryLeftStop();
                }
                if (key == Key.D)
                {
                    infoLabel.Content = "Gantry stopped!";
                    SendGantryRightStop();
                }

                if (key == Key.Up)
                {
                    infoLabel.Content = "Hoist stopped!";
                    SendHoistUpStop();
                }
                if (key == Key.Down)
                {
                    infoLabel.Content = "Hoist stopped!";
                    SendHoistDownStop();
                }

                if (key == Key.Q)
                {
                    infoLabel.Content = "Boom stopped!";
                    SendBoomUpStop();
                }
                if (key == Key.E)
                {
                    infoLabel.Content = "Boom stopped!";
                    SendBoomDownStop();
                }
            }
        }
        #region press
        private void SendTrolleyForward()
        {

        }
        private void SendTrolleyBackward()
        {

        }
        private void SendGantryLeft()
        {

        }
        private void SendGantryRight()
        {

        }
        private void SendHoistUp()
        {

        }
        private void SendHoistDown()
        {

        }
        private void SendBoomUp()
        {

        }
        private void SendBoomDown()
        {

        }
        private void SendNoodstop()
        {

        }
        private void SendUndoNoodstop()
        {

        }
        #endregion

        #region release
        private void SendTrolleyForwardStop()
        {

        }
        private void SendTrolleyBackwardStop()
        {

        }
        private void SendGantryLeftStop()
        {

        }
        private void SendGantryRightStop()
        {

        }
        private void SendHoistUpStop()
        {

        }
        private void SendHoistDownStop()
        {

        }
        private void SendBoomUpStop()
        {

        }
        private void SendBoomDownStop()
        {

        }
        #endregion
    }
}
