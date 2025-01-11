using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Rocketman : CustomBehaviour
{
    [SerializeField] private Player player;
    private float mPassedTime;
    private PlayerStates mPlayerState;
    private Vector3 mVelocity;
    private Vector3 mLastTouchPos;
    private bool mCanRotateAround = true;
    public override void Initialize()
    {
        base.Initialize();
        GameManager.OnLaunched += OnLaunched;
    }
    private void ListenActions()
    {
        InputManager.OnTouchMove += OnTouchMove;
        InputManager.OnTouchStart += OnTouchStart;
        InputManager.OnTouchEnd += OnTouchEnd;
    }
    private void Update()
    {
        if (mPlayerState == PlayerStates.Launched || mPlayerState == PlayerStates.AfterGliding)
            Flying();
    }
    private void Flying()
    {
        mPassedTime += Time.deltaTime;

        Vector3 displacement = Vector3.zero;

        if (mPlayerState == PlayerStates.Launched)
            displacement = new Vector3(mVelocity.x, (mVelocity.y * mPassedTime) + (.5f * player.Gravity * Mathf.Pow(mPassedTime, 2)), mVelocity.z); 
        else
            displacement = new Vector3(mVelocity.x, player.Gravity * mPassedTime, mVelocity.z);

        transform.position += displacement * Time.deltaTime;

        if (mCanRotateAround)
            transform.Rotate(Vector3.right, player.RotationSpeed * Time.deltaTime, Space.Self);
    }
    private void Gliding(Vector3 deltaPos)
    {
        Vector3 displacement = new Vector3(deltaPos.x * player.GlideSpeed, player.GlideGravity, mVelocity.z);
        transform.position += displacement * Time.deltaTime;

        float clampedRotation = Mathf.Clamp(transform.rotation.z - displacement.x, -30, 30);
        Quaternion rotation = Quaternion.Euler(60, 0, clampedRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
    }
    private void StartGliding()
    {
        DOTween.Kill(Constants.ROCKETMAN_TWEEN_ID);
        mPlayerState = PlayerStates.InGliding;

        Animator.Play("Anim_OpenWings", 0, 1 - GetAnimCurrentTime());
    }
    private void StopGliding()
    {
        mPassedTime = 0;
        mPassedTime += Time.deltaTime;
        mPlayerState = PlayerStates.AfterGliding;

        Animator.Play("Anim_CloseWings", 0, 1 - GetAnimCurrentTime());

        mCanRotateAround = false;
        transform.DORotate(new Vector3(60, 0, 0), .1f).OnComplete(() => mCanRotateAround = true).SetId(Constants.ROCKETMAN_TWEEN_ID);
    }
  
    private void OnLaunched(float pullAmount)
    {
        GameManager.OnLaunched -= OnLaunched;
        SetCamera();

        float power = pullAmount * player.LaunchPowerMultiplier;
        transform.parent = null;

        Vector3 avarageForward = (Vector3.forward + transform.forward).normalized;
        Debug.DrawRay(transform.position, avarageForward * 5, Color.green);
        mVelocity = avarageForward * power;
        mVelocity.z = Mathf.Abs(mVelocity.z * player.ForwardSpeed);
        Debug.Log(mVelocity);
        mPassedTime = 0;
        mPlayerState = PlayerStates.Launched;

        ListenActions();
    }
    private void OnTouchStart(Vector3 touchPos)
    {
        mLastTouchPos = touchPos;
        StartGliding();
    }
    private void OnTouchMove(Vector3 touchPos)
    {
        Gliding(touchPos - mLastTouchPos);
    }
    private void OnTouchEnd(Vector3 touchPos)
    {
        StopGliding();
    }
    private float GetAnimCurrentTime()
    {
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime > 1 ? 1 : stateInfo.normalizedTime;
    }
    private void SetCamera()
    {
        CameraManager.Instance.AssignTarget(transform);
        CameraManager.Instance.ChangeCameraProps(CameraStates.OnFly);
    }
    private void OnDestroy()
    {
        InputManager.OnTouchMove -= OnTouchMove;
        InputManager.OnTouchStart -= OnTouchStart;
        InputManager.OnTouchEnd -= OnTouchEnd;
    }
}
