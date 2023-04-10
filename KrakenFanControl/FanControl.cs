using System;
using System.Linq;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;
using Microsoft.VisualBasic.Devices;
using Timer = System.Windows.Forms.Timer;
using Computer = LibreHardwareMonitor.Hardware.Computer;
using LibreHardwareMonitor.Hardware.Cpu;
using HidSharp;

namespace KrakenFanControl
{
    public partial class FanControl : Form
    {
        private Computer _computer;
        private readonly Timer _timer;

        public FanControl()
        {

            InitializeComponent();

            

            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsStorageEnabled = true,


            };
            _computer.Open();


            _timer = new Timer();
            _timer.Interval = 1000; // 5 seconds interval
            _timer.Tick += Timer_Tick;
            _timer.Start();

            this.Load += Form1_Load;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get the current pump temperature
            var monitor = Monitor.GetInstance();
            var status = monitor.maintain();

            fan1Value.Text = GetString(status[0]);
            fan2Value.Text = GetString(status[1]);
            cpuTemp.Text = GetString(status[2]);
            pumpSpdValue.Text = GetString(status[3]);
            liquidTemp.Text = GetString(status[4]);
        }

        private string GetString(float? f)
        {
            if (f.HasValue)
            {
                return f.Value.ToString();
            }
            else
            {
                return "Loading..";
            }

        }


        // Ensure the Computer object is closed when the form is closed
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _timer.Stop();
            _computer.Close();
        }

        private void ShowForm(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
        }
        private void Exit(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}
