using System;
using UnityEngine;

namespace StatsSystem
{
    public class SanityController : MonoBehaviour
    {
        [SerializeField] private SanityDataSO sanityDataSO;
        private float _sanityPerStage;

        private int _currentSanityStage;
        private float _currentSanity;
        
        public int CurrentSanityStage => _currentSanityStage;
        public float CurrentSanity => _currentSanity;
        public float CurrentSanityNormalized => _currentSanity / sanityDataSO.maxSanity;

        public static SanityController Instance;
        public event EventHandler OnSanityStageChanged;
        public event EventHandler OnZeroSanity; 

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitValues();
        }

        private void Update()
        {
            DecreaseSanity(sanityDataSO.startingDegradationRate * Time.deltaTime);
        }

        private void InitValues()
        {
            _sanityPerStage = sanityDataSO.maxSanity / sanityDataSO.stagesCount;
            _currentSanityStage = 0;
            _currentSanity = sanityDataSO.maxSanity;
        }

        public void DecreaseSanity(float value)
        {
            if (value < 0f)
            {
                Debug.LogError("Decrease value should be positive");
            }
            _currentSanity -= value;
            if (_currentSanity < 0f)
            {
                OnZeroSanity?.Invoke(this, EventArgs.Empty);
            }

            int updatedSanityStage = (int)(_currentSanity / _sanityPerStage);
            if (updatedSanityStage != _currentSanityStage)
            {
                OnSanityStageChanged?.Invoke(this, EventArgs.Empty);
            }

            _currentSanityStage = updatedSanityStage;
        }

        public void IncreaseSanity(float value)
        {
            if (value < 0f)
            {
                Debug.LogError("Increase value should be positive");
            }
            
            float maxSanityPossible = (_currentSanityStage + 1) * _sanityPerStage;
            _currentSanity = Mathf.Min(_currentSanity + value, maxSanityPossible);
        }
    }
}