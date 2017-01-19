using System;
using System.Threading;
using CloudDotNet.Threading;

namespace CloudDotNet.Pattern.Behavioral.Cloud
{
    /// <summary>
    /// The Circuit Breaker State
    /// </summary>
    public class CircuitBreakerState
    {
        private static int _usingResource;
        private int _retrySuccessCount;
        private int _currentExecutions;

        /// <summary>
        /// The current failure count
        /// </summary>
        public int CurrentFailureCount { get; private set; }

        /// <summary>
        /// The retry success count
        /// </summary>
        public int RetrySuccessCount => _retrySuccessCount;

        /// <summary>
        /// The time stamp of the last failure
        /// </summary>
        public DateTimeOffset LastFailureTimestamp { get; private set; }

        /// <summary>
        /// The active execution count
        /// </summary>
        public int CurrentExecutions => _currentExecutions;

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
            GatedExecution.Execute(ref _usingResource, InnerReset);
        }

        /// <summary>
        /// Incrementing the failure count
        /// </summary>
        public void IncrementFailureCount()
        {
            GatedExecution.Execute(ref _usingResource, ()=>
            {
                CurrentFailureCount++;
                LastFailureTimestamp = DateTimeOffset.UtcNow;
            });
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
            return GatedExecution.Execute(ref _usingResource, ()=>GetInnerStatus(setting));
        }
        
        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>A formated string of the state</returns>
        public override string ToString()
        {
            return string.Format("Circuit State: Executions={0} Failures={1} LastFailure={2} RetrySuccesses={3}",
                                 _currentExecutions, CurrentFailureCount, LastFailureTimestamp.ToString("o"),
                                 RetrySuccessCount);
        }

        private CircuitBreakerStatus GetInnerStatus(CircuitBreakerSetting setting)
        {
            var status = CircuitBreakerStatus.Open;

            if (setting.FailureThreshold > CurrentFailureCount)
            {
                status = CircuitBreakerStatus.Closed;
            }
            else
            {
                if (LastFailureTimestamp + setting.RetryTimeout <= DateTimeOffset.UtcNow)
                {
                    if (_retrySuccessCount >= setting.RetrySuccessThreshold)
                    {
                        InnerReset();
                        status = CircuitBreakerStatus.Closed;
                    }
                    else if (_currentExecutions > setting.MaxRetryExecutionThreshold)
                    {
                        status = CircuitBreakerStatus.Open;
                    }
                    else
                    {
                        status = CircuitBreakerStatus.HalfOpen;
                    }
                }
            }

            return status;
        }

        private void InnerReset()
        {
            CurrentFailureCount = 0;
            LastFailureTimestamp = DateTimeOffset.MaxValue.ToUniversalTime();
            _retrySuccessCount = 0;
        }
    }
}