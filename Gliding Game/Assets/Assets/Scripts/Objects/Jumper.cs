using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : PoolObject
{

    public override void OnDespawn()
    {
    
    }

    public override void OnSpawn()
    {
        int horizontalScale = Random.Range(5, 15);
        transform.localScale = new Vector3(horizontalScale, Random.Range(10, 20), horizontalScale);
    }
}
