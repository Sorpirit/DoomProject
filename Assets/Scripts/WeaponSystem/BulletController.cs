using System;
using Core;
using StatsSystem;
using UnityEngine;

namespace WeaponSystem
{
    public class BulletController: MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private GameObject bulletPrefab; 
        private Weapon _weapon;
        private float _maxAliveTime;
        private Rigidbody _bulletRb;
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

        private void Awake()
        {
            _bulletRb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            explosion.Play();
            if (collision.collider.GetComponent<HealthSystem>())
            {
                collision.collider.GetComponent<HealthSystem>().TakeDamage(new DamageInfo(_weapon.Damage, _weapon.PushBackForce*_bulletRb.velocity.normalized));
            }
            bulletPrefab.SetActive(false);
            GetComponent<Rigidbody>().isKinematic = true;
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}