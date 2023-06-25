using UnityEngine;

namespace Core
{
    public struct DamageInfo
    {
        public float Damage { get; private set; }
        public Vector3 PushBackForce { get; private set; }

        public DamageInfo(float damage, Vector3 pushForce)
        {
            Damage = damage;
            PushBackForce = pushForce;
        }
    }
}