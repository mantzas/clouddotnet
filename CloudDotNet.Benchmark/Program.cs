using BenchmarkDotNet.Running;
using CloudDotNet.Benchmark.Pattern.Behavioral.Cloud;
using CloudDotNet.Benchmark.Pattern.Creational;

namespace CloudDotNet.Benchmark
{
    public static class Program
    {
        public static void Main()
        {
            var switcher = new BenchmarkSwitcher(new[] {
                typeof(CircuitBreakerBenchmark),
                typeof(ObjectPoolBenchmark)
            });
            switcher.Run();
        }
    }
}
