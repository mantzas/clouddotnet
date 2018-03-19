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
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_NullObjectFactory_Throws()
        {
            Action act = () => new ObjectPool<string>(1000, null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_full_Success()
        {
            new ObjectPool<string>(1000, () => string.Empty, (message) =>
            {
            }, (message) =>
            {
            }).Should().NotBeNull();
        }

        [Fact]
        public void Constructor_missing_log_Success()
        {
            new ObjectPool<string>(1000, () => string.Empty, (message) =>
            {
            }).Should().NotBeNull();
        }

        [Fact]
        public void Constructor_missing_log_sanitizer_Success()
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

        private sealed class Test : IDisposable
        {
            public string Name { get; set; }

            void IDisposable.Dispose()
            {
                // Method intentionally left empty.
            }
        }

        private static Test Create() => new Test { Name = "Test" };

        private static ObjectPool<Test> CreatePool(int poolSize = 1000)
        {
            return new ObjectPool<Test>(poolSize, Create);
        }
    }
}
