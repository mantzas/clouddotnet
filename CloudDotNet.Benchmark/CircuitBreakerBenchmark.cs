using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using CloudDotNet.CircuitBreaker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CloudDotNet.Benchmark
{
    [Config(typeof(Config))]
    public class CircuitBreakerBenchmark
    {
        private readonly CircuitBreaker.CircuitBreaker _breaker;

        public CircuitBreakerBenchmark()
        {
            _breaker = new CircuitBreaker.CircuitBreaker(new Logger(), new Provider(), new string[] { "KEY" });
        }

        [Benchmark]
        public string Execute()
        {
            Parallel.For(0, 1000, async (i, s) => await _breaker.ExecuteAsync("KEY", () => Task.FromResult(string.Empty)));

            return string.Empty;
        }

        private class Provider : ICircuitBreakerSettingsProvider
        {
            private CircuitBreakerSetting _setting;

            public Provider()
            {
                _setting = new CircuitBreakerSetting("KEY", 3, TimeSpan.FromMilliseconds(1000), 3, 3);
            }

            public Task<CircuitBreakerSetting> GetAsync(string key)
            {
                return Task.FromResult(_setting);
            }

            public Task SaveAsync(CircuitBreakerSetting setting)
            {
                _setting = setting;
                return Task.CompletedTask;
            }
        }

        private class Logger : ILogger<CircuitBreaker.CircuitBreaker>
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return false;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
            }
        }

        public class Config : ManualConfig
        {
            public Config()
            {
                Add(MarkdownExporter.GitHub);
                Add(StatisticColumn.Mean);
                Add(StatisticColumn.StdErr);
                Add(StatisticColumn.StdDev);
                Add(StatisticColumn.OperationsPerSecond);
                Add(StatisticColumn.Min);
                Add(StatisticColumn.Q1);
                Add(StatisticColumn.Median);
                Add(StatisticColumn.Q3);
                Add(StatisticColumn.Max);
                Add(StatisticColumn.P0);
                Add(StatisticColumn.P25);
                Add(StatisticColumn.P50);
                Add(StatisticColumn.P80);
                Add(StatisticColumn.P85);
                Add(StatisticColumn.P90);
                Add(StatisticColumn.P95);
                Add(StatisticColumn.P100);
            }
        }
    }
}
