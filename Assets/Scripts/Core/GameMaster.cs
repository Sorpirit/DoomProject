using System;
using UnityEngine;

namespace Core
{
    public class GameMaster : MonoBehaviour
    {
        private void Awake()
        {
            //init input
            var i = InputManager.Instance;
        }
    }
}