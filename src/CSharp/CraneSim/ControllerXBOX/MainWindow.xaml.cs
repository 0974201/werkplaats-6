using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using SharpDX.XInput;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
using System.Windows;

namespace ControllerXBOX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller;
        private bool _leftJoystickUp = false;
        private bool _leftJoystickDown = false;
        private bool _actionLeft = false;
        private bool _actionRight = false;
        private bool _rightJoystickUp = false;
        private bool _rightJoystickDown = false;
        private bool _yUp = false;
        private bool _aDown = false;
        private bool _isInStop = false;

        private bool _pickUp = false;

        private bool _arrowPadReleased = true;
        private bool _letterButtonsReleased = true;

        private DateTime _lastTrolleyMessage = DateTime.MinValue;
        private DateTime _lastGantryMessage = DateTime.MinValue;
        private DateTime _lastHoistMessage = DateTime.MinValue;
        private DateTime _lastBoomMessage = DateTime.MinValue;
        private DateTime _lastPickupMessage = DateTime.MinValue;

        private HiveMQClient _client;

        public MainWindow()
        {
            InitializeComponent();
            ListenToXbox();
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

        public void ListenToXbox()
        {
            controller = new Controller(UserIndex.One);

            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }

        public void TimerTick(object sender, EventArgs e)
        {

            var gamepadState = controller.GetState().Gamepad;
            var leftThumbY = gamepadState.LeftThumbY;
            var rightThumbY = gamepadState.RightThumbY;
            int joystickThreshold = 5000;
            var arrowPadState = gamepadState.Buttons;


            if (_isInStop)
            {
                if (arrowPadState.HasFlag(GamepadButtonFlags.X))
                {
                    UndoNoodStop();
                    _isInStop = false;
                }
            }
            else
            {
                if (leftThumbY > joystickThreshold && !_leftJoystickUp)
                {
                    _leftJoystickUp = true;
                }
                else if (leftThumbY < -joystickThreshold && !_leftJoystickDown)
                {
                    _leftJoystickDown = true;
                }
                else if (leftThumbY > -joystickThreshold && leftThumbY < joystickThreshold)
                {
                    if (_leftJoystickUp || _leftJoystickDown)
                    {
                        LeftJoystickNeutral();
                        _leftJoystickUp = false;
                        _leftJoystickDown = false;
                    }
                }


                if (arrowPadState.HasFlag(GamepadButtonFlags.DPadLeft))
                {
                    _actionLeft = true;
                    _actionRight = false;
                    _arrowPadReleased = false;
                }
                else if (arrowPadState.HasFlag(GamepadButtonFlags.DPadRight))
                {
                    _actionRight = true;
                    _actionLeft = false;
                    _arrowPadReleased = false;
                }
                else if (!_arrowPadReleased)
                {
                    if (!arrowPadState.HasFlag(GamepadButtonFlags.DPadRight) && !arrowPadState.HasFlag(GamepadButtonFlags.DPadLeft))
                    {
                        ReleaseAction();
                        _arrowPadReleased = true;
                        _actionLeft = false;
                        _actionRight = false;
                    }
                }
                if (arrowPadState.HasFlag(GamepadButtonFlags.Y))
                {
                    _yUp = true;
                    _aDown = false;
                    _letterButtonsReleased = false;
                }
                else if (arrowPadState.HasFlag(GamepadButtonFlags.A))
                {
                    _aDown = true;
                    _yUp = false;
                    _letterButtonsReleased = false;
                }
                else if (!_letterButtonsReleased)
                {
                    if (!arrowPadState.HasFlag(GamepadButtonFlags.Y) && !arrowPadState.HasFlag(GamepadButtonFlags.A))
                    {
                        BoomRelease();
                        _letterButtonsReleased = true;
                        _yUp = false;
                        _aDown = false;
                    }
                }
                if (arrowPadState.HasFlag(GamepadButtonFlags.B))
                {
                    NoodStop();
                    _isInStop = true;
                }

                if (rightThumbY > joystickThreshold && !_rightJoystickUp)
                {
                    _rightJoystickUp = true;
                }
                else if (rightThumbY < -joystickThreshold && !_rightJoystickDown)
                {
                    _rightJoystickDown = true;
                }
                else if (rightThumbY > -joystickThreshold && rightThumbY < joystickThreshold)
                {
                    if (_rightJoystickUp || _rightJoystickDown)
                    {
                        HoistRelease();
                        _rightJoystickUp = false;
                        _rightJoystickDown = false;
                    }
                }

                if (arrowPadState.HasFlag(GamepadButtonFlags.RightShoulder))
                {
                    _pickUp = !_pickUp;
                    SendPickup();
                }

                if (_leftJoystickUp)
                {
                    TrolleyForward();
                }
                else if (_leftJoystickDown)
                {
                    TrolleyBackwards();
                }

                else if (_actionLeft)
                {
                    GantryLeft();
                }
                else if (_actionRight)
                {
                    GantryRight();
                }

                else if (_rightJoystickUp)
                {
                    HoistUp();
                }
                else if (_rightJoystickDown)
                {
                    HoistDown();
                }

                else if (_yUp)
                {
                    BoomUp();
                }
                else if (_aDown)
                {
                    BoomDown();
                }
            }
        }


        private async void TrolleyForward()
        {
            if ((DateTime.Now - _lastTrolleyMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastTrolleyMessage = DateTime.Now;
            output.Content = "Trolley is moved up!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
        private async void TrolleyBackwards()
        {

            if ((DateTime.Now - _lastTrolleyMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastTrolleyMessage = DateTime.Now;
            output.Content = "Trolley is moved down!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
        private async void LeftJoystickNeutral()
        {
            output.Content = "Trolley is neutral!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }

        private async void GantryLeft()
        {
            if ((DateTime.Now - _lastGantryMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastGantryMessage = DateTime.Now;
            output.Content = "Gantry is going Left!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }
        private async void GantryRight()
        {
            if ((DateTime.Now - _lastGantryMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastGantryMessage = DateTime.Now;
            output.Content = "Gantry is going Right!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }
        private async void ReleaseAction()
        {
            output.Content = "Gantry is neutral!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }

        private async void HoistUp()
        {
            if ((DateTime.Now - _lastHoistMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastHoistMessage = DateTime.Now;
            output.Content = "Hoist is moved up!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/command\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/hoist/command", jsonString).ConfigureAwait(false);
        }
        private async void HoistDown()
        {
            if ((DateTime.Now - _lastHoistMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastHoistMessage = DateTime.Now;
            output.Content = "Hoist is moved Down!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/2\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/hoist/command", jsonString).ConfigureAwait(false);
        }
        private async void HoistRelease()
        {
            output.Content = "Hoist is in neutral!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/hoist/command\"},\"msg\":{\"target\":\"Hoist\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/hoist/command", jsonString).ConfigureAwait(false);
        }

        private async void BoomUp()
        {
            if ((DateTime.Now - _lastBoomMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastBoomMessage = DateTime.Now;
            output.Content = "Boom is moved up!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/1\"},\"msg\":{\"target\":\"Boom\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/boom/command", jsonString).ConfigureAwait(false);
        }
        private async void BoomDown()
        {
            if ((DateTime.Now - _lastBoomMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastBoomMessage = DateTime.Now;
            output.Content = "Boom is moved Down!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/2\"},\"msg\":{\"target\":\"Boom\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/boom/command", jsonString).ConfigureAwait(false);
        }
        private async void BoomRelease()
        {
            output.Content = "Boom is in neutral!";
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/boom/command\"},\"msg\":{\"target\":\"Boom\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/boom/command", jsonString).ConfigureAwait(false);
        }

        private async void NoodStop()
        {
            output.Content = "Noodstop has been pressed!!!!\nPress X to reset";
            var jsonString = "{\"meta\":{\"topic\":\"meta/emergency_button\"},\"msg\":{\"isPressed\":\"true\"}}";
            await _client.PublishAsync("meta/emergency_button", jsonString).ConfigureAwait(false);
        }
        private async void UndoNoodStop()
        {
            output.Content = "Undid the Noodstop!";
            var jsonString = "{\"meta\":{\"topic\":\"meta/emergency_button\"},\"msg\":{\"isPressed\":\"false\"}}";
            await _client.PublishAsync("meta/emergency_button", jsonString).ConfigureAwait(false);
        }

        private async void SendPickup()
        {
            if ((DateTime.Now - _lastPickupMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastPickupMessage = DateTime.Now;
            output.Content = "Grabbing or releasing container";
            if (_pickUp)
            {
                var jsonString = "{\"meta\":{\"topic\":\"crane/connectrequest\"},\"msg\":{\"isconnecting\":\"true\"}}";
                await _client.PublishAsync("crane/connectrequest", jsonString).ConfigureAwait(false);
            }
            else
            {
                var jsonString = "{\"meta\":{\"topic\":\"crane/connectrequest\"},\"msg\":{\"isconnecting\":\"false\"}}";
                await _client.PublishAsync("crane/connectrequest", jsonString).ConfigureAwait(false);
            }
        }
    }
}