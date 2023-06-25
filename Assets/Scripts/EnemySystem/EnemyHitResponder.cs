using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemySystem
{
    public class EnemyHitResponder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private float damage;
        private bool _attack;
        [SerializeField] private BoxCompHitbox[] hitBoxes;
        public float Damage => damage;

        private void Start()
        {
            foreach (var hitbox in hitBoxes)
            {
                hitbox.HitResponder = this;
            }
        }

        private void Update()
        {
            if (_attack)
            {
                foreach (var hitbox in hitBoxes)
                {
                    hitbox.CheckHit();
                }
            }
        }

        public bool CheckHit(HitData data)
        {
            return true;
        }

        public void Response(HitData data)
        {
            _attack = false;
        }

        public void SetAttackStarted()
        {
            _attack = true;
        }
    }
}