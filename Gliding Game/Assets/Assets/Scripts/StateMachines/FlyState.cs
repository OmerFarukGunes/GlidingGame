using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyState : IState
{
    private StateMachine mStatemachine;
    private Rocketman mRocketman;
    public FlyState(StateMachine stateMachine, Rocketman rocketman)
    {
        mStatemachine = stateMachine;
        mRocketman = rocketman;
    }
    public void Enter()
    {
    
        InputManager.OnTouchStart += mRocketman.OnTouchStart;
    }

    public void Execute()
    {
        Fly();
    }
    private void Fly()
    {
        mStatemachine.GetRocketman().Velocity.y += mStatemachine.GetRocketman().RocketmanData.Gravity * Time.deltaTime;
        mRocketman.transform.position += mRocketman.Velocity * Time.deltaTime;
        if (mRocketman.CanRotate)
            mRocketman.transform.Rotate(Vector3.right, mRocketman.RocketmanData.RotationSpeed * Time.deltaTime, Space.Self);
    }
    public void Exit()
    {
        InputManager.OnTouchStart -= mRocketman.OnTouchStart;
    }

    public void OnEvent()
    {

    }
}