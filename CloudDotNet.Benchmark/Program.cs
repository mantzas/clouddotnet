using BenchmarkDotNet.Running;
using CloudDotNet.Benchmark.Pattern.Behavioral.Cloud;
using CloudDotNet.Benchmark.Pattern.Creational;

namespace CloudDotNet.Benchmark
{
    public static class Program
    {
        public static void Main()
        {
            var circuitSummary = BenchmarkRunner.Run<CircuitBreakerBenchmark>();
            var poolSummary = BenchmarkRunner.Run<ObjectPoolBenchmark>();
        }
    }
}
