using System;
using UI;
using UnityEngine;
using WeaponSystem;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }

        [Space(10)]
        [Header("Weapon systems")]
        [SerializeField] private Weapon weapon;
        [SerializeField] private RayGun rayGun;
        
        [Space(10)]
        [Header("UI systems")]
        [SerializeField] private BulletsUIComponent bulletsUIComponent;
        
        private WeaponSystemController _weaponSystemController;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            _weaponSystemController = new WeaponSystemController(weapon, rayGun, bulletsUIComponent);
        }

        private void Update()
        {
            _weaponSystemController.UpdateWeaponSystem();
        }
    }
}