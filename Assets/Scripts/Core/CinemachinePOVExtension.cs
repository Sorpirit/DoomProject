using Cinemachine;
using Core;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField, Range(0.1f, 3.0f)] 
    private float sensitivityX = 1.0f;
    [SerializeField, Range(0.1f, 3.0f)] 
    private float sensitivityY = 1.0f;
    [SerializeField] 
    private float rotationSpeed = 10.0f;
    
    [SerializeField]
    private float clampAngle = 90.0f;
    
    [SerializeField]
    private Transform playerTransform;
    
    private float _rotationYaw;
    private float _rotationPitch;
    
    protected override void Awake()
    {
        base.Awake();

        var rotation = transform.localRotation.eulerAngles;
        _rotationYaw = rotation.y;
        _rotationPitch = rotation.x;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(!vcam.Follow || stage != CinemachineCore.Stage.Aim)
            return;

        var mouseDelta = InputManager.Instance.GetMouseDelta() * rotationSpeed * Time.deltaTime;
        _rotationYaw += mouseDelta.x;
        _rotationPitch -= mouseDelta.y;
        _rotationPitch = Mathf.Clamp(_rotationPitch, -clampAngle, clampAngle);
            
        state.RawOrientation = Quaternion.Euler(_rotationPitch, _rotationYaw, 0);
    }
}
