using Core;

namespace StatsSystem
{
    public interface IDamageReceiver
    {
        void TakeDamage(DamageInfo damageInfo);
    }
}