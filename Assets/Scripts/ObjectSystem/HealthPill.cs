using System;
using Core;
using StatsSystem;
using UnityEngine;
using Random = System.Random;

namespace ObjectSystem
{
    public class HealthPill: MonoBehaviour, IPickable
    {
        [SerializeField] private float healAmount;
        private SanityController _playerSanityController;

        public void OnPicked()
        {
            SanityController.Instance.IncreaseSanity(healAmount);
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
            var randomValue = new Random();
            double probs = randomValue.NextDouble();
            if (probs*100>=50)
            {
                return true;
            }

            return false;
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