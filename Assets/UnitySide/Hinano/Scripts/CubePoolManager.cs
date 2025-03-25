using UnityEngine;

public class CubePoolManager : PoolManager<CubeObject>
{
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_objectPool == null)
            {
                return;
            }
            
            var cubeObject = _objectPool.Get();

            if (cubeObject == null)
            {
                return;
            }
            
            cubeObject.transform.position = transform.position;
            cubeObject.Initialize();
        }
    }
}
