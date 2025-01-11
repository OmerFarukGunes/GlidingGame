using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : CustomBehaviour
{
    public override void Initialize()
    {
        gameObject.SetActive(false);
    }
    public virtual void ShowPanel()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        CanvasGroup.Open();
        UIManager.Instance.SetCurrentPanel(this);
    }
    public virtual void HidePanel()
    {
        CanvasGroup.Close();
    }

}
