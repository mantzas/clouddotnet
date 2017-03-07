using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using CloudDotNet.Pattern.Creational;
using System;

namespace CloudDotNet.Benchmark.Pattern.Creational
{
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
                Add(StatisticColumn.Mean);
                Add(StatisticColumn.StdErr);
                Add(StatisticColumn.StdDev);
                Add(StatisticColumn.OperationsPerSecond);
                Add(StatisticColumn.Min);
                Add(StatisticColumn.Q1);
                Add(StatisticColumn.Median);
                Add(StatisticColumn.Q3);
                Add(StatisticColumn.Max);
                Add(StatisticColumn.P0);
                Add(StatisticColumn.P25);
                Add(StatisticColumn.P50);
                Add(StatisticColumn.P80);
                Add(StatisticColumn.P85);
                Add(StatisticColumn.P90);
                Add(StatisticColumn.P95);
                Add(StatisticColumn.P100);
            }
        }

        public class Test : IDisposable
        {
            public string Name { get; set; }


            void IDisposable.Dispose()
            {
            }
        }

        private static Test Create()
        {
            return new Test { Name = "Test" };
        }
    }
}