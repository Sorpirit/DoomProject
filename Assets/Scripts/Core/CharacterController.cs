using System;
using UnityEngine;

namespace Core
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10.0f;
        [SerializeField] private float acceleration = 10.0f;
        [SerializeField] private float rotationSpeed = 100.0f;
        
        [SerializeField, Range(0.1f, 3.0f)] private float sensitivityX = 1.0f;
        [SerializeField, Range(0.1f, 3.0f)] private float sensitivityY = 1.0f;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraOrientation;
        
        
        private float _rotationYaw;
        private float _rotationPitch;
        private Rigidbody _rg;
        private int _clampAngle = 80;

        private float sqrMaxSpeed;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _rg = GetComponent<Rigidbody>();
            _rg.freezeRotation = true;
            sqrMaxSpeed = maxSpeed * maxSpeed;
        }

        private void FixedUpdate()
        {
            var mouseDelta = InputManager.Instance.GetMouseDelta() * rotationSpeed * Time.deltaTime;
            _rotationYaw += mouseDelta.x;
            _rotationPitch -= mouseDelta.y;
            _rotationPitch = Mathf.Clamp(_rotationPitch, -_clampAngle, _clampAngle);
            playerTransform.rotation = Quaternion.Euler(0, _rotationYaw, 0);
            cameraOrientation.rotation = Quaternion.Euler(_rotationPitch, _rotationYaw, 0);
            
            var direction = InputManager.Instance.GetMovementInput() * acceleration * Time.deltaTime;
            var moveDirection = playerTransform.forward * direction.y + playerTransform.right * direction.x;
            _rg.AddForce(moveDirection, ForceMode.Force);
            
            var velocity = _rg.velocity;
            if(velocity.sqrMagnitude >= sqrMaxSpeed)
                _rg.velocity = velocity.normalized * maxSpeed;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 50), "Speed:" + _rg.velocity.magnitude);
        }
    }
}