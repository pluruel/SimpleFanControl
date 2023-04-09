using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrakenFanControl
{
    public class FanSensorParsor : IVisitor
    {
        private const string mIDPrefixTemperature = "LHM/Temp";
        private const string mIDPrefixFan = "LHM/Fan";
        private const string mIDPrefixControl = "LHM/Control";
        private const string mIDPrefixOSD = "LHM/OSD";

        private bool mIsStart = false;

        private Computer mComputer = null;

        public FanSensorParsor() { }

        public void start()
        {
            if (mIsStart == true)
                return;
            mIsStart = true;

            mComputer = new Computer();
            mComputer.IsCpuEnabled = true;
            mComputer.IsMotherboardEnabled = true;
            mComputer.IsControllerEnabled = true;
            mComputer.IsGpuEnabled = true;
            mComputer.IsStorageEnabled = true;
            mComputer.IsMemoryEnabled = true;

            mComputer.Open();
            mComputer.Accept(this);
        }

        public void stop()
        {
            if (mIsStart == false)
                return;
            mIsStart = false;

            if (mComputer != null)
            {
                mComputer.Close();
                mComputer = null;
            }
        }

        
        public void createFan()
        {
            var hardwareArray = mComputer.Hardware;
            for (int i = 0; i < hardwareArray.Count; i++)
            {
                string hardwareName = (hardwareArray[i].Name.Length > 0) ? hardwareArray[i].Name : "Unknown";
             
                var sensorArray = hardwareArray[i].Sensors;
                for (int j = 0; j < sensorArray.Length; j++)
                {
                    if (sensorArray[j].SensorType != SensorType.Fan)
                        continue;

                    string id = string.Format("{0}{1}", mIDPrefixFan, sensorArray[j].Identifier.ToString());
                    string name = (sensorArray[j].Name.Length > 0) ? sensorArray[j].Name : "Fan";
                    
                }

                var subHardwareArray = hardwareArray[i].SubHardware;
                for (int j = 0; j < subHardwareArray.Length; j++)
                {
                    var subSensorList = subHardwareArray[j].Sensors;
                    for (int k = 0; k < subSensorList.Length; k++)
                    {
                        if (subSensorList[k].SensorType != SensorType.Fan)
                            continue;

                        string id = string.Format("{0}{1}", mIDPrefixFan, subSensorList[k].Identifier.ToString());
                        string name = (subSensorList[k].Name.Length > 0) ? subSensorList[k].Name : "Fan";
                        
                    }
                }

            }
        }

        public void createControl()
        {
            var hardwareArray = mComputer.Hardware;
            for (int i = 0; i < hardwareArray.Count; i++)
            {
                string hardwareName = (hardwareArray[i].Name.Length > 0) ? hardwareArray[i].Name : "Unknown";
              

                var sensorArray = hardwareArray[i].Sensors;
                for (int j = 0; j < sensorArray.Length; j++)
                {
                    if (sensorArray[j].SensorType != SensorType.Control)
                        continue;

                    string id = string.Format("{0}{1}", mIDPrefixControl, sensorArray[j].Identifier.ToString());
                    string name = (sensorArray[j].Name.Length > 0) ? sensorArray[j].Name : "Control";
                   
                }

                var subHardwareArray = hardwareArray[i].SubHardware;
                for (int j = 0; j < subHardwareArray.Length; j++)
                {
                    var subSensorList = subHardwareArray[j].Sensors;
                    for (int k = 0; k < subSensorList.Length; k++)
                    {
                        if (subSensorList[k].SensorType != SensorType.Control)
                            continue;

                        string id = string.Format("{0}{1}", mIDPrefixControl, subSensorList[k].Identifier.ToString());
                        string name = (subSensorList[k].Name.Length > 0) ? subSensorList[k].Name : "Control";
                        
                    }
                }

            }
        }

        

        public void update()
        {
            mComputer.Accept(this);
        }

        /////////////////////////// Visitor ///////////////////////////
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }

        public void VisitParameter(IParameter parameter) { }
    }
}
