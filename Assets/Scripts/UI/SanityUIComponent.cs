using StatsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SanityUIComponent : MonoBehaviour
    {
        [SerializeField] private Slider SanitySlider;
        [SerializeField] private Slider MaxSanitySlider;

        private void LateUpdate()
        {
            SanitySlider.value = SanityController.Instance.CurrentSanityNormalized;
        }
    }
}