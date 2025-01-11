using UnityEngine;
public class CustomBehaviour : MonoBehaviour
{
    public virtual void Initialize()
    {
    }
    private Animator mAnimator;
    public Animator Animator
    {
        get
        {
            if (mAnimator == null)
            {
                mAnimator = base.GetComponent<Animator>();
            }
            return mAnimator;
        }
    } 
    private Collider mCollider;
    public Collider Collider
    {
        get
        {
            if (mCollider == null)
            {
                mCollider = base.GetComponent<Collider>();
            }
            return mCollider;
        }
    } 
    
    private CanvasGroup mCanvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (mCanvasGroup == null)
            {
                mCanvasGroup = base.GetComponent<CanvasGroup>();
            }
            return mCanvasGroup;
        }
    }
}