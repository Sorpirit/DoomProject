using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ObjectSystem
{
    public class SpinNotPickedObject: MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        private void Update()
        {
            transform.Rotate(0, rotationSpeed*Time.deltaTime, 0, Space.Self);
        }
    }
}