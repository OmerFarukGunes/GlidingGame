using System.Collections.Generic;
public class StateMachine
{
    private Rocketman mRocketman;

    private IState mCurrentState;

    private List<IState> mStates = new List<IState>();
    public StateMachine(Rocketman rocketman)
    {
        mRocketman = rocketman;
        Initialize();
    }
    public void Initialize()
    {
        mStates.Add(new IdleState(this,mRocketman));
        mStates.Add(new FlyState(this, mRocketman));
        mStates.Add(new GlidingState(this, mRocketman));
        mStates.Add(new FallState(this, mRocketman));

        mCurrentState = mStates[0];
        mCurrentState.Enter();
    }
    public void ChangeStateTo(RocketmanStates state, bool force = false)
    {
        if (mCurrentState != mStates[(int)state] || force)
        {
            mCurrentState.Exit();
            mCurrentState = mStates[(int)state];
            mCurrentState.Enter();
        }
    }
    public bool CheckCurrentState(RocketmanStates state)
    {
        return mCurrentState == mStates[(int)state];
    }
    public void Enter()
    {
        mCurrentState.Enter();
    }
    public void Execute()
    {
        mCurrentState.Execute();
    }
    public void Exit()
    {
        mCurrentState.Exit();
    }
    public Rocketman GetRocketman()
    {
        return mRocketman;
    }
}