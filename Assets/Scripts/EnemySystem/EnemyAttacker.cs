using System;
using Core;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private float attackRange;
        public event EventHandler OnAttackStarted;
        public bool AttackAnimationFinished { get; set; } = true;

        private void Update()
        {
            if (CanPerformAttack())
            {
                OnAttackStarted?.Invoke(this, EventArgs.Empty);
                AttackAnimationFinished = false;
            }
        }

        private bool CanPerformAttack()
        {
            if (AttackAnimationFinished && Vector3.Distance(Player.Instance.transform.position, transform.position) <= attackRange)
            {
                return true;
            }

            return false;
        }
    }
}