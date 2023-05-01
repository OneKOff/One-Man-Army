
public interface IPooledObject
{
    public IPooledObject GetFromObjectPool();
    public void ReturnToObjectPool();
}
