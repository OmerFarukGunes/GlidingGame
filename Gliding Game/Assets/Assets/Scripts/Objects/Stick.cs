using UnityEngine;
public class Stick : CustomBehaviour
{
    [SerializeField] private Transform rocketmanParent;
    private float mAnimSensivity = 0.1f;
    private float mAnimationProgress = 0f;
    private Vector3 mLastMousePosition;
    public override void Initialize()
    {
        base.Initialize();
        ListenInputActions();
        SetCamera();
        GameManager.OnGetRocketmanParent += OnGetRocketmanParent;
        GameManager.OnLevelRestarted += OnLevelRestarted;
    }
    private void SetCamera()
    {
        CameraManager.Instance.AssignTarget(transform);
        CameraManager.Instance.ChangeCameraProps(CameraStates.InStick);
    }
    private void ListenInputActions()
    {
        InputManager.OnTouchMove += OnTouchMove;
        InputManager.OnTouchStart += OnTouchStart;
        InputManager.OnTouchEnd += OnTouchEnd;
    }
    private void OnTouchStart(Vector3 touchPos)
    {
        mLastMousePosition = touchPos;
    }
    private void OnTouchMove(Vector3 touchPos)
    {
        Vector3 mouseDelta = touchPos - mLastMousePosition;
        mLastMousePosition = touchPos;

        mAnimationProgress -= mouseDelta.x * Time.deltaTime * mAnimSensivity;
        mAnimationProgress = Mathf.Clamp01(mAnimationProgress);

        Animator.Play(StickAnimatorParams.BendAnimName, 0, mAnimationProgress);
    }
    private void OnTouchEnd(Vector3 touchPos)
    {
        if (mAnimationProgress < .1f)
        {
            Animator.SetTrigger(Constants.IDLE);
            return;
        }
        GameManager.Launch(mAnimationProgress);
        Animator.SetTrigger(StickAnimatorParams.ReleaseTrigger);
        mAnimationProgress = 0;

        DeListenInputActions();
    }
    private Transform OnGetRocketmanParent()
    {
        return rocketmanParent;
    }
    private void OnLevelRestarted()
    {
        ListenInputActions();
        SetCamera();
    }
    private void DeListenInputActions()
    {
        InputManager.OnTouchMove -= OnTouchMove;
        InputManager.OnTouchStart -= OnTouchStart;
        InputManager.OnTouchEnd -= OnTouchEnd;
    }
    private void OnDestroy()
    {
        DeListenInputActions();
        GameManager.OnGetRocketmanParent -= OnGetRocketmanParent;
        GameManager.OnLevelRestarted -= OnLevelRestarted;
    }
}