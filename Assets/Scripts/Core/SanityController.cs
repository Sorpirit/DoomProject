using System;
using UnityEngine;

namespace Core
{
    public class SanityController : MonoBehaviour, IDamageReceiver
    {
        [SerializeField, Tooltip("Should be in order from biggest to smallest")] private float[] maxSanityPoints;
        [SerializeField] private float degradationRate = 0.1f;

        private int _currentSanityLevel;
        private float _currentMaxSanityPoints;
        private float _nextLevelMaxSanityPoints;
        private float _sanityPoints;
    
        public int CurrentSanityLevel => _currentSanityLevel;

        public float[] MaxSanityPoints => maxSanityPoints;

        public float SanityPoints => _sanityPoints;

        public event Action OnMaxLevelChanged;
    
        private void Start()
        {
            _currentSanityLevel = 0;
            _currentMaxSanityPoints = maxSanityPoints[_currentSanityLevel];
            _nextLevelMaxSanityPoints = maxSanityPoints.Length >= 2 ? maxSanityPoints[_currentSanityLevel + 1] : 0;
            _sanityPoints = _currentMaxSanityPoints;
        
            GameMaster.Instance.Debug.AddQuickAction("1k sanity", () => _sanityPoints = 1000);
        }

        private void Update()
        {
            _sanityPoints -= degradationRate * Time.deltaTime;
            if (_sanityPoints <= 0)
            {
                UnityEngine.Debug.Log("Game Over");
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

        public void TakeDamage(DamageInfo damageInfo)
        {
        
        }
    }
}
