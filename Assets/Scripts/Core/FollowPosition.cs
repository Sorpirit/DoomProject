#region

using UnityEngine;

#endregion

namespace Core
{
    public class FollowPosition : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            transform.position = target.transform.position;
        }
    }
}