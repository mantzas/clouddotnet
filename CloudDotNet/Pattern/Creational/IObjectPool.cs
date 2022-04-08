namespace CloudDotNet.Pattern.Creational;

public interface IObjectPool<T> where T:class
{
    int Count { get; }
    T Borrow();
    void Return(T item);        
}