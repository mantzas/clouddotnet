using System;

namespace CloudDotNet.Pattern.Behavioral.Cloud
{
    /// <summary>
    /// Circuit Breaker Open Exception
    /// When thrown it defines a circuit breaker in open state
    /// </summary>
    public class CircuitBreakerOpenException : Exception
    {
        public CircuitBreakerOpenException()
        {
        }

        public CircuitBreakerOpenException(string message) : base(message)
        {
        }

        public CircuitBreakerOpenException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
