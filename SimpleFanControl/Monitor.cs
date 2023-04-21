using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakenFanControl
{
    public class Monitor : IVisitor
    {
        private static Monitor me = null;

        private Computer computer = null;
        private Handler hw = null;

        private Monitor()
        {
            computer = new Computer();
            computer.IsCpuEnabled = true;
            computer.IsMotherboardEnabled = true;
            computer.IsControllerEnabled = true;
            computer.IsMemoryEnabled = true;

            computer.Open();
            computer.Accept(this);

            hw = new Handler(computer);
        }

        public static Monitor GetInstance()
        {
            if (me == null)
            {
                me = new Monitor();
            }

            return me;
        }

        public void stop()
        {
            computer.Close();
            computer = null;
            me = null;
            hw = null;
        }

        public void refresh_computer()
        {
            computer.Accept(this);
        }

        public List<float?> maintain()
        {
            hw.Accept(this);
            hw.SetPumpSpd(GetProperPumpSpd());
            hw.SetFanSpd(GetProperFanSpd());
            List<float?> status = new List<float?>();

            status.Add(hw._cpu_fan_spd.Value);
            status.Add(hw._cpu_sub_fan_spd.Value);
            status.Add(hw._cpu_temp.Value);
            status.Add(hw._pump_fan_spd.Value);
            status.Add(hw._liquid_temp.Value);
            status.Add(hw._ram_temp.Value);
            status.Add(hw._ram2_temp.Value);

            return status;
        }

        private float GetProperPumpSpd()
        {
            var nullable_cpu_temp = hw._cpu_temp.Value;
            if (nullable_cpu_temp.HasValue)
            {
                float midTemp = 60f;
                float maxTemp = 65f;
                float temp = nullable_cpu_temp.Value;
                if (temp < midTemp)
                {
                    return 60f;
                }
                else if (temp >= maxTemp)
                {
                    return 100f;
                }
                else
                {
                    float range = maxTemp - midTemp;
                    float rangeRatio = 40f / range;
                    float tempInRange = temp - midTemp;
                    float ratio = tempInRange * rangeRatio;
                    float pumpSpeed = ratio + 60f;
                    return pumpSpeed;
                }

            }
            return 60f;
        }

        private float GetLiquidRelatedFanSpd()
        {
            var nullable_temp = hw._liquid_temp.Value;
            var minTemp = 29f;
            var midTemp = 33f;
            var maxTemp = 37f;
            float temp;

            if (nullable_temp.HasValue)
            {
                temp = nullable_temp.Value;
                if (temp < minTemp)
                {
                    return 30f;
                }
                else if (temp >= maxTemp)
                {
                    return 100f;
                }
                else if (temp < midTemp)
                {
                    var range = midTemp - minTemp;
                    var rangeRatio = 30f / range;
                    var tempInRange = temp - minTemp;
                    var ratio = tempInRange * rangeRatio;
                    var fanSpeed = ratio + 30f;
                    return fanSpeed;
                }
                else
                {
                    var range = maxTemp - midTemp;
                    var rangeRatio = 40f / range;
                    var tempInRange = temp - midTemp;
                    var ratio = tempInRange * rangeRatio;
                    var fanSpeed = ratio + 60f;
                    return fanSpeed;
                }
            }
            else
            {
                return 30;
            }
        }

        private float GetRamTemRelatedFanSpd() {
            var nullable_temp = hw._ram_temp.Value;
            var minTemp = 35f;
            var maxTemp = 50f;
            float temp;

            if (nullable_temp.HasValue)
            {
                temp = nullable_temp.Value;
                if (temp < minTemp)
                {
                    return 30f;
                }
                else if (temp >= maxTemp)
                {
                    return 100f;
                }
                else
                {
                    var range = maxTemp - minTemp;
                    var rangeRatio = 70f / range;
                    var tempInRange = temp - minTemp;
                    var ratio = tempInRange * rangeRatio;
                    var fanSpeed = ratio + 30f;
                    return fanSpeed;
                }
            }
            else
            {
                return 30;
            }
        }

        private float GetProperFanSpd()
        {
            return Math.Max(GetRamTemRelatedFanSpd(), GetLiquidRelatedFanSpd());
        }

        public void VisitComputer(IComputer computer)
        {
        }

        public void VisitHardware(IHardware hardware)
        {

        }

        public void VisitSensor(ISensor sensor) { }

        public void VisitParameter(IParameter parameter) { }
    }
}
