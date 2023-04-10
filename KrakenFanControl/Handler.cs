using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;
using Microsoft.VisualBasic.Devices;
using Computer = LibreHardwareMonitor.Hardware.Computer;

namespace KrakenFanControl
{
    internal class Handler
    {
        private Computer _computer = null;

        public ISensor _cpu_temp = null;
        //private List<ISensor> _mem_temp = null;
        public ISensor _cpu_fan_spd = null;
        public ISensor _cpu_sub_fan_spd = null;

        public ISensor _liquid_temp = null;
        public ISensor _pump_fan_spd = null;
        


        private ISensor _pump_ctrl = null;
        private ISensor _cpu_fan_ctrl = null;

        public Handler(Computer computer) {
            _computer = computer;
        }



        public void SetPumpSpd(float value)
        {
            if (_pump_ctrl.Value != value)
            {
                _pump_ctrl.Control.SetSoftware(value);
            }
            
        }

        public void SetFanSpd(float value)
        {
            if (_cpu_fan_ctrl.Value != value)
            {
                _cpu_fan_ctrl.Control.SetSoftware(value);
            }
                
        }


        public void Accept(IVisitor visit)
        {
            var i = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Motherboard);
            if (i != null)
            {
                var si = i.SubHardware.FirstOrDefault(h => h.HardwareType == HardwareType.SuperIO);
                if (si != null)
                {
                    si.Update();
                    var sens = si.Sensors.Where(s => s.SensorType == SensorType.Fan);
                    foreach (var sen in sens)
                    {
                        if (sen.Name == "Fan #2")
                        {
                            _cpu_fan_spd = sen;
                        }
                        else if (sen.Name == "Fan #3")
                        {
                            _cpu_sub_fan_spd = sen;
                        }
                    }
                    var ctrls = si.Sensors.Where(s => s.SensorType == SensorType.Control);
                    foreach (var ctrl in ctrls)
                    {
                        if (ctrl.Name == "Fan #2")
                        {
                            _cpu_fan_ctrl = ctrl;
                        }
                    }
                }

            }

            var kraken = _computer.Hardware.FirstOrDefault(h => h.Name.Contains("Kraken"));
            if (kraken != null)
            {
                kraken.Update();
                var sens = kraken.Sensors;
                foreach (var sen in sens)
                {
                    if (sen.Name == "Pump Control")
                    {
                        _pump_ctrl = sen;
                    }
                    else if (sen.Name == "Pump")
                    {
                        _pump_fan_spd = sen;
                    }
                    else if (sen.Name == "Internal Water")
                    {
                        _liquid_temp = sen;

                    }
                }
            }

            var cpu = _computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.Cpu);

            if (cpu != null)
            {
                cpu.Update();
                var sens = cpu.Sensors.Where(s => s.SensorType == SensorType.Temperature);
                foreach (var sen in sens)
                {
                    if (sen.Name == "CPU Package")
                    {
                        _cpu_temp = sen;
                    }
                }
            }
        }
    }
}

