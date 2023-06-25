#region

using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace WeaponSystem
{
    public class RayGun : MonoBehaviour
    {
        [SerializeField] private Transform shootingPoint;
        [SerializeField] private Transform barrelPoint;
        [SerializeField] private LayerMask shootingMask;
        [SerializeField] private GameObject bullet;
        [SerializeField] private ParticleSystem muzzleFlash;
    
        [SerializeField] private LineRenderer rayVisualizer;

        private float _shootingTimer;
        [SerializeField]private Weapon weapon;
    
        private void Start()
        {
            rayVisualizer.enabled = false;
        }

        public void Shoot()
    
        {
            Vector3 spread = Random.insideUnitSphere * Random.Range(0, weapon.Spread);
            Vector3 direction = (shootingPoint.forward + spread).normalized;

            var currentBullet = Instantiate(bullet, barrelPoint.position, Quaternion.identity);
            currentBullet.GetComponent<BulletController>().Init(weapon);
            currentBullet.transform.forward = direction;
            currentBullet.GetComponent<Rigidbody>().AddForce(direction*weapon.ShootForce, ForceMode.Impulse);
            muzzleFlash.Play();
            var ray = new Ray(shootingPoint.position, direction);
            bool hasHit = Physics.Raycast(ray, out var hit, weapon.MaxDistance, shootingMask);
            //ShotDraw(ray, hasHit, hit, barrelPoint.position);
        }

        private void ShotDraw(Ray ray, bool hasHit, RaycastHit hitInfo, Vector3 barrelPointPosition)
        {
            rayVisualizer.enabled = true;
            rayVisualizer.SetPosition(0, barrelPointPosition);
            if(hasHit)
                rayVisualizer.SetPosition(1, hitInfo.point);
            else
                rayVisualizer.SetPosition(1, ray.origin + ray.direction * weapon.MaxDistance);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(shootingPoint.position, shootingPoint.forward * weapon.MaxDistance);
        }
    }
}
