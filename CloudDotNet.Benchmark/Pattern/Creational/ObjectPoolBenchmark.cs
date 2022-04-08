using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using CloudDotNet.Pattern.Creational;

namespace CloudDotNet.Benchmark.Pattern.Creational;

[Config(typeof(Config))]
public class ObjectPoolBenchmark
{
    private readonly ObjectPool<Test> _emptyPool;
    private readonly ObjectPool<Test> _fullPool;

    public ObjectPoolBenchmark()
    {
        _emptyPool = new ObjectPool<Test>(1000,Create);
        _fullPool = new ObjectPool<Test>(1000,Create);
        for (var i = 0; i < 500; i++)
        {
            _fullPool.Return(new Test());
        }            
    }

    [Benchmark(Baseline = true, Description = "Empty pool Rent - Return")]
    public Test EmptyPoolRentReturn()
    {
        var item = _emptyPool.Borrow();
        item.Name = "Benchmark";
        _emptyPool.Return(item);
        return item;
    }

    [Benchmark(Description = "Full pool Rent - Return")]
    public Test FullPoolRentReturn()
    {
        var item = _fullPool.Borrow();
        item.Name = "Benchmark";
        _fullPool.Return(item);
        return item;
    }

    public class Config : ManualConfig
    {
        public Config()
        {
            AddColumn(StatisticColumn.Mean);
            AddColumn(StatisticColumn.StdErr);
            AddColumn(StatisticColumn.StdDev);
            AddColumn(StatisticColumn.OperationsPerSecond);
            AddColumn(StatisticColumn.Min);
            AddColumn(StatisticColumn.Q1);
            AddColumn(StatisticColumn.Median);
            AddColumn(StatisticColumn.Q3);
            AddColumn(StatisticColumn.Max);
            AddColumn(StatisticColumn.P0);
            AddColumn(StatisticColumn.P25);
            AddColumn(StatisticColumn.P50);
            AddColumn(StatisticColumn.P80);
            AddColumn(StatisticColumn.P85);
            AddColumn(StatisticColumn.P90);
            AddColumn(StatisticColumn.P95);
            AddColumn(StatisticColumn.P100);
        }
    }

    public class Test : IDisposable
    {
        public string Name { get; set; } = "Test";
            
        void IDisposable.Dispose()
        {
        }
    }

    private static Test Create()
    {
        return new Test {  };
    }
}