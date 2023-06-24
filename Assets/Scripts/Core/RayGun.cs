#region

using Core;
using UnityEngine;

#endregion

public class RayGun : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform barrelPoint;
    [SerializeField] private float shootingRate = 0.2f;
    [SerializeField] private LayerMask shootingMask;
    [SerializeField] private float maxDistance = 100;

    [SerializeField] private float spreadRange = 0.1f;

    [SerializeField] private LineRenderer rayVisualizer;

    private float _shootingTimer;

    private void Start()
    {
        rayVisualizer.enabled = false;
    }

    private void Update()
    {
        if(_shootingTimer >= 0)
            _shootingTimer -= Time.deltaTime;
        
        if (InputManager.Instance.GetShootInput() && _shootingTimer <= 0)
        {
            Shoot();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootingPoint.position, shootingPoint.forward * maxDistance);
    }

    private void Shoot()
    {
        Vector3 spread = Random.insideUnitSphere * Random.Range(0, spreadRange);
        Vector3 direction = (shootingPoint.forward + spread).normalized;
        
        var ray = new Ray(shootingPoint.position, direction);
        bool hasHit = Physics.Raycast(ray, out var hit, maxDistance, shootingMask);
        if (hasHit && hit.collider.TryGetComponent<IDamageReceiver>(out var enemy))
        {
            enemy.TakeDamage(new DamageInfo(1));
        }
        ShotDraw(ray, hasHit, hit, barrelPoint.position);
        _shootingTimer = shootingRate;
    }

    private void ShotDraw(Ray ray, bool hasHit, RaycastHit hitInfo, Vector3 barrelPointPosition)
    {
        rayVisualizer.enabled = true;
        rayVisualizer.SetPosition(0, barrelPointPosition);
        if(hasHit)
            rayVisualizer.SetPosition(1, hitInfo.point);
        else
            rayVisualizer.SetPosition(1, ray.origin + ray.direction * maxDistance);
    }
}
