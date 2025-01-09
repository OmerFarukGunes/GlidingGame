using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private string sceneName;
    public override void Initialize()
    {
        base.Initialize();
        LoadLevel();
    }
    private void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync(sceneName));
    }
    private IEnumerator LoadLevelAsync(string sceneName)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
            yield return null;

        GameManager.LevelLoaded();
    }
}