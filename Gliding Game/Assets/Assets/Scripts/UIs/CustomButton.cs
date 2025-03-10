using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CustomButton : Button
{
    private event Action mOnClick;
    public virtual void AddListener(Action onClick)
    {
        mOnClick = onClick;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }
        mOnClick();
    }
}