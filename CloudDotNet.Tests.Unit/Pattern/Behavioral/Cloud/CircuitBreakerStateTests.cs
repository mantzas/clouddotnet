using CloudDotNet.Pattern.Behavioral.Cloud;
using FluentAssertions;
using System;
using System.Threading;
using Xunit;

namespace CloudDotNet.Tests.Unit.Pattern.Behavioral.Cloud
{
    public class CircuitBreakerStateTests
    {
        [Fact]
        public void IncrementFailureCount()
        {
            var state = new CircuitBreakerState();
            state.IncrementFailureCount();
            state.CurrentFailureCount.Should().Be(1);
            state.LastFailureTimestamp.Should().BeAfter(DateTimeOffset.UtcNow.AddSeconds(-1.0));
        }

        [Fact]
        public void IncrementRetrySuccessCount()
        {
            var state = new CircuitBreakerState();
            state.IncrementRetrySuccessCount();
            state.RetrySuccessCount.Should().Be(1);
        }

        [Fact]
        public void Reset()
        {
            var state = new CircuitBreakerState();
            state.IncrementFailureCount();
            state.IncrementRetrySuccessCount();
            state.CurrentFailureCount.Should().Be(1);
            state.RetrySuccessCount.Should().Be(1);
            state.LastFailureTimestamp.Should().BeAfter(DateTimeOffset.UtcNow.AddSeconds(-1.0));
            state.Reset();
            state.CurrentFailureCount.Should().Be(0);
            state.RetrySuccessCount.Should().Be(0);
            state.LastFailureTimestamp.Should().Be(DateTimeOffset.MaxValue.ToUniversalTime());
        }

        [Fact]
        public void GetStatus()
        {
            var settings = new CircuitBreakerSetting("test", 1, TimeSpan.FromMilliseconds(50),1, 1);
            var state = new CircuitBreakerState();
            state.GetStatus(settings).Should().Be(CircuitBreakerStatus.Closed);
            state.IncrementFailureCount();
            state.GetStatus(settings).Should().Be(CircuitBreakerStatus.Open);
            Thread.Sleep(60);
            state.GetStatus(settings).Should().Be(CircuitBreakerStatus.HalfOpen);
            state.IncrementFailureCount();
            state.GetStatus(settings).Should().Be(CircuitBreakerStatus.Open);
            Thread.Sleep(60);
            state.GetStatus(settings).Should().Be(CircuitBreakerStatus.HalfOpen);
            state.IncrementRetrySuccessCount();
            state.GetStatus(settings).Should().Be(CircuitBreakerStatus.Closed);
            state.CurrentFailureCount.Should().Be(0);
            state.RetrySuccessCount.Should().Be(0);
            state.LastFailureTimestamp.Should().Be(DateTimeOffset.MaxValue.ToUniversalTime());
        }
    }
}
