using System;
using Core;
using EnemySystem.AI;
using ObjectSystem;
using StatsSystem;
using UnityEngine;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour, IEnemyController
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private HuntAI huntAI;
        [SerializeField] private Collider bodyCollider;

        [SerializeField] private HealthPill healthPill;

        private State _currentState;
        public Collider BodyCollider => bodyCollider;
        public HealthSystem HealthSystem => healthSystem;

        public bool IsHit { get; private set; }
        public bool IsDead => _currentState == State.Dead;
        public bool IsAttacking => _currentState == State.Attacking;
        public bool IsChasing => _currentState == State.Chasing;
        public bool IsIdle => _currentState == State.Idle;

        private void Start()
        {
            healthSystem.OnDead += HealthSystemOnDead;
            healthSystem.OnHit += HealthSystemOnHit;
            healthSystem.OnDead += ()=> healthPill.Spawn(gameObject.transform);
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

        private void HealthSystemOnDead()
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