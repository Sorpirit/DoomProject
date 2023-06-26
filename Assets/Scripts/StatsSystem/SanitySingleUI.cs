using System;
using UnityEngine;
using UnityEngine.UI;

namespace StatsSystem
{
    public class SanitySingleUI : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image image;
        [SerializeField] private Image filledBackground;

        private void Start()
        {
            image.fillAmount = 1f;
            SetToActive();
        }

        public void SetToFilled()
        {
            filledBackground.gameObject.SetActive(true);
            background.gameObject.SetActive(false);
            image.gameObject.SetActive(false);
        }

        public void SetToActive()
        {
            filledBackground.gameObject.SetActive(false);
            background.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
        }

        public void SetFillAmount(float amountNormalized)
        {
            image.fillAmount = amountNormalized;
        }
    }
}