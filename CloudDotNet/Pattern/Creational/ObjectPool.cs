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
        private readonly bool _areObjectsDisposable = typeof(IDisposable).IsAssignableFrom(typeof(T));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objectFactory">The method to create objects</param>
        /// <param name="objectSanitizer">The method to sanitize a object before returning it to the pool</param>
        public ObjectPool(Func<T> objectFactory, Action<T> objectSanitizer)
        {
            objectFactory.ThrowIfNull("objectFactory");
            objectSanitizer.ThrowIfNull("objectSanitizer");

            _pool = new ConcurrentBag<T>();
            _objectFactory = objectFactory;
            _objectSanitizer = objectSanitizer;
        }

        /// <summary>
        /// Rent a object from the pool
        /// </summary>
        /// <returns>Return a object from the pool or creates a new one if the pool is empty</returns>
        public T Rent()
        {
            T result;
            return _pool.TryTake(out result) ? result : _objectFactory();
        }

        /// <summary>
        /// Return the object to the pool after usage.
        /// The object will be sanitized before adding it to the pool.
        /// </summary>
        /// <param name="item">The object to return to the pool</param>
        public void Return(T item)
        {
            _objectSanitizer(item);
            _pool.Add(item);
        }

        /// <summary>
        /// The pool count
        /// </summary>
        /// <returns>The count of the objects in the pool</returns>
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
