using Cinemachine;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private List<CameraProperties> cameraProperties;
    [SerializeField] private CinemachineVirtualCamera currentCamera;
    private CinemachineTransposer mCurrentTransposer => currentCamera.GetCinemachineComponent<CinemachineTransposer>();
    [Serializable]
    public class CameraProperties
    {
        public Vector3 FollowOffset;
        public Vector3 Rotation;
    }
    public override void Initialize()
    {
        base.Initialize();
    }
    public void AssignTarget(Transform target)
    {
      currentCamera.Follow = target;
    }
    public void ChangeCameraProps(CameraStates state)
    {
        DOTween.Kill(GetInstanceID());
        Vector3 currentVector = mCurrentTransposer.m_FollowOffset;
        DOTween.To(() => currentVector, x => currentVector = x, cameraProperties[(int)state].FollowOffset, .5f).OnUpdate(() =>
        {
            mCurrentTransposer.m_FollowOffset = currentVector;
        }).SetId(Constants.CAMERA_TWEEN_ID);
        currentCamera.transform.DORotate(cameraProperties[(int)state].Rotation,.5f).SetId(Constants.CAMERA_TWEEN_ID);
    }
}