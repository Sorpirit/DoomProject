using UnityEngine;

namespace Core
{
    public class HealthSystem : MonoBehaviour, IDamageReceiver
    {
        public void TakeDamage(DamageInfo damageInfo)
        {
            Debug.Log( name + " received damage points: " +damageInfo.Damage);
        }
    }
}