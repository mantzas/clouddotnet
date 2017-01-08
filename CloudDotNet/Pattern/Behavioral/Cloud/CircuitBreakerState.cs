using System;
using System.Threading;

namespace CloudDotNet.Pattern.Behavioral.Cloud
{
    /// <summary>
    /// The Circuit Breaker State
    /// </summary>
    public class CircuitBreakerState
    {
        private static int usingResource = 0;
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
        /// The time stamp of the last failure
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
            SafeExecute(ref usingResource, InnerReset);
        }

        /// <summary>
        /// Incrementing the failure count
        /// </summary>
        public void IncrementFailureCount()
        {
            SafeExecute(ref usingResource, ()=> 
            {
                _currentFailureCount++;
                _lastFailureTimestamp = DateTimeOffset.UtcNow;
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
            return SafeExecute(ref usingResource, ()=>GetInnerStatus(setting));
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

        private CircuitBreakerStatus GetInnerStatus(CircuitBreakerSetting setting)
        {
            var status = CircuitBreakerStatus.Open;

            if (setting.FailureThreshold > _currentFailureCount)
            {
                status = CircuitBreakerStatus.Closed;
            }
            else
            {
                if (_lastFailureTimestamp + setting.RetryTimeout <= DateTimeOffset.UtcNow)
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
            _currentFailureCount = 0;
            _lastFailureTimestamp = DateTimeOffset.MaxValue.ToUniversalTime();
            _retrySuccessCount = 0;
        }

        private T SafeExecute<T>(ref int usingResource, Func<T> fun)
        {
            // Spin until we get a lock
            while (0 != Interlocked.Exchange(ref usingResource, 1))
            {
                Thread.Sleep(0);
            }

            var result = fun();
            Interlocked.Exchange(ref usingResource, 0);
            return result;
        }

        private void SafeExecute(ref int usingResource, Action act)
        {
            // Spin until we get a lock
            while (0 != Interlocked.Exchange(ref usingResource, 1))
            {
                Thread.Sleep(0);
            }

            act();
            Interlocked.Exchange(ref usingResource, 0);
        }
    }
}