using System.Collections.Generic;
using UnityEngine;
public class Rocketman : CustomBehaviour
{
    [HideInInspector] public bool CanRotate;
    [HideInInspector] public Vector3 Velocity;

    public Transform RocketmanTransform;
    public List<TrailRenderer> TrailRenderers;
    public Vector3 LastTouchPos { get; private set; }
    public Vector3 RotateAxis { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public RocketmanData RocketmanData;
    public override void Initialize()
    {
        base.Initialize();
        StateMachine = new StateMachine(this);
        PlayerManager.Instance.Rocketman = this;
    }
    private void Update()
    {
        StateMachine.Execute();
    }
    public void SetCamera()
    {
        CameraManager.Instance.AssignTarget(transform);
        CameraManager.Instance.ChangeCameraProps(CameraStates.OnFly);
    }
    public float GetAnimCurrentTime()
    {
        AnimatorStateInfo stateInfo = ChildAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime > 1 ? 1 : stateInfo.normalizedTime;
    }

    #region Collision Behaviours
    private void OnCollisionEnter(Collision collision)
    {
        PoolManager.Instance.Spawn(PoolObjectType.SmokeVFX, null).transform.position = collision.contacts[0].point;
        if (collision.transform.CompareTag(TAGS.JUMPER_AROUND))
        {
            collision.transform.GetComponent<Collider>().enabled = false;
            BounceOfHit(collision.contacts[0].normal);
        }
        else if (collision.transform.CompareTag(TAGS.GROUND))
        {
            BounceOfHit(collision.contacts[0].normal);
        }
        else if (!StateMachine.CheckCurrentState(RocketmanStates.Fall))
        {
            if (collision.transform.CompareTag(TAGS.DOUBLE_JUMPER))
            {
                HitJumper(RocketmanData.JumperPower * 2);
            }
            else if (collision.transform.CompareTag(TAGS.JUMPER))
            {
                HitJumper(RocketmanData.JumperPower);
            }
        }
        Rigidbody.ResetRigidbody();
    }
    private void AfterHitCollision(RocketmanStates state)
    {
        RotateAxis = Vector3.Cross(Velocity.normalized, Vector3.forward).normalized;
        StateMachine.ChangeStateTo(state);
    }
    private void BounceOfHit(Vector3 collisionNormal)
    {
        Vector3 bounceDirection = Vector3.Reflect(Velocity.normalized, collisionNormal);
        bounceDirection.y = Mathf.Abs(bounceDirection.y);
        Velocity = bounceDirection * Velocity.z * RocketmanData.BounceDamping;
        AfterHitCollision(RocketmanStates.Fall);
    }
    private void HitJumper(float power)
    {
        Velocity.y = 0;
        Velocity += Vector3.up * power;
        AfterHitCollision(RocketmanStates.Fly);
    }
    #endregion

    #region Actions 
    public void OnLaunched(float pullAmount)
    {
        SetCamera();

        RotateAxis = Vector3.right;
        transform.parent = null;

        float power = pullAmount * RocketmanData.LaunchPowerMultiplier;
        Vector3 avarageForward = (Vector3.forward + transform.forward).normalized;
        Velocity = avarageForward * power;
        Velocity.z = Mathf.Abs(Velocity.z * RocketmanData.ForwardSpeedMultiplier);
        transform.forward = avarageForward;
        StateMachine.ChangeStateTo(RocketmanStates.Fly);
    }
    public void OnTouchStart(Vector3 touchPos)
    {
        LastTouchPos = touchPos;
        StateMachine.ChangeStateTo(RocketmanStates.Gliding);
    }
    #endregion
}