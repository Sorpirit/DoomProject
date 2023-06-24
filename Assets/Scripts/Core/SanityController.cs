#region

using System;
using UnityEngine;

#endregion

namespace Core
{
    public class SanityController : MonoBehaviour, IDamageReceiver
    {
        [SerializeField, Tooltip("Should be in order from biggest to smallest")] private float[] maxSanityPoints;
        [SerializeField] private float degradationRate = 0.1f;
        private float _currentMaxSanityPoints;

        private int _currentSanityLevel;
        private float _nextLevelMaxSanityPoints;
        private float _sanityPoints;

        public int CurrentSanityLevel => _currentSanityLevel;

        public float[] MaxSanityPoints => maxSanityPoints;

        public float SanityPoints => _sanityPoints;

        private void Start()
        {
            _currentSanityLevel = 0;
            _currentMaxSanityPoints = maxSanityPoints[_currentSanityLevel];
            _nextLevelMaxSanityPoints = maxSanityPoints.Length >= 2 ? maxSanityPoints[_currentSanityLevel + 1] : 0;
            _sanityPoints = _currentMaxSanityPoints;
        
            //GameMaster.Instance.Debug.AddQuickAction("1k sanity", () => _sanityPoints = 1000);
        }

        private void Update()
        {
            DecreaseSanity(degradationRate * Time.deltaTime);
        }

        public event Action OnMaxLevelChanged;

        public void TakeDamage(DamageInfo damageInfo)
        {
            DecreaseSanity(damageInfo.Damage);
        }

        private void DecreaseSanity(float value)
        {
            _sanityPoints -= value;
            if (_sanityPoints <= 0)
            {
                Debug.Log("Game Over");
                return;
            }

            if (_sanityPoints <= _nextLevelMaxSanityPoints)
            {
                _currentSanityLevel++;
                _currentMaxSanityPoints = maxSanityPoints[_currentSanityLevel];
                _nextLevelMaxSanityPoints = maxSanityPoints.Length >= 2 ? maxSanityPoints[_currentSanityLevel + 1] : 0;
                OnMaxLevelChanged?.Invoke();
            }
        }
    }
}
