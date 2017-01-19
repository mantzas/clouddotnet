using System;
using System.Threading;

namespace CloudDotNet.Threading
{
    public static class GatedExecution
    {
        public static T Execute<T>(ref int usingResource, Func<T> fun)
        {
            // Spin until we get a lock
            while (0 != Interlocked.Exchange(ref usingResource, 1))
            {
                Thread.Sleep(0);
            }

            try
            {
                return fun();
            }
            finally
            {
                Interlocked.Exchange(ref usingResource, 0);
            }
        }

        public static void Execute(ref int usingResource, Action act)
        {
            // Spin until we get a lock
            while (0 != Interlocked.Exchange(ref usingResource, 1))
            {
                Thread.Sleep(0);
            }

            try
            {
                act();
            }
            finally
            {
                Interlocked.Exchange(ref usingResource, 0);
            }
        }
    }
}