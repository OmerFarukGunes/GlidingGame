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
     
    private Rigidbody mRigidbody;
    public Rigidbody Rigidbody
    {
        get
        {
            if (mRigidbody == null)
            {
                mRigidbody = base.GetComponent<Rigidbody>();
            }
            return mRigidbody;
        }
    } 
    
    private Animator mChildAnimator;
    public Animator ChildAnimator
    {
        get
        {
            if (mChildAnimator == null)
            {
                mChildAnimator = base.GetComponentInChildren<Animator>();
            }
            return mChildAnimator;
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
    
    private Collider mChildCollider;
    public Collider ChildCollider
    {
        get
        {
            if (mChildCollider == null)
            {
                mChildCollider = base.GetComponentInChildren<Collider>();
            }
            return mChildCollider;
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