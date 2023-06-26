using System;
using UnityEngine.Audio;
using UnityEngine;

namespace WeaponSystem
{
    public class Weapon: MonoBehaviour
    {
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
        [field: SerializeField] public float MaxAmountOfBullets { get; private set; }
        [field: SerializeField] public float PushBackForce { get; private set; }
        [field:SerializeField] public float CurrentAmountOfBullets { get; private set; }
        [field: SerializeField] public float Spread { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float ShootForce { get; private set; }

        [field: SerializeField] public float BulletLifeTime { get; private set; }
        [field: SerializeField] public float Damage {get; private set; }


        public void Init()
        {
            CurrentAmountOfBullets = MaxAmountOfBullets;
        }

        public void Shoot()
        {
            CurrentAmountOfBullets -= 1;
        }

        public void Reload()
        {
            CurrentAmountOfBullets = MaxAmountOfBullets;
        }
    }
}