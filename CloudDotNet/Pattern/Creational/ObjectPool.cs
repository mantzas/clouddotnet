using System;
using System.Collections.Concurrent;
using System.Reflection;
using ValidDotNet;

namespace CloudDotNet.Pattern.Creational
{
    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        private readonly ConcurrentBag<T> _pool;
        private readonly Func<T> _objectFactory;
        private readonly Action<T> _objectSanitizer;
        private readonly bool _areObjectsDisposable;
        private readonly int _poolSize;
        private readonly Action<string> _log;

        public ObjectPool(int poolSize, Func<T> objectFactory) 
            : this(poolSize, objectFactory, null, null)
        {
        }

        public ObjectPool(int poolSize, Func<T> objectFactory, Action<string> log)
            : this(poolSize, objectFactory, null, log)
        {
        }

        public ObjectPool(int poolSize, Func<T> objectFactory, Action<T> objectSanitizer, Action<string> log)
        {
            poolSize.ThrowIfLessOrEqual("poolSize", 0);
            objectFactory.ThrowIfNull("objectFactory");

            _pool = new ConcurrentBag<T>();
            _objectFactory = objectFactory;
            _objectSanitizer = objectSanitizer;
            _areObjectsDisposable = typeof(IDisposable).IsAssignableFrom(typeof(T));
            _poolSize = poolSize;
            _log = log;
        }

        public T Borrow()
        {
            T result;
            return _pool.TryTake(out result) ? result : _objectFactory();
        }

        public void Return(T item)
        {
            if (_pool.Count >= _poolSize)
            {
                _log?.Invoke(string.Format("Maximum pool size [{0}] reached.", _poolSize));
                return;
            }

            _objectSanitizer?.Invoke(item);
            _pool.Add(item);
        }

        public int Count => _pool.Count;

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing && _areObjectsDisposable)
            {
                while (_pool.Count > 0)
                {
                    T item;

                    if (_pool.TryTake(out item))
                    {
                        ((IDisposable)item).Dispose();
                    }
                }
            }

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
