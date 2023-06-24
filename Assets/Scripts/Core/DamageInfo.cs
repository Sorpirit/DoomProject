namespace Core
{
    public struct DamageInfo
    {
        public float Damage { get; private set; }
        public float PushBackForce { get; private set; }

        public DamageInfo(float damage, float pushForce)
        {
            Damage = damage;
            PushBackForce = pushForce;
        }
    }
}