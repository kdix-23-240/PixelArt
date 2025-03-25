using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeObject : MonoBehaviour, IPooledObject<CubeObject>
{
    private IObjectPool<CubeObject> _objectPool;
    public IObjectPool<CubeObject> ObjectPool
    {
        set => _objectPool = value;
    }

    public void Initialize()
    {
        StartCoroutine(DelayDeactivation(3f));
    }

    private IEnumerator DelayDeactivation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Deactivate();
    }

    public void Deactivate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        _objectPool.Release(this);
    }
}
