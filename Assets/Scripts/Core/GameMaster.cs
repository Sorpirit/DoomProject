using System;
using UnityEngine;

namespace Core
{
    public class GameMaster : MonoBehaviour
    {
        private void Awake()
        {
            var i = InputManager.Instance;
        }
    }
}