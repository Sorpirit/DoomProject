using System;
using EnemySystem.AI;
using ObjectSystem;
using JetBrains.Annotations;
using StatsSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour, IEnemyController
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private HuntAI huntAI;
        [SerializeField] private Collider bodyCollider;

        [SerializeField] private HealthPill healthPill;

        [SerializeField] private EnemyAttacker enemyAttacker;
        [SerializeField] [CanBeNull] private EnemyHitResponder enemyHitResponder;
        [SerializeField] private EnemyAnimationController enemyAnimationController;
        
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
            healthSystem.OnDead += ()=> healthPill.Spawn(gameObject.transform);
            enemyAttacker.OnAttackStarted += EnemyAttackerOnAttackStarted;
            enemyAnimationController.OnAttackAnimationFinished += EnemyAnimationControllerOnAttackAnimationFinished;
        }

        private void EnemyAnimationControllerOnAttackAnimationFinished(object sender, EventArgs e)
        {
            enemyAttacker.AttackAnimationFinished = true;
            huntAI.StartMoving();
            _currentState = State.Chasing;
        }

        private void EnemyAttackerOnAttackStarted(object sender, EventArgs e)
        {
            _currentState = State.Attacking;
            if (enemyHitResponder is not null) enemyHitResponder.SetAttackStarted();
            huntAI.StopAgent();
        }

        private void LateUpdate()
        {
            ResetOneTimeStuff();
        }

        private void HealthSystemOnHit(object sender, EventArgs e)
        {
            IsHit = true;
        }

        private void HealthSystemOnDead()
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