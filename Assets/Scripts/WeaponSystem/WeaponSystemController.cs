using Core;
using UI;
using UnityEngine;

namespace WeaponSystem
{
    
    public class WeaponSystemController
    {
        private Weapon _weapon;
        private RayGun _rayGun;
        private WeaponState _currentState;
        private BulletsUIComponent _bulletsUIComponent;
        private bool _canShootFireRate;
        private float _latsTimeShot;
        private bool _enoughBullets;
        private float _startReloadTime;

        public void Init(Weapon weapon, RayGun rayGun, BulletsUIComponent bulletsUIComponent)
        {
            _weapon = weapon;
            _weapon.Init();
            _rayGun = rayGun;
            _bulletsUIComponent = bulletsUIComponent;
            _bulletsUIComponent.SetCurrentWeapon(_weapon);
            _canShootFireRate = true;
            _enoughBullets = true;
            _currentState = WeaponState.ReadyToShoot;
        }

        public void StartShooting()
        {
            if (_weapon.CurrentAmountOfBullets!=0 && _canShootFireRate && _currentState == WeaponState.ReadyToShoot && _enoughBullets)
            {
                _latsTimeShot = Time.deltaTime;
                _weapon.Shoot();
                _rayGun.Shoot();
                _canShootFireRate = false;
            }
        }

        public void StartReloading()
        {
            _startReloadTime = Time.time;
            _currentState = WeaponState.Reloading;
        }
        

        public void UpdateWeaponSystem()
        {
            if (InputManager.Instance.GetShootInput())
            {
                StartShooting();
            }

            if (InputManager.Instance.GetReloadInput())
            {
                StartReloading();
            }
            if (Time.time - _latsTimeShot >= _weapon.FireRate)
            {
                _canShootFireRate = true;
            }

            if (_weapon.CurrentAmountOfBullets == 0)
            {
                _enoughBullets = false;
            }

            if (_currentState == WeaponState.Reloading)
            {
                if (Time.time - _startReloadTime >= _weapon.ReloadTime)
                {
                    _currentState = WeaponState.ReadyToShoot;
                    _enoughBullets = true;
                    _weapon.Reload();
                }
            }
            _bulletsUIComponent.UpdateUIComponents();
        }
    }
}