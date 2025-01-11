using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<UIPanel> uIPanelList;
    private UIPanel mCurrentPanel;
    public override void Initialize()
    {
        base.Initialize();
        uIPanelList.ForEach(x =>
        {
            x.Initialize();
        });
        GetPanel<HudPanel>().ShowPanel();
        GameManager.OnLevelFailed += OnLevelFailed;
    }
    public void SetCurrentPanel(UIPanel panel)
    {
        mCurrentPanel = panel;
    }
    public T GetPanel<T>() where T : UIPanel
    {
        foreach (var item in uIPanelList)
        {
            if (item.GetType() == typeof(T)) return (T)item;
        }

        return null;
    }
    private void OnLevelFailed() 
    {
        mCurrentPanel?.HidePanel();
        GetPanel<LevelFailedPanel>().ShowPanel();
    }
}
