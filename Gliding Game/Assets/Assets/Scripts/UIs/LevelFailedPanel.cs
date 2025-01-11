using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelFailedPanel : UIPanel
{
    [SerializeField] private Image icon;
    [SerializeField] private Sprite bestScoreIcon;
    [SerializeField] private Sprite scoreIcon;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private CustomButton restartButton;
    public override void Initialize()
    {
        base.Initialize();
        restartButton.AddListener(OnClickRestartButton);
    }
    public override void ShowPanel()
    {
        SetScore();
        base.ShowPanel();
    }
    public void SetScore()
    {
        scoreTxt.SetText(PlayerManager.Instance.GetCurrentScore().ToString());
        icon.sprite = PlayerManager.Instance.GetCurrentScore()>=PlayerManager.Instance.GetBestScore() ? bestScoreIcon : scoreIcon;
    }
    private void OnClickRestartButton()
    {
        GameManager.LevelRestarted();
        UIManager.Instance.GetPanel<HudPanel>().ShowPanel();
        HidePanel();
    }
}