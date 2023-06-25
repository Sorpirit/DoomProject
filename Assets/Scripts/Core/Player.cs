#region

using UnityEngine;

#endregion

namespace Core
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}