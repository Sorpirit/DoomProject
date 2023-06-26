using System;
using StatsSystem;
using UnityEngine;

namespace ObjectSystem
{
    public class HealthPill: MonoBehaviour, IPickable
    {
        [SerializeField] private float healAmount;
        private SanityController _playerSanityController;
        public static event Action OnPillPicked; 

        public void OnPicked()
        {
            SanityController.Instance.IncreaseSanity(healAmount);
            OnPillPicked?.Invoke();
            Destroy(gameObject);
        }

        public void Spawn(Transform position)
        {
            if (RandomSpawn())
            {
                Instantiate(gameObject, position.position+new Vector3(0,1,0), position.rotation);
            }
        }

        private bool RandomSpawn()
        {
            // var randomValue = new Random();
            // double probs = randomValue.NextDouble();
            // if (probs*100>=50)
            // {
                // return true;
            // }

            // return false;
            return true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerPicker>())
            {
                OnPicked();
            }
        }
    }
}