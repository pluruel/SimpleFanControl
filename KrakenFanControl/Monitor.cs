using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
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

        private Monitor() {
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
            
            return status;
        }

        private float GetProperPumpSpd()
        {
            if (hw._cpu_temp.Value > 75) {
                return 100;
            }
            return 70;
            
        }

        private float GetProperFanSpd()
        {
            var nullable_temp = hw._liquid_temp.Value;
            var minTemp = 30f;
            var maxTemp = 40f;
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
                else if (temp < 35)
                {
                    var range = 5f;
                    var rangeRatio = 30f / range;
                    var tempInRange = temp - minTemp;
                    var ratio = tempInRange * rangeRatio;
                    var fanSpeed = ratio + 30f;
                    return fanSpeed;
                }
                else
                {
                    var range = maxTemp - 35f;
                    var rangeRatio = 40f / range;
                    var tempInRange = temp - 35f;
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
