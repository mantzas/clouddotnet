using CloudDotNet.Pattern.Creational;
using FluentAssertions;
using System;
using Xunit;

namespace CloudDotNet.Tests.Unit.Pattern.Creational
{
    public class ObjectPoolTests
    {
        [Fact]
        public void Constructor_PoolSizeInvalid_Throws()
        {
            Action act = () => new ObjectPool<string>(0, null);
            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Constructor_NullObjectFactory_Throws()
        {
            Action act = () => new ObjectPool<string>(1000, null);
            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_full_Success()
        {
            new ObjectPool<string>(1000, () => string.Empty, (message) => { }, (message) => { }).Should().NotBeNull();
        }

        [Fact]
        public void Constructor_missin_log_Success()
        {
            new ObjectPool<string>(1000, () => string.Empty, (message) => { }).Should().NotBeNull();
        }

        [Fact]
        public void Constructor_missin_log_sanitizer_Success()
        {
            new ObjectPool<string>(1000, () => string.Empty).Should().NotBeNull();
        }

        [Fact]
        public void Rent_OnEmpty_ReturnsNew()
        {
            var pool = CreatePool();
            pool.Count.Should().Be(0);
            var item = pool.Borrow();
            pool.Count.Should().Be(0);
            item.Should().NotBeNull();
            item.Name.Should().Be("Test");
        }

        [Fact]
        public void Return_Rent_Success()
        {
            var pool = CreatePool();
            pool.Count.Should().Be(0);
            pool.Return(new Test { Name = "Test" });
            pool.Count.Should().Be(1);
            var item = pool.Borrow();
            pool.Count.Should().Be(0);
            item.Should().NotBeNull();
            item.Name.Should().Be("Test");
        }

        [Fact]
        public void Return_Full_Success()
        {
            var pool = CreatePool(1);
            pool.Count.Should().Be(0);
            pool.Return(new Test { Name = "Test" });
            pool.Count.Should().Be(1);
            pool.Return(new Test { Name = "Test" });
            pool.Count.Should().Be(1);
        }

        [Fact]
        public void Dispose_Success()
        {
            var pool = CreatePool();
            pool.Count.Should().Be(0);
            pool.Return(new Test { Name = "Test" });
            pool.Count.Should().Be(1);
            pool.Dispose();
            pool.Count.Should().Be(0);
        }

        private class Test : IDisposable
        {
            public string Name { get; set; }

            #region IDisposable Support

            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                    }
                    disposedValue = true;
                }
            }

            void IDisposable.Dispose()
            {
                Dispose(true);
            }

            #endregion
        }

        private Test Create()
        {
            return new Test { Name = "Test"};
        }

        private void Sanitize(Test test)
        {
            test.Name = null;
        }

        private ObjectPool<Test> CreatePool(int poolSize = 1000)
        {
            return new ObjectPool<Test>(poolSize,() => Create());
        }
    }
}
