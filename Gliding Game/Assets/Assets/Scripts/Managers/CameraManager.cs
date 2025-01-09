using Cinemachine;
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

    }
    public void ChangeCameraProps(CameraState state)
    {

    }
}