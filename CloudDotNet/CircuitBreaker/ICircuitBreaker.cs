using System;
using System.Threading.Tasks;

namespace CloudDotNet.CircuitBreaker
{
    /// <summary>
    /// The Circuit Breaker interface
    /// </summary>
    public interface ICircuitBreaker
    {
        /// <summary>
        /// Execution of the func
        /// </summary>
        /// <typeparam name="T">The return parameter of the func</typeparam>
        /// <param name="key">The state key to use</param>
        /// <param name="func">The func to be executed</param>
        /// <returns>The return parameter of the func</returns>
        Task<T> ExecuteAsync<T>(string key, Func<Task<T>> func);
    }
}
