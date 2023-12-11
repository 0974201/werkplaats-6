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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;
using NLog.Targets;

namespace Controller.wpf
{
    public partial class MainWindow : Window
    {

        private bool _isStop = false;
        private HiveMQClient _client;
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

            _client = new HiveMQClient(options);

            var connectResult = await _client.ConnectAsync().ConfigureAwait(false);
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
                    TrolleyStop();
                }
                if (key == Key.S)
                {
                    infoLabel.Content = "Trolley stopped!";
                    TrolleyStop();
                }

                if (key == Key.A)
                {
                    infoLabel.Content = "Gantry stopped!";
                    GantryStop();
                }
                if (key == Key.D)
                {
                    infoLabel.Content = "Gantry stopped!";
                    GantryStop();
                }

                if (key == Key.Up)
                {
                    infoLabel.Content = "Hoist stopped!";
                    HoistStop();
                }
                if (key == Key.Down)
                {
                    infoLabel.Content = "Hoist stopped!";
                    HoistStop();
                }

                if (key == Key.Q)
                {
                    infoLabel.Content = "Boom stopped!";
                    BoomStop();
                }
                if (key == Key.E)
                {
                    infoLabel.Content = "Boom stopped!";
                    BoomStop();
                }
            }
        }

        #region press
        private async void SendTrolleyForward()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/1\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/trolley/1", jsonString).ConfigureAwait(false);
        }
        private async void SendTrolleyBackward()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/2\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"2\"}}";
            await _client.PublishAsync("crane/components/trolley/2", jsonString).ConfigureAwait(false);
        }
        private async void SendGantryLeft()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/2\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"2\"}}";
            await _client.PublishAsync("crane/components/gantry/2", jsonString).ConfigureAwait(false);
        }
        private async void SendGantryRight()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/1\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/gantry/1", jsonString).ConfigureAwait(false);
        }
        private async void SendHoistUp()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/1\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/hoist/1", jsonString).ConfigureAwait(false);
        }
        private async void SendHoistDown()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/2\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"2\"}}";
            await _client.PublishAsync("crane/components/hoist/2", jsonString).ConfigureAwait(false);
        }
        private async void SendBoomUp()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/1\"},\"msg\":{\"target\":\"Boom\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/boom/1", jsonString).ConfigureAwait(false);
        }
        private async void SendBoomDown()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/2\"},\"msg\":{\"target\":\"Boom\",\"command\":\"2\"}}";
            await _client.PublishAsync("crane/components/boom/2", jsonString).ConfigureAwait(false);
        }
        private async void SendNoodstop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"meta/emergency_button\"},\"msg\":{\"isPressed\":\"true\"}}";
            await _client.PublishAsync("meta/emergency_button", jsonString).ConfigureAwait(false);
        }
        private async void SendUndoNoodstop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"meta/emergency_button\"},\"msg\":{\"isPressed\":\"false\"}}";
            await _client.PublishAsync("meta/emergency_button", jsonString).ConfigureAwait(false);
        }
        #endregion

        #region release
        private async void TrolleyStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/0\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/trolley/0", jsonString).ConfigureAwait(false);
        }
        private async void GantryStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/0\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/gantry/0", jsonString).ConfigureAwait(false);
        }
        private async void HoistStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/0\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/hoist/0", jsonString).ConfigureAwait(false);
        }
        private async void BoomStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/0\"},\"msg\":{\"target\":\"Boom\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/boom/0", jsonString).ConfigureAwait(false);
        }
        #endregion
    }
}
