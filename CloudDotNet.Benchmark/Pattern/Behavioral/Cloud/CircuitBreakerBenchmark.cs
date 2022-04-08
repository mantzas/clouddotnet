using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using CloudDotNet.Pattern.Behavioral.Cloud;
using Microsoft.Extensions.Logging;

namespace CloudDotNet.Benchmark.Pattern.Behavioral.Cloud;

[Config(typeof(Config))]
public class CircuitBreakerBenchmark
{
    private readonly CircuitBreaker _breaker;

    public CircuitBreakerBenchmark()
    {
        _breaker = new CircuitBreaker(new Logger(), new Provider(), new [] { "KEY" });
    }

    [Benchmark(Baseline = true, Description = "Sequential")]
    public Task<string> ExecuteSingle()
    {
        return _breaker.ExecuteAsync("KEY", () => Task.FromResult(string.Empty));
    }

    [Benchmark(Description = "Parallel")]
    public string ExecuteParallel()
    {
        Parallel.For(0, 1000, async (_, _) => await _breaker.ExecuteAsync("KEY", () => Task.FromResult(string.Empty)).ConfigureAwait(false));

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

    private class Logger : ILogger<CircuitBreaker>
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
            AddColumn(StatisticColumn.Mean);
            AddColumn(StatisticColumn.StdErr);
            AddColumn(StatisticColumn.StdDev);
            AddColumn(StatisticColumn.OperationsPerSecond);
            AddColumn(StatisticColumn.Min);
            AddColumn(StatisticColumn.Q1);
            AddColumn(StatisticColumn.Median);
            AddColumn(StatisticColumn.Q3);
            AddColumn(StatisticColumn.Max);
            AddColumn(StatisticColumn.P0);
            AddColumn(StatisticColumn.P25);
            AddColumn(StatisticColumn.P50);
            AddColumn(StatisticColumn.P80);
            AddColumn(StatisticColumn.P85);
            AddColumn(StatisticColumn.P90);
            AddColumn(StatisticColumn.P95);
            AddColumn(StatisticColumn.P100);
        }
    }
}