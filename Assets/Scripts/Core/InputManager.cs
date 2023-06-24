using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    public class InputManager
    {
        public static InputManager Instance => _instance ??= new InputManager();

        [CanBeNull] 
        private static InputManager _instance;

        private readonly PlayerInput _playerInput;

        private InputManager()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        public Vector2 GetMovementInput()
        {
            return _playerInput.Player.Movement.ReadValue<Vector2>();
        }
        
        public Vector2 GetMouseDelta()
        {
            return _playerInput.Player.Look.ReadValue<Vector2>();
        }
    }
}