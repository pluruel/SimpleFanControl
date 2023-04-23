using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFanControl
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
            hw.SetPumpSpd(Functions.GetProperPumpSpd(hw._cpu_temp.Value));
            hw.SetFanSpd(Functions.GetProperFanSpd(hw._liquid_temp.Value));
            List<float?> status = new List<float?>();

            status.Add(hw._cpu_fan_spd.Value);
            status.Add(hw._cpu_sub_fan_spd.Value);
            status.Add(hw._cpu_temp.Value);
            status.Add(hw._pump_fan_spd.Value);
            status.Add(hw._liquid_temp.Value);

            return status;
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
