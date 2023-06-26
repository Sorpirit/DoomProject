using System;
using Core;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponSoundController: MonoBehaviour
    {
        [SerializeField] private AudioSource bulletFireSound;
        [SerializeField] private AudioSource blankFireSound;

        private void Start()
        {
            InputManager.Instance._playerInput.Player.Shoot.canceled += _ => StopShootSounds();
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
    }
}