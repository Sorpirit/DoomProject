namespace Core
{
    public struct DamageInfo
    {
        public float Damage { get; private set; }

        public DamageInfo(float damage)
        {
            Damage = damage;
        }
    }
}