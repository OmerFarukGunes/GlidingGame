using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private string sceneName;
    private List<Ground> mGroundList = new List<Ground>();
    private int mGroundCount;
    public override void Initialize()
    {
        base.Initialize();
        GameManager.OnLevelRestarted += OnLevelRestarted;
        LoadLevel();
    }
    private void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync(sceneName));
        InitGrounds();
    }
    private IEnumerator LoadLevelAsync(string sceneName)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
            yield return null;

        GameManager.LevelLoaded();
    }
    private void InitGrounds()
    {
        for (int i = 0; i < Constants.INIT_PLANE_COUNT; i++)
        {
            SpawnGround();
        }
    }
    public void RemoveGroundInList(Ground ground)
    {
        mGroundList.Remove(ground);
        PoolManager.Instance.DeSpawn(ground);
        SpawnGround();
    }
    public void SpawnGround()
    {
        mGroundList.Add(PoolManager.Instance.Spawn(PoolObjectType.Ground, null) as Ground);
        mGroundList[^1].transform.position = Vector3.forward * mGroundCount * mGroundList[^1].transform.GetChild(0).localScale.z * Constants.PLANE_Z_SIZE_MULT;
        mGroundList[^1].SpawnJumpers();
        mGroundCount++;
    }
    private void OnLevelRestarted()
    {
        mGroundCount = 0;
        mGroundList.ForEach(x => PoolManager.Instance.DeSpawn(x));
        mGroundList.Clear();
        InitGrounds();
    }
}