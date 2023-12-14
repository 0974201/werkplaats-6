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
        int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            ListenToXbox();
        }

        public void ListenToXbox()
        {
            // Initialize the controller
            controller = new Controller(UserIndex.One);

            // Start a timer to check the controller input periodically
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(100); // Adjust the interval as needed
            timer.Start();
        }

        public void TimerTick(object sender, EventArgs e)
        {
            // Get the current state of the left joystick Y-axis
            var state = controller.GetState().Gamepad.LeftThumbY;

            // Define a threshold for joystick movement
            int joystickThreshold = 5000;

            if (state > joystickThreshold && !leftJoystickUp)
            {
                leftJoystickUp = true;
            }
            else if (state < -joystickThreshold && !leftJoystickDown)
            {
                DoSomethingOnLeftJoystickDown();
                leftJoystickDown = true;
            }
            else if (state > -joystickThreshold && state < joystickThreshold)
            {
                if (leftJoystickUp || leftJoystickDown)
                {
                    DoSomethingOnLeftJoystickNeutral();
                    leftJoystickUp = false;
                    leftJoystickDown = false;
                }
            }

            // Call DoSomethingOnLeftJoystickUp every tick when the joystick is up
            if (leftJoystickUp)
            {
                DoSomethingOnLeftJoystickUp();
            }
        }

        private void DoSomethingOnLeftJoystickUp()
        {
            counter++;
            output.Content = "Left joystick is moved up!" + counter.ToString();
        }

        private void DoSomethingOnLeftJoystickDown()
        {
            output.Content = "Left joystick is moved down!";
        }

        private void DoSomethingOnLeftJoystickNeutral()
        {
            // General code for both up and down scenarios.
            // Does this once
            output.Content = "Left joystick is neutral!";
        }

    }
}