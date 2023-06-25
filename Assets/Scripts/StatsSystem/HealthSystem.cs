#region

using System;
using Core;
using UnityEngine;

#endregion

namespace StatsSystem
{
    public class HealthSystem : MonoBehaviour, IDamageReceiver
    {
        private const float DEFAULT_HEALTH = 100f;
        [SerializeField] private StatsSO stats;
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = stats is not null ? stats.maxHealth : DEFAULT_HEALTH;
        }

        public event Action OnDead;
        public event EventHandler OnHit;

        public void TakeDamage(DamageInfo damageInfo)
        {
            _currentHealth -= damageInfo.Damage;
            if (_currentHealth <= 0)
            {
                OnDead?.Invoke();
            }
            else
            {
                OnHit?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}