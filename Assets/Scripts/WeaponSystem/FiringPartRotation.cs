using System;
using System.Collections;
using Core;
using UnityEngine;

namespace WeaponSystem
{
    public class FiringPartRotation: MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Transform rotatingPart;
        

        public void RotationUpdate()
        {
            rotatingPart.Rotate(0,0, rotationSpeed*Time.deltaTime, Space.Self);
        }

        public void ReloadAnimation(float duration)
        {
            float startRotation = transform.localRotation.eulerAngles.z;
            float endRotation = startRotation - 360.0f;
            StartCoroutine(Rotate(duration, endRotation));

        }

        IEnumerator Rotate(float duration, float endRotation)
        {
            var transformLocalRotation = transform.localRotation;
            float currentRotation = transformLocalRotation.eulerAngles.z;
            float t = 0.0f;
            while ( t  < duration )
            {
                t += Time.deltaTime;
                float zRotation = Mathf.Lerp(currentRotation, endRotation, t / duration) % 360.0f;
                transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, zRotation));
                yield return null;
            }
        }
    }
}