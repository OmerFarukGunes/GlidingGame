using UnityEngine;

public class Rocketman : CustomBehaviour
{
     public Vector3 Velocity;
    [HideInInspector] public bool CanRotate;
    public Vector3 LastTouchPos{ get; private set; }
    public StateMachine StateMachine { get; private set; }

    public RocketmanData RocketmanData;

    public override void Initialize()
    {
        base.Initialize();
        StateMachine = new StateMachine(this);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(TAGS.GROUND))
        {
            BounceOffGround(collision.contacts[0].normal);
        }
        else if (!StateMachine.CheckCurrentState(RocketmanStates.Fall))
        {
            if (collision.transform.CompareTag(TAGS.DOUBLE_JUMPER))
            {
                HitJumper(Vector3.up, RocketmanData.JumperPower * 2);
            }
            else if (collision.transform.CompareTag(TAGS.JUMPER))
            {
                HitJumper(Vector3.up, RocketmanData.JumperPower);
            }
        }
     
    }
    private void BounceOffGround(Vector3 collisionNormal)
    {
        Vector3 bounceDirection = Vector3.Reflect(Velocity.normalized, collisionNormal);
        Velocity = bounceDirection * Velocity.magnitude * RocketmanData.BounceDamping;
        StateMachine.ChangeStateTo(RocketmanStates.Fall);
    }
    private void HitJumper(Vector3 direction, float power)
    {
        Velocity.y = 0;
        Velocity += direction * power;
        StateMachine.ChangeStateTo(RocketmanStates.Fall);
    }
    private void Update()
    {
        StateMachine.Execute();
    }
    public void OnLaunched(float pullAmount)
    {
        SetCamera();
        float power = pullAmount * RocketmanData.LaunchPowerMultiplier;
        transform.parent = null;
        Vector3 avarageForward = (Vector3.forward + transform.forward).normalized;
        Velocity = avarageForward * power;
        Velocity.z = Mathf.Abs(Velocity.z * RocketmanData.ForwardSpeedMultiplier);
        StateMachine.ChangeStateTo(RocketmanStates.Fly);
    }
    public void OnTouchStart(Vector3 touchPos)
    {
        LastTouchPos = touchPos;
        StateMachine.ChangeStateTo(RocketmanStates.Gliding);
    }
    public float GetAnimCurrentTime()
    {
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime > 1 ? 1 : stateInfo.normalizedTime;
    }
    public void SetCamera()
    {
        CameraManager.Instance.AssignTarget(transform);
        CameraManager.Instance.ChangeCameraProps(CameraStates.OnFly);
    }
    private void OnDestroy()
    {

    }
}