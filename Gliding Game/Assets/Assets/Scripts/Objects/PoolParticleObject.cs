using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PoolParticleObject : PoolObject
{
    private float CloseTime;

    private void Start()
    {
        CloseTime = GetComponent<ParticleSystem>().main.duration;
    }
    private void Update()
    {
        if (CloseTime > 0)
        {
            CloseTime -= Time.deltaTime;
            if (CloseTime <= 0)
            {
                PoolManager.Instance.DeSpawn(this);
            }
        }
    }
    public override void OnDespawn()
    {
    }

    public override void OnSpawn()
    {
       
    }
}