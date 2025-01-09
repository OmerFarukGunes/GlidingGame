using UnityEngine;
public class Stick : CustomBehaviour
{
    private float mAnimSensivity = 0.1f;
    private float mAnimationProgress = 0f;
    private Vector3 mLastMousePosition;
    public override void Initialize()
    {
        base.Initialize();
        ListenActions();
        SetCamera();
    }
    private void SetCamera()
    {
        CameraManager.Instance.AssignTarget(transform);
        CameraManager.Instance.ChangeCameraProps(CameraStates.InStick);
    }
    private void ListenActions()
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
        GameManager.Launch(mAnimationProgress);

        Animator.SetTrigger(StickAnimatorParams.ReleaseTrigger);
        mAnimationProgress = 0;

        DeListenActions();
    }
    private void DeListenActions()
    {
        InputManager.OnTouchMove -= OnTouchMove;
        InputManager.OnTouchStart -= OnTouchStart;
        InputManager.OnTouchEnd -= OnTouchEnd;
    }
}