using SimpleFanControl;

namespace Tester
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetProperFanSpd_WhenTemperatureIsNull_Returns30()
        {
            float? nullableTemp = null;
            float expectedFanSpeed = 30f;

            float actualFanSpeed = Functions.GetProperFanSpd(nullableTemp);

            Assert.AreEqual(expectedFanSpeed, actualFanSpeed);
        }

        public static IEnumerable<TestCaseData> TemperatureAndExpectedFanSpeedTestCases
        {
            get
            {
                for (float temperature = 25f; temperature <= 40f; temperature += 0.01f)
                {
                    float expectedFanSpeed;
                    if (temperature < 29f)
                    {
                        expectedFanSpeed = 30f;
                    }
                    else if (temperature < 32f)
                    {
                        expectedFanSpeed = 30f + 30f * (temperature - 29f) / (32f - 29f);
                    }
                    else if (temperature < 35f)
                    {
                        expectedFanSpeed = 60f + 40f * (temperature - 32f) / (35f - 32f);
                    }
                    else
                    {
                        expectedFanSpeed = 100f;
                    }

                    yield return new TestCaseData(temperature, expectedFanSpeed);
                }
            }
        }

        private float pre = 30f;

        [Test, TestCaseSource(nameof(TemperatureAndExpectedFanSpeedTestCases))]
        public void GetProperFanSpd_GivenVariousTemperatures_ReturnsExpectedFanSpeed(float temperature, float expectedFanSpeed)
        {
            float actualFanSpeed = Functions.GetProperFanSpd(temperature);

            Assert.AreEqual(expectedFanSpeed, actualFanSpeed, 1e-3);
            Assert.Less(actualFanSpeed - pre, 0.134);
            pre = actualFanSpeed;
        }


        public static IEnumerable<TestCaseData> TemperatureAndExpectedPumpSpeedTestCases
        {
            get
            {
                for (float temperature = 50f; temperature <= 70f; temperature += 0.01f)
                {
                    yield return new TestCaseData(temperature);
                }
            }
        }

        private float pre_pumpsped = 30f;

        [Test, TestCaseSource(nameof(TemperatureAndExpectedPumpSpeedTestCases))]
        public void GetProperPumpSpd_GivenVariousTemperatures(float temperature)
        {
            float actualFanSpeed = Functions.GetProperPumpSpd(temperature);

            Assert.Less(actualFanSpeed - pre, 0.13);
            pre = actualFanSpeed;
        }
    }
}