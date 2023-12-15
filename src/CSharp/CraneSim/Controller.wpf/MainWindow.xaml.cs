using System;
using System.Windows;
using System.Windows.Input;
using HiveMQtt.Client;
using HiveMQtt.Client.Options;

namespace Controller.wpf
{
    public partial class MainWindow : Window
    {

        private bool _isStop = false;
        private HiveMQClient _client;
        private bool _gantryMovement = false;
        private bool _boomMovement = false;

        private DateTime _lastKeyDownTime = DateTime.MinValue;
        private DateTime _lastKeyUpTime = DateTime.MinValue;
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
            if ((DateTime.Now - _lastKeyDownTime).TotalSeconds < 1)
            {
                // Ignore the key press if it's been less than a second since the last one
                return;
            }

            _lastKeyDownTime = DateTime.Now;
            HandleKeyPress(e.Key);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if ((DateTime.Now - _lastKeyUpTime).TotalSeconds < 1)
            {
                // Ignore the key release if it's been less than a second since the last one
                return;
            }

            _lastKeyUpTime = DateTime.Now;
            HandleKeyRelease(e.Key);
        }
        private void HandleKeyPress(Key key)
        {
            if (!_isStop)
            {
                if (!_boomMovement && !_gantryMovement)
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

                    if (key == Key.A)
                    {
                        infoLabel.Content = "Gantry left!";
                        _gantryMovement = true;
                        SendGantryLeft();
                    }
                    if (key == Key.D)
                    {
                        infoLabel.Content = "Gantry right!";
                        _gantryMovement = true;
                        SendGantryRight();
                    }
                    if (key == Key.Q)
                    {
                        infoLabel.Content = "Boom Up!";
                        _boomMovement = true;
                        SendBoomUp();
                    }
                    if (key == Key.E)
                    {
                        infoLabel.Content = "Boom Down!";
                        _boomMovement = true;
                        SendBoomDown();
                    }
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
                if (!_boomMovement && !_gantryMovement)
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
                }
                if (!_boomMovement)
                {
                    if (key == Key.A)
                    {
                        infoLabel.Content = "Gantry stopped!";
                        _gantryMovement = false;
                        GantryStop();
                    }
                    if (key == Key.D)
                    {
                        infoLabel.Content = "Gantry stopped!";
                        _gantryMovement = false;
                        GantryStop();
                    }
                }
                if (!_gantryMovement)
                {
                    if (key == Key.Q)
                    {
                        infoLabel.Content = "Boom stopped!";
                        _boomMovement = false;
                        BoomStop();
                    }
                    if (key == Key.E)
                    {
                        infoLabel.Content = "Boom stopped!";
                        _boomMovement = false;
                        BoomStop();
                    }
                }
            }
        }

        #region press
        private async void SendTrolleyForward()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
        private async void SendTrolleyBackward()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
        private async void SendGantryLeft()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }
        private async void SendGantryRight()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }
        private async void SendHoistUp()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/command\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/hoist/command", jsonString).ConfigureAwait(false);
        }
        private async void SendHoistDown()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/2\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/hoist/command", jsonString).ConfigureAwait(false);
        }
        private async void SendBoomUp()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/1\"},\"msg\":{\"target\":\"Boom\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/boom/command", jsonString).ConfigureAwait(false);
        }
        private async void SendBoomDown()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/2\"},\"msg\":{\"target\":\"Boom\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/boom/command", jsonString).ConfigureAwait(false);
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
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
        private async void GantryStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }
        private async void HoistStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/command\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/hoist/command", jsonString).ConfigureAwait(false);
        }
        private async void BoomStop()
        {
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/command\"},\"msg\":{\"target\":\"Boom\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/boom/command", jsonString).ConfigureAwait(false);
        }
        #endregion
    }
}
