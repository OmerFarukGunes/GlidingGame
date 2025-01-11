using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private StateMachine mStatemachine;
    private Rocketman mRocketman;
    public IdleState(StateMachine stateMachine, Rocketman rocketman)
    {
        mStatemachine = stateMachine;
        mRocketman = rocketman;
    }
    public void Enter()
    {
        GameManager.OnLaunched += mRocketman.OnLaunched;
        mRocketman.transform.parent = GameManager.GetRocketmanParent();
        mRocketman.transform.localPosition = Vector3.zero;
        mRocketman.transform.localEulerAngles = Vector3.zero;
        mRocketman.Animator.SetTrigger(Constants.IDLE);
        mRocketman.CanRotate = true;
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        GameManager.OnLaunched -= mRocketman.OnLaunched;
    }

    public void OnEvent()
    {

    }
}
