using System;
using System.Linq;
using System.Windows.Forms;
using LibreHardwareMonitor.Hardware;
using Microsoft.VisualBasic.Devices;
using Timer = System.Windows.Forms.Timer;
using Computer = LibreHardwareMonitor.Hardware.Computer;

namespace KrakenFanControl
{
    public partial class FanControl : Form
    {
        private readonly Computer _computer;
        private readonly Timer _timer;

        public FanControl()
        {

            InitializeComponent();

            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsStorageEnabled = true
            };
            _computer.Open();
  

            _timer = new Timer();
            _timer.Interval = 5000; // 5 seconds interval
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get the current pump temperature
            float pumpTemperature = GetPumpTemperature(_computer);

            // Calculate the desired fan speed based on the pump temperature
            int fanSpeed = CalculateFanSpeed(pumpTemperature);

            // Set the fan speed
            SetFanSpeed(_computer, fanSpeed);
        }

        private float GetPumpTemperature(Computer computer)
        {
            // Find the NZXT x73 pump temperature sensor
            var temperatureSensor = computer.Hardware
                .Where(h => h.HardwareType == HardwareType.Cpu)
                .SelectMany(h => h.Sensors)
                .FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("NZXT x73"));

            // Return the current temperature value
            return temperatureSensor?.Value ?? 0;
        }

        private int CalculateFanSpeed(float temperature)
        {
            // Set the desired temperature range and fan speed range
            float minTemperature = 30; // Minimum temperature in Celsius
            float maxTemperature = 80; // Maximum temperature in Celsius
            int minFanSpeed = 25; // Minimum fan speed in percentage (0-100)
            int maxFanSpeed = 100; // Maximum fan speed in percentage (0-100)

            // Clamp the temperature to the specified range
            float clampedTemperature = Math.Clamp(temperature, minTemperature, maxTemperature);

            // Calculate the fan speed
            float fanSpeedPercentage = (clampedTemperature - minTemperature) / (maxTemperature - minTemperature) * (maxFanSpeed - minFanSpeed) + minFanSpeed;

            // Round the fan speed and clamp it to the valid range
            return (int)Math.Clamp(Math.Round(fanSpeedPercentage), minFanSpeed, maxFanSpeed);
        }

        private void SetFanSpeed(Computer computer, int fanSpeed)
        {
            // Find the NZXT x73 fans
            var hardware = computer.Hardware.FirstOrDefault(hw => hw.Name.Contains("X3"));
            if (hardware != null)
            {
                hardware.Update();
                var sensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);
                Console.WriteLine($"NZXT X73 냉각수 온도: {sensor.Value} °C");
            }
            bool foundFanSensor = false;
            foreach (var hw in computer.Hardware)
            {
                hw.Update();
                var fanSensor = hw.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Fan);
                if (fanSensor != null)
                {
                    Console.WriteLine($"CPU 팬 속도: {fanSensor.Value} RPM");
                    foundFanSensor = true;
                    break;
                }
            }
            if (foundFanSensor)
            {
                Console.WriteLine("CPU 팬 센서를 찾을 수 없습니다.");
            }

        }

        // Ensure the Computer object is closed when the form is closed
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _timer.Stop();
            _computer.Close();
        }
    }
}
