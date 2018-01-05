using NUnit.Framework;

namespace PatchKit.Network
{
    public class SimpleRequestRetryDelaystrategyTest
    {
        [Test]
        public void DelayGreaterOrEqual_ForEach_OnRequestFailure()
        {
            var strategy = new SimpleInfiniteRequestRetryStrategy();

            int previousDelay = strategy.DelayBeforeNextTry;
            
            for (int i = 0; i < 10; i++)
            {
                strategy.OnRequestFailure();
                Assert.GreaterOrEqual(strategy.DelayBeforeNextTry, previousDelay);
                previousDelay = strategy.DelayBeforeNextTry;
            }
        }
        
        [Test]
        public void DelayResets_For_OnRequestSuccess()
        {
            var strategy = new SimpleInfiniteRequestRetryStrategy();

            int initialDelay = strategy.DelayBeforeNextTry;
            
            for (int i = 0; i < 10; i++)
            {
                strategy.OnRequestFailure();
            }
            
            strategy.OnRequestSuccess();

            Assert.AreEqual(strategy.DelayBeforeNextTry, initialDelay);
        }
        
        [Test]
        public void DelayIsPositive_For_NewInstance()
        {
            var strategy = new SimpleInfiniteRequestRetryStrategy();

            Assert.GreaterOrEqual(strategy.DelayBeforeNextTry, 0);
        }
    }
}