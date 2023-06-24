using System;
using UnityEngine;

namespace Core
{
    public class Enemy : MonoBehaviour, IEnemyController
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private HuntAI huntAI;

        private void Start()
        {
            healthSystem.OnDead += HealthSystemOnDead;
            healthSystem.OnHit += HealthSystemOnHit;
        }

        private void HealthSystemOnHit(object sender, EventArgs e)
        {
            IsHit = true;
        }

        private void HealthSystemOnDead(object sender, EventArgs e)
        {
            healthSystem.enabled = false;
            huntAI.enabled = false;
        }

        public bool IsHit { get; private set; }

        private void ResetOneTimeStuff()
        {
            IsHit = false;
        }

        private void Update()
        {
            ResetOneTimeStuff();
        }
    }

    public interface IEnemyController
    {
        public bool IsHit { get; }
    }
}