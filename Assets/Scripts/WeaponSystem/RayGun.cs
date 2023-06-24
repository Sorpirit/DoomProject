using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSystem;
using Random = UnityEngine.Random;

public class RayGun : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform barrelPoint;
    [SerializeField] private LayerMask shootingMask;
    [SerializeField] private GameObject bullet; 
    
    [SerializeField] private LineRenderer rayVisualizer;

    private float _shootingTimer;
    private Weapon _weapon;
    
    private void Start()
    {
        rayVisualizer.enabled = false;
    }

    public void Init(Weapon weapon)
    {
        _weapon = weapon;
    }
    public void Shoot()
    {
        Vector3 spread = Random.insideUnitSphere * Random.Range(0, _weapon.Spread);
        Vector3 direction = (shootingPoint.forward + spread).normalized;

        var currentBullet = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        currentBullet.transform.forward = direction;
        currentBullet.GetComponent<Rigidbody>().AddForce(direction*_weapon.ShootForce);
        
        var ray = new Ray(shootingPoint.position, direction);
        bool hasHit = Physics.Raycast(ray, out var hit, _weapon.MaxDistance, shootingMask);
        if (hasHit && hit.collider.TryGetComponent<IDamageReceiver>(out var enemy))
        {
            enemy.TakeDamage(new DamageInfo(1));
        }
        ShotDraw(ray, hasHit, hit, barrelPoint.position);
    }

    private void ShotDraw(Ray ray, bool hasHit, RaycastHit hitInfo, Vector3 barrelPointPosition)
    {
        rayVisualizer.enabled = true;
        rayVisualizer.SetPosition(0, barrelPointPosition);
        if(hasHit)
            rayVisualizer.SetPosition(1, hitInfo.point);
        else
            rayVisualizer.SetPosition(1, ray.origin + ray.direction * _weapon.MaxDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootingPoint.position, shootingPoint.forward * _weapon.MaxDistance);
    }
}
