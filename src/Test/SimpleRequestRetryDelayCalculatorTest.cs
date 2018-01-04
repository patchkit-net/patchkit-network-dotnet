using NUnit.Framework;

namespace PatchKit.Network
{
    public class SimpleRequestRetryDelayCalculatorTest
    {
        [Test]
        public void DelayGreaterOrEqual_ForEach_OnRequestFailure()
        {
            var calculator = new SimpleRequestRetryDelayCalculator();

            int previousDelay = calculator.Delay;
            
            for (int i = 0; i < 10; i++)
            {
                calculator.OnRequestFailure();
                Assert.GreaterOrEqual(calculator.Delay, previousDelay);
                previousDelay = calculator.Delay;
            }
        }
        
        [Test]
        public void DelayResets_For_OnRequestSuccess()
        {
            var calculator = new SimpleRequestRetryDelayCalculator();

            int initialDelay = calculator.Delay;
            
            for (int i = 0; i < 10; i++)
            {
                calculator.OnRequestFailure();
            }
            
            calculator.OnRequestSuccess();

            Assert.AreEqual(calculator.Delay, initialDelay);
        }
        
        [Test]
        public void DelayIsPositive_For_NewInstance()
        {
            var calculator = new SimpleRequestRetryDelayCalculator();

            Assert.GreaterOrEqual(calculator.Delay, 0);
        }
    }
}