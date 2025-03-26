using UnityEngine.Pool;

public interface IPooledObject<T> where T : class
{
    public IObjectPool<T> ObjectPool { set; }
    public void Initialize(int x, int y);
    public void Deactivate();
}
