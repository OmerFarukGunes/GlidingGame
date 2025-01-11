using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    private StateMachine mStatemachine;
    private Rocketman mRocketman;
    public FallState(StateMachine stateMachine, Rocketman rocketman)
    {
        mStatemachine = stateMachine;
        mRocketman = rocketman;
    }
    public void Enter()
    {

        mRocketman.Animator.SetTrigger(Constants.IDLE);
    }

    public void Execute()
    {
        mStatemachine.GetRocketman().Velocity.y += mStatemachine.GetRocketman().RocketmanData.Gravity * Time.deltaTime;
        mRocketman.transform.position += mRocketman.Velocity * Time.deltaTime;

        if (mRocketman.Velocity.magnitude < mRocketman.RocketmanData.MinBounceSpeed)
        {
            mRocketman.Velocity = Vector3.zero;
        }
        else if (mRocketman.CanRotate)
        {
            mRocketman.transform.Rotate(-mRocketman.Velocity, mRocketman.RocketmanData.RotationSpeed * Time.deltaTime, Space.Self);
            mRocketman.Velocity -= mRocketman.Velocity * .1f * Time.deltaTime;
        }
    }

    public void Exit()
    {
   
    }

    public void OnEvent()
    {
    
    }
}
