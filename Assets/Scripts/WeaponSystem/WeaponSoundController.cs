using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WeaponSystem
{
    public class WeaponSoundController: MonoBehaviour
    {
        [SerializeField] private AudioSource bulletFireSound;
        [SerializeField] private AudioSource blankFireSound;

        private void Start()
        {
            // InputManager.Instance._playerInput.Player.Shoot.canceled += _ => StopShootSounds();
            InputManager.Instance._playerInput.Player.Shoot.canceled += ShootOnCanceled;
        }

        private void ShootOnCanceled(InputAction.CallbackContext obj)
        {
            StopShootSounds();
        }

        public void PlaySound(bool hasAmmo)
        {
            if (hasAmmo && !bulletFireSound.isPlaying)
            {
                bulletFireSound.Play();
            }
            else
            {
                if (!blankFireSound.isPlaying)
                {
                    blankFireSound.Play();   
                }
            }
        }

        private void StopShootSounds()
        {
            bulletFireSound.Stop();
            blankFireSound.Stop();
        }

        private void OnDestroy()
        {
            InputManager.Instance._playerInput.Player.Shoot.canceled -= ShootOnCanceled;
        }
    }
}