using NUnit.Framework;

namespace PatchKit.Network
{
    public class SimpleRequestTimeoutCalculatorTest
    {
        [Test]
        public void TimeoutGreaterOrEqual_ForEach_OnRequestFailure()
        {
            var calculator = new SimpleRequestTimeoutCalculator();

            int previousDelay = calculator.Timeout;
            
            for (int i = 0; i < 10; i++)
            {
                calculator.OnRequestFailure();
                Assert.GreaterOrEqual(calculator.Timeout, previousDelay);
                previousDelay = calculator.Timeout;
            }
        }
        
        [Test]
        public void TimeoutDoesntReset_For_OnRequestSuccess()
        {
            var calculator = new SimpleRequestTimeoutCalculator();

            int initialDelay = calculator.Timeout;
            
            for (int i = 0; i < 10; i++)
            {
                calculator.OnRequestFailure();
            }
            
            calculator.OnRequestSuccess();

            Assert.AreNotEqual(calculator.Timeout, initialDelay);
        }
        
        [Test]
        public void TimeoutIsPositive_For_NewInstance()
        {
            var calculator = new SimpleRequestTimeoutCalculator();

            Assert.GreaterOrEqual(calculator.Timeout, 0);
        }
    }
}