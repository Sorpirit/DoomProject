using UI;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponSystem
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
            if (_weapon.CurrentAmountOfBullets!=0 && _canShootFireRate && _currentState == WeaponState.ReadyToShoot)
            {
                _latsTimeShot = Time.deltaTime;
                _weapon.Shoot();
                _rayGun.Shoot();
                _canShootFireRate = false;
            }
        }

        public void StartReloading()
        {
            _startReloadTime = Time.deltaTime;
            _currentState = WeaponState.Reloading;
            _weapon.Reload();
        }
        

        public void Update()
        {
            if (Time.deltaTime - _latsTimeShot >= _weapon.FireRate)
            {
                _canShootFireRate = true;
            }

            if (_weapon.CurrentAmountOfBullets == 0)
            {
                _enoughBullets = false;
            }

            if (_currentState == WeaponState.Reloading)
            {
                if (Time.deltaTime - _startReloadTime >= _weapon.ReloadTime)
                {
                    _currentState = WeaponState.ReadyToShoot;
                }
            }
            _bulletsUIComponent.UpdateUIComponents();
        }
    }
}