#region

using System;
using UnityEngine;

#endregion

namespace Core
{
    public class Enemy : MonoBehaviour, IEnemyController
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private HuntAI huntAI;
        [SerializeField] private Collider bodyCollider;

        private State _currentState;

        public bool IsHit { get; private set; }
        public bool IsDead => _currentState == State.Dead;
        public bool IsAttacking => _currentState == State.Attacking;
        public bool IsChasing => _currentState == State.Chasing;
        public bool IsIdle => _currentState == State.Idle;

        private void Start()
        {
            healthSystem.OnDead += HealthSystemOnDead;
            healthSystem.OnHit += HealthSystemOnHit;
        }

        private void LateUpdate()
        {
            ResetOneTimeStuff();
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
            bodyCollider.enabled = false;
            _currentState = State.Dead;
        }

        private void ResetOneTimeStuff()
        {
            IsHit = false;
        }

        private enum State
        {
            Idle,
            Chasing,
            Attacking,
            Dead
        }
    }
}