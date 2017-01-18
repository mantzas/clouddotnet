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
            _emptyPool = new ObjectPool<Test>(Create, Sanitize);
            _fullPool = new ObjectPool<Test>(Create, Sanitize);
            for (var i = 0; i < 100000; i++)
            {
                _fullPool.Return(new Test());
            }            
        }

        [Benchmark(Baseline = true, Description = "Empty pool Rent - Return")]
        public Test EmptyPoolRentReturn()
        {
            var item = _emptyPool.Rent();
            item.Name = "Benchmark";
            _emptyPool.Return(item);
            return item;
        }

        [Benchmark(Description = "Full pool Rent - Return")]
        public Test FullPoolRentReturn()
        {
            var item = _fullPool.Rent();
            item.Name = "Benchmark";
            _fullPool.Return(item);
            return item;
        }

        [Benchmark(Description = "Full pool Rent - Return 10000")]
        public Test FullPoolRentReturn10000()
        {
            Test item = null;

            for (var i = 0; i < 10000; i++)
            {
                item = _fullPool.Rent();
                _fullPool.Return(item);
            }

            return item;
        }

        public class Config : ManualConfig
        {
            public Config()
            {
                Add(MarkdownExporter.GitHub);
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

            #region IDisposable Support

            private bool _disposedValue; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (_disposedValue)
                {
                    return;
                }

                if (disposing)
                {
                }
                _disposedValue = true;
            }

            void IDisposable.Dispose()
            {
                Dispose(true);
            }

            #endregion
        }

        private static Test Create() => new Test { Name = "Test" };

        private static void Sanitize(Test test) => test.Name = null;
    }
}