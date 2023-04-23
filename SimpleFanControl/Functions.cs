using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFanControl
{
    public class Functions
    {

        public static float GetProperPumpSpd(float? nullable_cpu_temp)
        {
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

        public static float GetProperFanSpd(float? nullable_temp)
        {
            var minTemp = 29f;
            var midTemp = 32f;
            var maxTemp = 35f;
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
    }
}
