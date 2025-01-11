public abstract class PoolObject : CustomBehaviour
{
    public PoolObjectType PoolType;
    public abstract void OnDespawn();
    public abstract void OnSpawn();
}