using System;
using Core;
using UnityEngine;

namespace WeaponSystem
{
    public class BulletController: MonoBehaviour
    {
        private Weapon _weapon;
        private float _maxAliveTime;
        public void Init(Weapon weapon)
        {
            _weapon = weapon;
            _maxAliveTime = _weapon.BulletLifeTime;
        }

        private void Update()
        {
            _maxAliveTime -= Time.deltaTime;
            if (_maxAliveTime<=0)
            {
                DestroyBullet();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<HealthSystem>())
            {
                collision.collider.GetComponent<HealthSystem>().TakeDamage(new DamageInfo(_weapon.Damage, _weapon.PushBackForce));
                DestroyBullet();
            }
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}