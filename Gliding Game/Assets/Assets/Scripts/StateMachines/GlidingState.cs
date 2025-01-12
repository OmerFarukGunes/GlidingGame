using DG.Tweening;
using UnityEngine;
public class GlidingState : IState
{
    private StateMachine mStatemachine;
    private Rocketman mRocketman;
    public GlidingState(StateMachine stateMachine, Rocketman rocketman)
    {
        mStatemachine = stateMachine;
        mRocketman = rocketman;
    }
    public void Enter()
    {
        InputManager.OnTouchEnd += OnTouchEnd;
        InputManager.OnTouchMove += OnTouchMove;

        mRocketman.Velocity.z = mRocketman.RocketmanData.GlideForwardSpeed;
        DOTween.Kill(Constants.ROCKETMAN_TWEEN_ID);

        mRocketman.ChildAnimator.Play(RocketmanAnimatorParams.OPEN_WINGS_NAME, 0, 1 - mRocketman.GetAnimCurrentTime());

        mRocketman.TrailRenderers.ForEach(r =>
        {
            r.time = .25f;
            r.enabled = true;
        });
    }
    public void Execute()
    {

    }
    public void Exit()
    {
        mRocketman.RocketmanTransform.localEulerAngles = new Vector3(90, 0, 0);
        mRocketman.ChildAnimator.Play(RocketmanAnimatorParams.CLOSE_WINGS_NAME, 0, 1 - mRocketman.GetAnimCurrentTime());
        InputManager.OnTouchEnd -= OnTouchEnd;
        InputManager.OnTouchMove -= OnTouchMove;

        mRocketman.TrailRenderers.ForEach(r =>
        {
            r.enabled = false;
            r.time = 0;
            r.Clear();
        });
    }

    #region Actions
    private void OnTouchEnd(Vector3 deltaPos)
    {
        mRocketman.Velocity.y = 0;
        mStatemachine.ChangeStateTo(RocketmanStates.Fly);
    }
    private void OnTouchMove(Vector3 touchpos)
    {
        Vector3 deltaPos = touchpos - mRocketman.LastTouchPos;

        float turnRotation = Mathf.Clamp(deltaPos.x * mRocketman.RocketmanData.GlideSwipeSpeed, -30, 30);

        float currentY = NormalizeAngle(mRocketman.transform.eulerAngles.y);
        Quaternion rotation = Quaternion.Euler(0, Mathf.Clamp(currentY + turnRotation, -70, 70), 0);
        mRocketman.transform.rotation = Quaternion.Lerp(mRocketman.transform.rotation, rotation, Time.deltaTime * 5);

        Quaternion rotationChild = Quaternion.Euler(Mathf.Clamp(90 + turnRotation, 60, 120), 90, 90);
        mRocketman.RocketmanTransform.localRotation = Quaternion.Lerp(mRocketman.RocketmanTransform.localRotation, rotationChild, Time.deltaTime * 5);

        Vector3 Velocity = mRocketman.Velocity;
        Velocity.y = mRocketman.RocketmanData.GlideGravity * Time.deltaTime;
        mRocketman.transform.MoveForward(Velocity);
    }
    #endregion

    private float NormalizeAngle(float angle)
    {
        if (angle > 180) angle -= 360;
        return angle;
    }
}