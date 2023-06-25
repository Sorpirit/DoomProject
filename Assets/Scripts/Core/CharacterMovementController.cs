using UnityEngine;

namespace Core
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10.0f;
        [SerializeField] private float acceleration = 10.0f;
        [SerializeField] private float rotationSpeed = 100.0f;
        [SerializeField] private float moveDrag = 10.0f;
        [SerializeField] private float jumpForce = 10.0f;
        [SerializeField] private float airControlMultiplayer;

        [SerializeField] private float playerHeight = 1.0f;
        [SerializeField] private LayerMask groundLayer;
        
        [SerializeField, Range(0.1f, 3.0f)] private float sensitivity = 1.0f;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform cameraOrientation;
        [SerializeField] private int clampAngle = 80;
        private Rigidbody _rg;
        private float _rotationPitch;


        private float _rotationYaw;

        private float _sqrMaxSpeed;
        
        private Vector2 _inputDirection;
        private bool _jumpInput;
        
        private bool _readyToJump;
        
        private Collider[] _collidersBuffer = new Collider[1];

        private void Start()
        {
            _rg = GetComponent<Rigidbody>();
            _rg.freezeRotation = true;
            _sqrMaxSpeed = maxSpeed * maxSpeed;
        }

        private void Update()
        {
            ApplyRotation();
            GatherMoveInput();
            LimitSpeed();
        }

        private void FixedUpdate()
        {
            bool isGrounded = GroundCheck();
            _rg.drag = isGrounded ? moveDrag : 0.1f;
            
            ApplyFlatSpeed(isGrounded);
            ApplyVerticalSpeed(isGrounded);
        }

        private void GatherMoveInput()
        {
            _inputDirection = InputManager.Instance.GetMovementInput();
            _jumpInput = InputManager.Instance.GetJumpInput();
        }
        
        private void LimitSpeed()
        {
            var velocity = _rg.velocity;
            var flatVelocity = new Vector3(velocity.x, 0, velocity.z);
            if(flatVelocity.sqrMagnitude >= _sqrMaxSpeed)
                _rg.velocity = flatVelocity.normalized * maxSpeed + Vector3.up * velocity.y;
        }

        private void ApplyRotation()
        {
            var mouseDelta = InputManager.Instance.GetMouseDelta() * sensitivity * rotationSpeed * Time.deltaTime;
            _rotationYaw += mouseDelta.x;
            _rotationPitch -= mouseDelta.y;
            _rotationPitch = Mathf.Clamp(_rotationPitch, -clampAngle, clampAngle);
            playerTransform.rotation = Quaternion.Euler(0, _rotationYaw, 0);
            cameraOrientation.rotation = Quaternion.Euler(_rotationPitch, _rotationYaw, 0);
        }
        
        private void ApplyFlatSpeed(bool isGrounded)
        {
            var direction = _inputDirection * acceleration * Time.deltaTime;
            var moveDirection = playerTransform.forward * direction.y + playerTransform.right * direction.x;
            if(!isGrounded)
                moveDirection *= airControlMultiplayer;
            _rg.AddForce(moveDirection, ForceMode.Force);
        }
        
        private void ApplyVerticalSpeed(bool isGrounded)
        {
            if(!isGrounded)
                return;

            if (_rg.velocity.y < 0.05f)
                _readyToJump = true;
            
            if(_readyToJump && _jumpInput)
            {
                _rg.velocity = new Vector3(_rg.velocity.x, 0, _rg.velocity.z);
                _rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private bool GroundCheck()
        {
            int collider = Physics.OverlapSphereNonAlloc(playerTransform.position + Vector3.down * playerHeight, .2f, _collidersBuffer, groundLayer);
            return collider > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position + Vector3.down * playerHeight, .2f);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 100), "Speed:" + _rg.velocity.magnitude);
        }
    }
}