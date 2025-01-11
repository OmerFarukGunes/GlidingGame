using System.Collections.Generic;
using UnityEngine;
public class Rocketman : CustomBehaviour
{
    [HideInInspector] public bool CanRotate;
    [HideInInspector] public Vector3 Velocity;
    public List<TrailRenderer> TrailRenderers;
    public Vector3 LastTouchPos{ get; private set; }
    public Vector3 RotateAxis{ get; private set; }
    public StateMachine StateMachine { get; private set; }
    public RocketmanData RocketmanData;
    public override void Initialize()
    {
        base.Initialize();
        StateMachine = new StateMachine(this);
        PlayerManager.Instance.Rocketman = this;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(TAGS.GROUND))
        {
            PoolManager.Instance.Spawn(PoolObjectType.SmokeVFX, null).transform.position = collision.contacts[0].point;
            BounceOffGround(collision.contacts[0].normal);
        }
        else if (!StateMachine.CheckCurrentState(RocketmanStates.Fall))
        {
            if (collision.transform.CompareTag(TAGS.DOUBLE_JUMPER))
            {
                PoolManager.Instance.Spawn(PoolObjectType.SmokeVFX, null).transform.position = collision.contacts[0].point;
                HitJumper(Vector3.up, RocketmanData.JumperPower * 2);
            }
            else if (collision.transform.CompareTag(TAGS.JUMPER))
            {
                PoolManager.Instance.Spawn(PoolObjectType.SmokeVFX, null).transform.position = collision.contacts[0].point;
                HitJumper(Vector3.up, RocketmanData.JumperPower);
            }
        }
     
    }
    private void BounceOffGround(Vector3 collisionNormal)
    {
        Vector3 bounceDirection = Vector3.Reflect(Velocity.normalized, collisionNormal);
        Velocity = bounceDirection * Velocity.magnitude * RocketmanData.BounceDamping;
        RotateAxis = Vector3.Cross(Velocity.normalized, Vector3.forward).normalized;
        StateMachine.ChangeStateTo(RocketmanStates.Fall);
    }
    private void HitJumper(Vector3 direction, float power)
    {
        Velocity.y = 0;
        Velocity += direction * power;
        RotateAxis = Vector3.Cross(Velocity.normalized, Vector3.forward).normalized;
        StateMachine.ChangeStateTo(RocketmanStates.Fly);
    }
    private void Update()
    {
        StateMachine.Execute();
    }
    public void OnLaunched(float pullAmount)
    {
        SetCamera();

        RotateAxis = Vector3.right;
        transform.parent = null;

        float power = pullAmount * RocketmanData.LaunchPowerMultiplier;
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
}