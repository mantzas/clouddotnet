using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CloudDotNet.CircuitBreaker
{
    /// <summary>
    /// A Circuit Breaker setting provider based on a concurrent dictionary
    /// </summary>
    public class LocalCircuitBreakerSettingsProvider : ICircuitBreakerSettingsProvider
    {
        private readonly ConcurrentDictionary<string, CircuitBreakerSetting> _store;

        public LocalCircuitBreakerSettingsProvider()
        {
            _store = new ConcurrentDictionary<string, CircuitBreakerSetting>();
        }

        public Task<CircuitBreakerSetting> GetAsync(string key)
        {
            return Task.FromResult(_store[key]);
        }

        public Task SaveAsync(CircuitBreakerSetting setting)
        {
            _store[setting.Key] = setting;
            return Task.CompletedTask;
        }
    }
}
