using System;
using TMPro;
using UnityEngine;
using WeaponSystem;

namespace UI
{
    public class BulletsUIComponent: MonoBehaviour
    {
        [SerializeField] private TMP_Text CurrentBulletsAmount;
        [SerializeField] private TMP_Text MaxBulletsAmount;
        private Weapon _weapon;

        public void SetCurrentWeapon(Weapon currentWeapon)
        {
            _weapon = currentWeapon;
            CurrentBulletsAmount.text = currentWeapon.MaxAmountOfBullets.ToString();
            MaxBulletsAmount.text = currentWeapon.MaxAmountOfBullets.ToString();
        }

        public void UpdateUIComponents()
        {
            CurrentBulletsAmount.text = _weapon.CurrentAmountOfBullets.ToString();
        }
    }
}