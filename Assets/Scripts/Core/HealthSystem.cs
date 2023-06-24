using System;
using UnityEngine;

namespace Core
{
    public class HealthSystem : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private StatsSO stats;
        private float _currentHealth;
        private const float DEFAULT_HEALTH = 100f;
        public event EventHandler OnDead;
        public event EventHandler OnHit;

        private void Start()
        {
            _currentHealth = stats is not null ? stats.maxHealth : DEFAULT_HEALTH;
        }

        public void TakeDamage(DamageInfo damageInfo)
        {
            _currentHealth -= damageInfo.Damage;
            if (_currentHealth <= 0)
            {
                OnDead?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnHit?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}