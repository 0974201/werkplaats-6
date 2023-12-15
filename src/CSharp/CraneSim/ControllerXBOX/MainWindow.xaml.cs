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
        private bool leftJoystickUp = false;
        private bool leftJoystickDown = false;
        private bool _actionLeft = false;
        private bool _actionRight = false;

        private bool _arrowPadReleased = false;

        private DateTime _lastTrolleyMessage = DateTime.MinValue;
        private DateTime _lastGantryMessage = DateTime.MinValue;
        int counter = 0;

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

            // Start a timer to check the controller input periodically
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Start();
        }

        public void TimerTick(object sender, EventArgs e)
        {
            var gamepadState = controller.GetState().Gamepad;

            int joystickThreshold = 5000;

            // Check left joystick for trolley movement
            var leftThumbY = gamepadState.LeftThumbY;
            if (leftThumbY > joystickThreshold && !leftJoystickUp)
            {
                leftJoystickUp = true;
            }
            else if (leftThumbY < -joystickThreshold && !leftJoystickDown)
            {
                leftJoystickDown = true;
            }
            else if (leftThumbY > -joystickThreshold && leftThumbY < joystickThreshold)
            {
                if (leftJoystickUp || leftJoystickDown)
                {
                    LeftJoystickNeutral();
                    leftJoystickUp = false;
                    leftJoystickDown = false;
                }
            }

            var arrowPadState = gamepadState.Buttons;
            if (arrowPadState.HasFlag(GamepadButtonFlags.DPadLeft))
            {
                _actionLeft = true;
                _actionRight = false;
            }
            else if (arrowPadState.HasFlag(GamepadButtonFlags.DPadRight))
            {
                _actionRight = true;
                _actionLeft = false;
            }
            else if(_arrowPadReleased)
            {
                if (!arrowPadState.HasFlag(GamepadButtonFlags.DPadRight) && !arrowPadState.HasFlag(GamepadButtonFlags.DPadLeft))
                {
                    ReleaseAction(); // Call ReleaseAction only once when both DPad buttons are released
                    _arrowPadReleased = true;
                    _actionLeft = false;
                    _actionRight = false;
                }
            }
            

            if (leftJoystickUp)
            {
                TrolleyForward();
            }
            else if (leftJoystickDown)
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
        }


        private async void TrolleyForward()
        {
            if ((DateTime.Now - _lastTrolleyMessage).TotalSeconds < 1)
            {
                return;
            }
            _lastTrolleyMessage = DateTime.Now;

            counter++;
            output.Content = "Left joystick is moved up!" + counter.ToString();
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

            counter++;
            output.Content = "Left joystick is moved down!" + counter.ToString();
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }

        private async void LeftJoystickNeutral()
        {
            output.Content = "Left joystick is neutral!";

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
            output.Content = "DPad is Left!";

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
            output.Content = "DPad is Right!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }
        private async void ReleaseAction()
        {
            output.Content = "DPad is neutral!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/gantry/command\"},\"msg\":{\"target\":\"Gantry\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/gantry/command", jsonString).ConfigureAwait(false);
        }

    }
}