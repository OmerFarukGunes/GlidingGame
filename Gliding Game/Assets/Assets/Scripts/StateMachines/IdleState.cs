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
        mRocketman.RocketmanTransform.localEulerAngles = Vector3.zero;
        mRocketman.ChildAnimator.SetTrigger(Constants.IDLE);

        mRocketman.CanRotate = true;
        mRocketman.Collider.enabled = true;
    }
    public void Execute()
    {

    }
    public void Exit()
    {
        GameManager.OnLaunched -= mRocketman.OnLaunched;
    }
}