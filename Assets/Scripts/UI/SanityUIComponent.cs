using System;
using StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SanityUIComponent : MonoBehaviour
    {
        [SerializeField] private SanitySingleUI sanitySliderTemplate;
        [SerializeField] private Transform slidersGroup;

        private SanitySingleUI[] _sliders;
        private int _currentSanityUIIndex;

        private void Start()
        {
            InstantiateAllSliders();
            SanityController.Instance.OnSanityStageChanged += SanityControllerOnSanityStageChanged;
            _currentSanityUIIndex = _sliders.Length - 1;
        }

        private void SanityControllerOnSanityStageChanged(object sender, EventArgs e)
        {
            int currentSanityStage = SanityController.Instance.CurrentSanityStage;
            int stagesCount = SanityController.Instance.SanityDataSO.stagesCount;
            for (int i = Mathf.Clamp(currentSanityStage,0, _sliders.Length); i < stagesCount; i++)
            {
                _sliders[i].SetToFilled();
            }

            _currentSanityUIIndex = Mathf.Clamp(currentSanityStage-1,0, _sliders.Length -1 );
        }

        private void InstantiateAllSliders()
        {
            int slidersCount = SanityController.Instance.SanityDataSO.stagesCount;

            _sliders = new SanitySingleUI[slidersCount];
            foreach (Transform child in slidersGroup)
            {
                if (child != sanitySliderTemplate.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            for (int i = 0; i < slidersCount; i++)
            {
                _sliders[i] = Instantiate(sanitySliderTemplate, slidersGroup);
                _sliders[i].SetToActive();
            }

            sanitySliderTemplate.gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            _sliders[_currentSanityUIIndex].SetFillAmount(SanityController.Instance.CurrentStageSanityNormalized);
        }
    }
}