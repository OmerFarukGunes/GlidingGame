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
        GameManager.LevelFailed();
        GameManager.OnLevelRestarted += OnLevelRestarted;
        mRocketman.ChildAnimator.SetTrigger(Constants.IDLE);
        mRocketman.Rigidbody.velocity = Vector3.zero;
        mRocketman.Rigidbody.angularVelocity = Vector3.zero;
    }
    public void Execute()
    {
        if (mRocketman.Velocity.magnitude < mRocketman.RocketmanData.MinBounceSpeed || mRocketman.transform.position.y < Constants.MIN_Y_LIMIT)
        {
            mRocketman.Collider.enabled = false;
            mRocketman.Velocity = Vector3.zero;
            return;
        }
        else if (mRocketman.CanRotate)
        {
            mRocketman.RocketmanTransform.Rotate(mRocketman.RotateAxis, mRocketman.Velocity.magnitude * mRocketman.RocketmanData.FallRotationSpeed  * Time.deltaTime, Space.Self);
            mRocketman.Velocity -= mRocketman.Velocity * .1f * Time.deltaTime;
        }

        mRocketman.Velocity.y += mRocketman.RocketmanData.Gravity * Time.deltaTime;
        mRocketman.transform.MoveForward(mRocketman.Velocity);
    }

    public void Exit()
    {
        GameManager.OnLevelRestarted -= OnLevelRestarted;
    }
    private void OnLevelRestarted()
    {
        mStatemachine.ChangeStateTo(RocketmanStates.Idle);
    }
}