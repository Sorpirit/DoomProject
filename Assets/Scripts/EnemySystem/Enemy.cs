using System;
using EnemySystem.AI;
using StatsSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour, IEnemyController
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private HuntAI huntAI;
        [SerializeField] private Collider bodyCollider;
        [SerializeField] private EnemyAttacker enemyAttacker;

        private State _currentState;
        public Collider BodyCollider => bodyCollider;
        public HealthSystem HealthSystem => healthSystem;

        public bool IsHit { get; private set; }
        public bool IsDead => HealthSystem.CurrentHealth <= 0f;
        public bool IsAttacking => _currentState == State.Attacking;
        public bool IsChasing => _currentState == State.Chasing;
        public bool IsIdle => _currentState == State.Idle;

        private void Start()
        {
            healthSystem.OnDead += HealthSystemOnDead;
            healthSystem.OnHit += HealthSystemOnHit;
            enemyAttacker.OnAttackPerformed += EnemyAttackerOnAttackPerformed;
        }

        private void EnemyAttackerOnAttackPerformed(object sender, EventArgs e)
        {
            _currentState = State.Attacking;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _currentState = State.Attacking;
            }
        }

        private void LateUpdate()
        {
            ResetOneTimeStuff();
            _currentState = State.Chasing;
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
            huntAI.StopAgent();
            healthSystem.enabled = false;
            huntAI.enabled = false;
            bodyCollider.enabled = false;
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
        }
    }
}