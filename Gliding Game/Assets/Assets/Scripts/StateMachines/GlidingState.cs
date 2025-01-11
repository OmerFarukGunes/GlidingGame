using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

        mRocketman.Animator.Play("Anim_OpenWings", 0, 1 - mRocketman.GetAnimCurrentTime());

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
        InputManager.OnTouchEnd -= OnTouchEnd;
        InputManager.OnTouchMove -= OnTouchMove;

        mRocketman.TrailRenderers.ForEach(r =>
        {
            r.enabled = false;
            r.time = 0;
            r.Clear();
        });
    }
    private void OnTouchEnd(Vector3 deltaPos)
    {
        mRocketman.Velocity.y = 0;

        mRocketman.Animator.Play("Anim_CloseWings", 0, 1 - mRocketman.GetAnimCurrentTime());

        mRocketman.CanRotate = false;
        mRocketman.transform.DORotate(new Vector3(90, 0, 0), .1f).OnComplete(() => mRocketman.CanRotate = true).SetId(Constants.ROCKETMAN_TWEEN_ID);

        mStatemachine.ChangeStateTo(RocketmanStates.Fly);
    }
    private void OnTouchMove(Vector3 touchpos)
    {
        Vector3 deltaPos = touchpos - mRocketman.LastTouchPos;
        Vector3 displacement = new Vector3(deltaPos.x * mRocketman.RocketmanData.GlideSwipeSpeed, mRocketman.RocketmanData.GlideGravity, mRocketman.Velocity.z);
        mRocketman.transform.position += displacement * Time.deltaTime;
        float clampedRotation = Mathf.Clamp(displacement.x, -30, 30);

        Quaternion rotation = Quaternion.Euler(Mathf.Clamp(90+clampedRotation,60,120), 90, 90);
        mRocketman.transform.rotation = Quaternion.Lerp(mRocketman.transform.rotation, rotation, Time.deltaTime * 5);
    }
}