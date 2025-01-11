using TMPro;
using UnityEngine;
public class HudPanel : UIPanel
{
    [SerializeField] private TMP_Text bestScoreTxt;
    [SerializeField] private TMP_Text scoreTxt;
    public override void Initialize()
    {
        base.Initialize();
        GameManager.OnLevelFailed += SetBestScore;
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
    }
    private void Update()
    {
        SetScore();
    }
    private void SetBestScore()
    {
        bestScoreTxt.SetText(PlayerManager.Instance.GetBestScore().ToString());
    }
    public void SetScore()
    {
        scoreTxt.SetText(PlayerManager.Instance.GetCurrentScore().ToString());
    }
}