﻿using ValidDotNet;

namespace CloudDotNet.Pattern.Behavioral.Cloud;

/// <summary>
/// Circuit Breaker Settings
/// </summary>
public class CircuitBreakerSetting
{
    /// <summary>
    /// The key for this setting
    /// </summary>
    public string Key { get; }
    /// <summary>
    /// The threshold for the circuit to open
    /// </summary>
    public int FailureThreshold { get; }
    /// <summary>
    /// The timeout after which we set the state to half-open and allow a retry
    /// </summary>
    public TimeSpan RetryTimeout { get; }
    /// <summary>
    /// The threshold of the retry successes which returns the state to open
    /// </summary>
    public int RetrySuccessThreshold { get; }
    /// <summary>
    /// The threshold of how many retry executions are allowed when the status is half-open
    /// </summary>
    public int MaxRetryExecutionThreshold { get; }

    public CircuitBreakerSetting(string key, int failureThreshold,
        TimeSpan retryTimeout, int retrySuccessThreshold,
        int maxRetryExecutionThreshold)
    {
        key.ThrowIfNullOrWhitespace(nameof(key));
        failureThreshold.ThrowIfLessOrEqual(nameof(failureThreshold), 0);
        retryTimeout.ThrowIfZero(nameof(retryTimeout));
        retrySuccessThreshold.ThrowIfLessOrEqual(nameof(retrySuccessThreshold), 0);
        maxRetryExecutionThreshold.ThrowIfLessOrEqual(nameof(maxRetryExecutionThreshold), 0);

        Key = key;
        FailureThreshold = failureThreshold;
        RetryTimeout = retryTimeout;
        RetrySuccessThreshold = retrySuccessThreshold;
        MaxRetryExecutionThreshold = maxRetryExecutionThreshold;
    }
}