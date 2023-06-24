using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SanityUIComponent : MonoBehaviour
    {
        [SerializeField] private Slider SanitySlider;
        [SerializeField] private Slider MaxSanitySlider;
        
        private SanityController _sanityController;

        public void Init(SanityController sanityController)
        {
            _sanityController = sanityController;

            float maxPoints = _sanityController.MaxSanityPoints[0];
            
            SanitySlider.maxValue = maxPoints;
            MaxSanitySlider.maxValue = maxPoints;

            SanitySlider.value = maxPoints;
            MaxSanitySlider.value = 0;
            
            _sanityController.OnMaxLevelChanged += UpdateMaxSanitySlider;
        }
        
        private void LateUpdate()
        {
            SanitySlider.value = _sanityController.SanityPoints;
        }

        private void UpdateMaxSanitySlider()
        {
            MaxSanitySlider.value = _sanityController.MaxSanityPoints[0] - _sanityController.MaxSanityPoints[_sanityController.CurrentSanityLevel];
        }
    }
}