using System;
using System.Threading;

namespace CloudDotNet.CircuitBreaker
{
    /// <summary>
    /// The Circuit Breaker State
    /// </summary>
    public class CircuitBreakerState
    {
        private static readonly object _syncLock = new object();
        private int _currentFailureCount;
        private int _retrySuccessCount;
        private int _currentExecutions;
        private DateTimeOffset _lastFailureTimestamp;

        /// <summary>
        /// The current failure count
        /// </summary>
        public int CurrentFailureCount
        {
            get { return _currentFailureCount; }
        }

        /// <summary>
        /// The retry success count
        /// </summary>
        public int RetrySuccessCount
        {
            get { return _retrySuccessCount; }
        }

        /// <summary>
        /// The timestamp of the last failure
        /// </summary>
        public DateTimeOffset LastFailureTimestamp
        {
            get { return _lastFailureTimestamp; }
        }

        /// <summary>
        /// The active execution count
        /// </summary>
        public int CurrentExecutions
        {
            get { return _currentExecutions; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CircuitBreakerState()
        {
            Reset();
        }

        /// <summary>
        /// Reseting the state
        /// </summary>
        public void Reset()
        {
            lock (_syncLock)
            {
                _currentFailureCount = 0;
                _lastFailureTimestamp = DateTimeOffset.MaxValue.ToUniversalTime();
                _retrySuccessCount = 0;
            }
        }

        /// <summary>
        /// Incrementing the failure count
        /// </summary>
        public void IncrementFailureCount()
        {
            lock (_syncLock)
            {
                _currentFailureCount++;
                _lastFailureTimestamp = DateTimeOffset.UtcNow;
            }
        }

        /// <summary>
        /// Incrementing the retry success count
        /// </summary>
        public void IncrementRetrySuccessCount()
        {
            Interlocked.Increment(ref _retrySuccessCount);
        }

        /// <summary>
        /// Decrease the active execution count
        /// </summary>
        public void IncreaseExecutions()
        {
            Interlocked.Increment(ref _currentExecutions);
        }

        /// <summary>
        /// Increase the active execution count
        /// </summary>
        public void DecreaseExecutions()
        {
            Interlocked.Decrement(ref _currentExecutions);
        }

        /// <summary>
        /// Getting the status of the circuit
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public CircuitBreakerStatus GetStatus(CircuitBreakerSetting setting)
        {
            lock (_syncLock)
            {
                if (setting.FailureThreshold > _currentFailureCount)
                {
                    return CircuitBreakerStatus.Closed;
                }

                if (_lastFailureTimestamp + setting.RetryTimeout <= DateTimeOffset.UtcNow)
                {
                    if (_retrySuccessCount >= setting.RetrySuccessThreshold)
                    {
                        Reset();
                        return CircuitBreakerStatus.Closed;
                    }

                    if (_currentExecutions > setting.MaxRetryExecutionThreshold)
                    {
                        return CircuitBreakerStatus.Open;
                    }

                    return CircuitBreakerStatus.HalfOpen;
                }

                return CircuitBreakerStatus.Open;
            }
        }

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>A formated string of the state</returns>
        public override string ToString()
        {
            return string.Format("Circuit State: Executions={0} Failures={1} LastFailure={2} RetrySuccesses={3}",
                                 _currentExecutions, _currentFailureCount, _lastFailureTimestamp.ToString("o"),
                                 RetrySuccessCount);
        }
    }
}