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
            _currentState = State.Chasing;
            IsHit = true;
        }

        private void HealthSystemOnDead(object sender, EventArgs e)
        {
            // Destroy(healthSystem);
            // Destroy(huntAI);
            healthSystem.enabled = false;
            huntAI.enabled = false;
            _currentState = State.Dead;
        }

        public bool IsHit { get; private set; }
        public bool IsDead => _currentState == State.Dead;
        public bool IsAttacking => _currentState == State.Attacking;
        public bool IsChasing => _currentState == State.Chasing;
        public bool IsIdle => _currentState == State.Idle;

        private void ResetOneTimeStuff()
        {
            IsHit = false;
        }

        private void Update()
        {
            ResetOneTimeStuff();
        }

        private State _currentState;
        
        private enum State
        {
            Idle, 
            Chasing,
            Attacking,
            Dead
        }
    }
}