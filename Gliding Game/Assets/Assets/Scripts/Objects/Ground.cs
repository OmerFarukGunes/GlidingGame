using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Ground : PoolObject
{
    [SerializeField] private RandomPointGenerator randomPointGenerator;
    private List<PoolObject> mSpawnedJumperList = new List<PoolObject>();
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TAGS.PLAYER))
        {
            LevelManager.Instance.RemoveGroundInList(this);
        }
    }
    public void SpawnJumpers()
    {
        int count = Random.Range(10, 15);
        System.Random random = new System.Random();
        var selectedPointList = randomPointGenerator.PositionList.OrderBy(x => random.Next()).Take(count);
        foreach (var point in selectedPointList)
        {
            mSpawnedJumperList.Add(PoolManager.Instance.Spawn(Random.Range(0, 3) == 0 ? PoolObjectType.DoubleJumper : PoolObjectType.Jumper, null));
            mSpawnedJumperList[^1].transform.position = point + transform.position;
        }
    }
    public override void OnDespawn()
    {
        mSpawnedJumperList.ForEach(jumper => PoolManager.Instance.DeSpawn(jumper));
        mSpawnedJumperList.Clear();
    }
    public override void OnSpawn()
    {
    }
}
