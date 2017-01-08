namespace CloudDotNet.Pattern.Creational
{
    public interface IObjectPool<T> where T:class
    {
        int Count { get; }
        T Rent();
        void Return(T item);        
    }
}
