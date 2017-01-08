using CloudDotNet.Pattern.Behavioral.Cloud;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CloudDotNet.Tests.Unit.Pattern.Behavioral.Cloud
{
    public class CircuitBreakerTests
    {
        private readonly ITestOutputHelper _output;

        public CircuitBreakerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Constructor_NullLogger_Throws()
        {
            Action act = () => new CircuitBreaker(null ,null, null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_NullProvider_Throws()
        {
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            Action act = () => new CircuitBreaker(logger, null, null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_NullKeys_Throws()
        {
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            Action act = () => new CircuitBreaker(logger, provider, null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_EmptyKeys_Throws()
        {
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            Action act = () => new CircuitBreaker(logger, provider, new string[0]);
            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Execute_KeyNull_Throws()
        {
            const string Key = "KEY";
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(Key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            var circuit = new CircuitBreaker(logger, provider, new[] { Key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync<string>(null, null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Execute_FuncNull_Throws()
        {
            const string Key = "KEY";
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(Key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            var circuit = new CircuitBreaker(logger, provider, new[] { Key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync<string>(Key, null).ConfigureAwait(false);
            func.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public async Task Execute_CircuitClosedAsync()
        {
            const string Key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(Key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(Key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { Key });
            var response = await circuit.ExecuteAsync(Key, ReturnSuccess).ConfigureAwait(false);
            response.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Execute_FuncThrows()
        {
            const string Key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(Key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(Key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { Key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync(Key, ReturnThrows).ConfigureAwait(false);
            func.ShouldThrow<Exception>();
        }

        [Fact]
        public void Execute_CircuitOpen()
        {
            const string Key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(Key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(Key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { Key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync(Key, ReturnThrows).ConfigureAwait(false);
            func.ShouldThrow<Exception>();
            func.ShouldThrow<CircuitBreakerOpenException>();
            func.ShouldThrow<CircuitBreakerOpenException>();
        }

        [Fact]
        public async Task Execute_CircuitHalfOpenAsync()
        {
            const string Key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(Key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(Key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { Key });
            Func<Task<string>> funcThrows = async () => await circuit.ExecuteAsync(Key, ReturnThrows).ConfigureAwait(false);
            funcThrows.ShouldThrow<Exception>();
            funcThrows.ShouldThrow<CircuitBreakerOpenException>();
            await Task.Delay(30).ConfigureAwait(false);
            var response = await circuit.ExecuteAsync(Key, ReturnSuccess).ConfigureAwait(false);
            response.Should().BeNullOrEmpty();
        }

        private Task<string> ReturnSuccess()
        {
            return Task.FromResult(string.Empty);
        }

        private Task<string> ReturnThrows()
        {
            throw new Exception("EXCEPTION");
        }

        private Task<string> ReturnSuccessDelayed(int sleep)
        {
            Thread.Sleep(sleep);
            return Task.FromResult(string.Empty);
        }

        private class TestLogger : ILogger<CircuitBreaker>
        {
            private readonly ITestOutputHelper _output;

            public TestLogger(ITestOutputHelper output)
            {
                _output = output;
            }

            IDisposable ILogger.BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }

            bool ILogger.IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                _output.WriteLine(state.ToString());
            }
        }
    }
}
