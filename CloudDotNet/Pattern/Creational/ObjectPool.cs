using System;
using System.Collections.Generic;
using System.Reflection;
using CloudDotNet.Threading;
using ValidDotNet;

namespace CloudDotNet.Pattern.Creational
{
    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        private int _usingResource;
        private readonly Stack<T> _pool;
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

            _pool = new Stack<T>(poolSize);
            _objectFactory = objectFactory;
            _objectSanitizer = objectSanitizer;
            _areObjectsDisposable = typeof(IDisposable).IsAssignableFrom(typeof(T));
            _poolSize = poolSize;
            _log = log;
        }

        public T Borrow()
        {
            return GatedExecution.Execute(ref _usingResource, () => _pool.Count == 0 ? _objectFactory() : _pool.Pop());
        }

        public void Return(T item)
        {
            GatedExecution.Execute(ref _usingResource, () =>
            {
                if (_pool.Count >= _poolSize)
                {
                    _log?.Invoke(string.Format("Maximum pool size [{0}] reached.", _poolSize));
                    return;
                }

                _objectSanitizer?.Invoke(item);
                _pool.Push(item);
            });
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
                    var item = _pool.Pop();
                    ((IDisposable)item).Dispose();
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
