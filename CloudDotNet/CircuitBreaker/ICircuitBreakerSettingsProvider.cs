using System.Threading.Tasks;

namespace CloudDotNet.CircuitBreaker
{
    /// <summary>
    /// A Circuit Breaker Settings Provider interface
    /// </summary>
    public interface ICircuitBreakerSettingsProvider
    {
        /// <summary>
        /// Save setting
        /// </summary>
        /// <param name="setting">The setting to save</param>
        Task SaveAsync(CircuitBreakerSetting setting);

        /// <summary>
        /// Get setting async
        /// </summary>
        /// <param name="key">The key of the setting to get</param>
        /// <returns>The setting</returns>
        Task<CircuitBreakerSetting> GetAsync(string key);
    }
}
