#region

using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace Core
{
    public class InputManager
    {
        [CanBeNull] 
        private static InputManager _instance;

        private readonly PlayerInput _playerInput;

        private InputManager()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        public static InputManager Instance => _instance ??= new InputManager();

        public Vector2 GetMovementInput()
        {
            return _playerInput.Player.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return _playerInput.Player.Look.ReadValue<Vector2>();
        }

        public bool GetShootInput()
        {
            return _playerInput.Player.Shoot.triggered;
        }
        
        public bool GetJumpInput()
        {
            return _playerInput.Player.Jump.IsPressed();
        }
    }
}