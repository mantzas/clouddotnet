using Microsoft.Extensions.Logging;
using ValidDotNet;

namespace CloudDotNet.Pattern.Behavioral.Cloud;

/// <summary>
/// Circuit Breaker Pattern
/// </summary>
public class CircuitBreaker : ICircuitBreaker
{
    private readonly ICircuitBreakerSettingsProvider _settingsProvider;
    private readonly Dictionary<string, CircuitBreakerState> _states;
    private readonly ILogger<CircuitBreaker> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">The logger</param>
    /// <param name="settingsProvider">The settings provider implementation</param>
    /// <param name="keys">The keys for each state used</param>
    public CircuitBreaker(ILogger<CircuitBreaker> logger, ICircuitBreakerSettingsProvider settingsProvider,
        string[] keys)
    {
        logger.ThrowIfNull(nameof(logger));
        settingsProvider.ThrowIfNull(nameof(settingsProvider));
        keys.ThrowIfNullOrEmpty(nameof(keys));
        _logger = logger;
        _settingsProvider = settingsProvider;
        _states = keys.ToDictionary(p => p, _ => new CircuitBreakerState());
    }

    /// <summary>
    /// Execution of the func
    /// </summary>
    /// <typeparam name="T">The return parameter of the func</typeparam>
    /// <param name="key">The state key to use</param>
    /// <param name="func">The func to be executed</param>
    /// <returns>The return parameter of the func</returns>
    public async Task<T> ExecuteAsync<T>(string key, Func<Task<T>> func)
    {
        key.ThrowIfNullOrWhitespace(nameof(key));
        func.ThrowIfNull(nameof(func));

        var setting = await _settingsProvider.GetAsync(key).ConfigureAwait(false);
        var state = _states[key];
        var status = state.GetStatus(setting);

        _logger.LogInformation("Entry. {0}", state);

        if (status == CircuitBreakerStatus.Open)
        {
            _logger.LogWarning("Open! {0}", state);
            throw new CircuitBreakerOpenException();
        }

        try
        {
            state.IncreaseExecutions();

            var result = await func().ConfigureAwait(false);

            if (status != CircuitBreakerStatus.HalfOpen)
            {
                return result;
            }

            state.IncrementRetrySuccessCount();
            _logger.LogInformation("Half open. Retry successes incremented. {0}", state);
            return result;
        }
        catch (Exception)
        {
            state.IncrementFailureCount();
            _logger.LogWarning("Exception occurred while executing. Failures incremented. {0}", state);
            throw;
        }
        finally
        {
            state.DecreaseExecutions();
            _logger.LogInformation("Exit. {0}", state);
        }
    }
}