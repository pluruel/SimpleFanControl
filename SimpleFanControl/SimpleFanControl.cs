using System;
using System.Linq;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;
using Microsoft.VisualBasic.Devices;
using Timer = System.Windows.Forms.Timer;
using LibreHardwareMonitor.Hardware.Cpu;
using HidSharp;

namespace KrakenFanControl
{
    public partial class SimpleFanControl : Form
    {

        public SimpleFanControl()
        {

            InitializeComponent();
            this.FormClosing += FanControl_FormClosing;
            this.Shown += (s, e) => Hide();
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
        }

        private void ShowForm(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            var monitor = Monitor.GetInstance();
            monitor.refresh_computer();

        }

        private void Exit(object sender, EventArgs e)
        {


            maintainer.Stop();
            Monitor.GetInstance().stop();

            this.FormClosing -= FanControl_FormClosing;
            this.Close();

            trayIcon.Visible = false;
            trayIcon.Dispose();
            Application.Exit();
        }

        private void Show_Click(object sender, EventArgs e)
        {
            ShowForm(sender, e);
        }



        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowForm(sender, e);
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Exit(sender, e);
        }
        private void FanControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

        }

        private void maintainer_Tick(object sender, EventArgs e)
        {
            // Get the current pump temperature
            var monitor = Monitor.GetInstance();
            var status = monitor.maintain();

            this.fan1Value.Text = GetString(status[0]);
            this.fan2Value.Text = GetString(status[1]);
            this.cpuTemp.Text = GetString(status[2]);
            this.pumpSpdValue.Text = GetString(status[3]);
            this.liquidTemp.Text = GetString(status[4]);
        }
    }
}
