using UnityEngine;

namespace EnemySystem
{
    public class CompHurtbox : MonoBehaviour, IHurtBox
    {
        [SerializeField] private bool active = true;
        [SerializeField] private GameObject owner = null;
        private IHurtResponder _hurtResponder;
        public bool Active => active;
        public GameObject Owner => owner;
        public Transform Transform => transform;
        public IHurtResponder HurtResponder { get => _hurtResponder; set => _hurtResponder = value; }
        public bool CheckHit(HitData hitData)
        {
            if (_hurtResponder == null)
            {
                Debug.Log("No responder");
            }

            return true;
        }
    }
}