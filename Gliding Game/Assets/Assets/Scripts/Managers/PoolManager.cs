using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private List<PoolItem<PoolObject>> poolableObjects = new List<PoolItem<PoolObject>>();

    private Dictionary<PoolObjectType, IObjectPool<PoolObject>> mPool;

    [Serializable]
    public class PoolItem<T> where T : PoolObject
    {
        public GameObject Prefab;
    }

    public override void Initialize()
    {
        base.Initialize();
        InstantiatePool();
    }

    private void InstantiatePool()
    {
        mPool = DictionaryPool<PoolObjectType, IObjectPool<PoolObject>>.Get();

        poolableObjects.ForEach(
            item =>
            {
                CreateObjectsPool(item);
                InitializeObjectsPool(item, 0);
            }
        );
    }

    private void CreateObjectsPool<T>(PoolItem<T> item) where T : PoolObject
    {
        if (!item.Prefab)
        {
            throw new Exception("Pool Item prefab is null");
        }

        var type = item.Prefab.GetComponent<T>().PoolType;

        mPool.Add(
            type,
            new ObjectPool<PoolObject>(
                () => CreatePooledItem(item.Prefab),
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPoolObject,
                true,
                1,
                10
            )
        );
    }


    private void InitializeObjectsPool<T>(PoolItem<T> item, int count) where T : PoolObject
    {
        if (!item.Prefab)
        {
            throw new Exception("Pool Item prefab is null");
        }
        var type = item.Prefab.GetComponent<T>().PoolType;
        if (!mPool.TryGetValue(type, out var objectPool))
        {
            CreateObjectsPool(item);
            objectPool = mPool[type];
        }
        for (var i = 0; i < count; ++i)
        {
            objectPool.Get();
        }
    }
    private PoolObject CreatePooledItem(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        go.name = prefab.name;

        var pooledGO = go.GetComponent<PoolObject>();
        return pooledGO;
    }
    private void OnGetFromPool(PoolObject pO)
    {
        SetActivePoolObject(pO, true);
        pO.OnSpawn();
    }
    private void OnReleaseToPool(PoolObject pO)
    {
        pO.gameObject.transform.SetParent(transform);
        SetActivePoolObject(pO, false);
        pO.OnDespawn();
    }
    private void SetActivePoolObject(PoolObject pO, bool active)
    {
        pO.gameObject.SetActive(active);
    }
    private void OnDestroyPoolObject(PoolObject destroyedGO)
    {
        Destroy(destroyedGO.gameObject);
    }

    public PoolObject Spawn(PoolObjectType type, Transform parent = null)
    {
        if (mPool.TryGetValue(type, out var objectPool))
        {
            var pooledGO = objectPool.Get();
            if (parent)
            {
                pooledGO.gameObject.transform.SetParent(parent);
            }

            return pooledGO;
        }
        else
        {
            throw new Exception("Pool Type not found!");
        }
    }

    public void DeSpawn(PoolObject pO)
    {
        mPool[pO.PoolType].Release(pO);
    }
}
public enum PoolObjectType
{
    Ground = 0,
    Jumper = 1,
    DoubleJumper = 2,
    SmokeVFX = 3,
}