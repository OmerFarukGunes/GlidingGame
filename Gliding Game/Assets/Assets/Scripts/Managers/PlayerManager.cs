using UnityEngine;
public class PlayerManager : Singleton<PlayerManager>
{
    [HideInInspector] public Rocketman Rocketman;

    public override void Initialize()
    {
        base.Initialize();
        GameManager.OnLevelFailed += OnLevelFailed;
    }
    private void SaveBestScore()
    {
        PlayerPrefs.SetInt(SaveKeys.BEST_SCORE, GetCurrentScore());
    }
    public int GetCurrentScore()
    {
        return (int)Rocketman.transform.position.z;
    }
    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(SaveKeys.BEST_SCORE, 0);
    }
    private void OnLevelFailed()
    {
        if (GetBestScore() < GetCurrentScore())
            SaveBestScore();
    }
}