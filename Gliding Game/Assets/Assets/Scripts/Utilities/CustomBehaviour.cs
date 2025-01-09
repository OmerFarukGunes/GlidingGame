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
}