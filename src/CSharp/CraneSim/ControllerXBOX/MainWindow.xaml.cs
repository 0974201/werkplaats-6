using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using SharpDX.XInput;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
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
        private DateTime _lastKeyDownTime = DateTime.MinValue;
        private DateTime _lastKeyUpTime = DateTime.MinValue;
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
            var state = controller.GetState().Gamepad.LeftThumbY;

            int joystickThreshold = 5000;

            if (state > joystickThreshold && !leftJoystickUp)
            {
                leftJoystickUp = true;
            }
            else if (state < -joystickThreshold && !leftJoystickDown)
            {
                
                leftJoystickDown = true;
            }
            else if (state > -joystickThreshold && state < joystickThreshold)
            {
                if (leftJoystickUp || leftJoystickDown)
                {
                    LeftJoystickNeutral();
                    leftJoystickUp = false;
                    leftJoystickDown = false;
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
        }

        private async void TrolleyForward()
        {
            if ((DateTime.Now - _lastKeyDownTime).TotalSeconds < 1)
            {
                return;
            }
            _lastKeyDownTime = DateTime.Now;

            counter++;
            output.Content = "Left joystick is moved up!" + counter.ToString();
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
        private async void TrolleyBackwards()
        {
            
            if ((DateTime.Now - _lastKeyDownTime).TotalSeconds < 1)
            {
                return;
            }
            _lastKeyDownTime = DateTime.Now;

            counter++;
            output.Content = "Left joystick is moved down!" + counter.ToString();
            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"-1\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }

        private async void LeftJoystickNeutral()
        {
            // General code for both up and down scenarios.
            // Does this once
            output.Content = "Left joystick is neutral!";

            var jsonString = "{\"meta\":{\"topic\":\"crane/components/trolley/command\"},\"msg\":{\"target\":\"Trolley\",\"command\":\"0\"}}";
            await _client.PublishAsync("crane/components/trolley/command", jsonString).ConfigureAwait(false);
        }
    }
}