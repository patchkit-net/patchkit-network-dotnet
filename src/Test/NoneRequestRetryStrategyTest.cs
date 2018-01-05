using NUnit.Framework;

namespace PatchKit.Network
{
    public class NoneRequestRetryStrategyTest
    {
        [Test]
        public void ShouldRetryIsAlwaysFalse_For_NewInstance()
        {
            var strategy = new NoneRequestRetryStrategy();
            Assert.IsFalse(strategy.ShouldRetry);
        }
        
        [Test]
        public void ShouldRetryIsAlwaysFalse_For_OnRequestFailure()
        {
            var strategy = new NoneRequestRetryStrategy();
            strategy.OnRequestFailure();
            Assert.IsFalse(strategy.ShouldRetry);
        }
    }
}