using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeObject : MonoBehaviour, IPooledObject<CubeObject>
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    private Color _color;
    public Color Color
    {
        get => _color;
        set {
            _color = value;
            GetComponent<Renderer>().material.color = _color;
        }
    }
    private IObjectPool<CubeObject> _objectPool;
    public IObjectPool<CubeObject> ObjectPool
    {
        set => _objectPool = value;
    }

    public void Initialize(int x, int y)
    {
        Activate(x, y);
    }

    public void Activate(int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    public void Deactivate()
    {
        if (!gameObject.activeSelf) // すでに非アクティブならリリースしない
        {
            // Debug.LogWarning("このオブジェクトはすでにプールに戻されています: " + this.name);
            return;
        }

        // Debug.Log("通過：Deactivate");
        gameObject.SetActive(false); // オブジェクトを非表示にする
        _objectPool.Release(this);   // プールに戻す
    }
}
