using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    public class PlayerHurtResponder : MonoBehaviour, IHurtResponder
    {
        private List<CompHurtbox> _hurtBoxes;
        public event EventHandler<ReceivedDamageEventArgs> OnDamageReceived;

        private void Start()
        {
            _hurtBoxes = new List<CompHurtbox>(GetComponentsInChildren<CompHurtbox>());
            foreach (var compHurtbox in _hurtBoxes)
            {
                compHurtbox.HurtResponder = this;
            }
        }

        public bool CheckHit(HitData data)
        {
            return true;
        }

        public void Response(HitData data)
        {
            OnDamageReceived?.Invoke(this, new ReceivedDamageEventArgs(){damageReceived = data.damage});
        }
    }

    public class ReceivedDamageEventArgs : EventArgs
    {
        public float damageReceived;
    }
}