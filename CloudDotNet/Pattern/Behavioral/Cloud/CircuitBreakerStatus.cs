namespace CloudDotNet.Pattern.Behavioral.Cloud;

/// <summary>
/// Circuit Breaker Status
/// </summary>
public enum CircuitBreakerStatus
{
    /// <summary>
    /// Closed, allow execution
    /// </summary>
    Closed = 0,
    /// <summary>
    /// Half-Open, allowing execution to check if resource works again
    /// </summary>
    HalfOpen = 1,
    /// <summary>
    /// Open, disallowing execution
    /// </summary>
    Open = 2
}