using CloudDotNet.Pattern.Behavioral.Cloud;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
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
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_NullProvider_Throws()
        {
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            Action act = () => new CircuitBreaker(logger, null, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_NullKeys_Throws()
        {
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            Action act = () => new CircuitBreaker(logger, provider, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_EmptyKeys_Throws()
        {
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            Action act = () => new CircuitBreaker(logger, provider, new string[0]);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Execute_KeyNull_Throws()
        {
            const string key = "KEY";
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var circuit = new CircuitBreaker(logger, provider, new[] { key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync<string>(null, null).ConfigureAwait(false);
            func.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Execute_FuncNull_Throws()
        {
            const string key = "KEY";
            var logger = Substitute.For<ILogger<CircuitBreaker>>();
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var circuit = new CircuitBreaker(logger, provider, new[] { key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync<string>(key, null).ConfigureAwait(false);
            func.Should().Throw<ArgumentException>();
        }

        [Fact]
        public async Task Execute_CircuitClosedAsync()
        {
            const string key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { key });
            var response = await circuit.ExecuteAsync(key, ReturnSuccess).ConfigureAwait(false);
            response.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Execute_FuncThrows()
        {
            const string key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync(key, ReturnThrows).ConfigureAwait(false);
            func.Should().Throw<Exception>();
        }

        [Fact]
        public void Execute_CircuitOpen()
        {
            const string key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { key });
            Func<Task<string>> func = async () => await circuit.ExecuteAsync(key, ReturnThrows).ConfigureAwait(false);
            func.Should().Throw<Exception>();
            func.Should().Throw<CircuitBreakerOpenException>();
            func.Should().Throw<CircuitBreakerOpenException>();
        }

        [Fact]
        public async Task Execute_CircuitHalfOpenAsync()
        {
            const string key = "KEY";
            var provider = Substitute.For<ICircuitBreakerSettingsProvider>();
            var setting = new CircuitBreakerSetting(key, 1, TimeSpan.FromMilliseconds(20), 1, 1);
            provider.GetAsync(key).Returns(Task.FromResult(setting));
            var circuit = new CircuitBreaker(new TestLogger(_output), provider, new[] { key });
            Func<Task<string>> funcThrows = async () => await circuit.ExecuteAsync(key, ReturnThrows).ConfigureAwait(false);
            funcThrows.Should().Throw<Exception>();
            funcThrows.Should().Throw<CircuitBreakerOpenException>();
            await Task.Delay(30).ConfigureAwait(false);
            var response = await circuit.ExecuteAsync(key, ReturnSuccess).ConfigureAwait(false);
            response.Should().BeNullOrEmpty();
        }

        private static Task<string> ReturnSuccess()
        {
            return Task.FromResult(string.Empty);
        }

        private static Task<string> ReturnThrows()
        {
            throw new Exception("EXCEPTION");
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
