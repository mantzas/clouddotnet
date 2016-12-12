using BenchmarkDotNet.Running;

namespace CloudDotNet.Benchmark
{
    public static class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<CircuitBreakerBenchmark>();
        }
    }
}
