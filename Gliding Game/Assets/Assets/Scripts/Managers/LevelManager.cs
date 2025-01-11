using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private string sceneName;
    private int mGroundCount;
    public override void Initialize()
    {
        base.Initialize();
        LoadLevel();
    }
    private void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync(sceneName));
        InitGrounds();
    }
    private void InitGrounds()
    {
        for (int i = 0; i < 3; i++)
        {
            var ground = PoolManager.Instance.Spawn(PoolObjectType.Ground, null);
            ground.transform.position = Vector3.forward * mGroundCount * ground.transform.GetChild(0).localScale.z * 10; // Plane default width is 10
            (ground as Ground).SpawnJumpers();
            mGroundCount++;
        }
    }
    private IEnumerator LoadLevelAsync(string sceneName)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
            yield return null;

        GameManager.LevelLoaded();
    }
}