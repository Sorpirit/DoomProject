using UnityEngine;

namespace EnemySystem
{
    public class BoxCompHitbox : MonoBehaviour, IHitDetector
    {
        [SerializeField] private BoxCollider hitboxCollider;
        [SerializeField] private LayerMask hitBoxLayerMask;
        public IHitResponder HitResponder { get; set; }
        private readonly float _thickness = 0.025f;
        private const int MAX_SIMULTANEOUS_HITS = 10;
        private RaycastHit[] _raycastHitsBuffer = new RaycastHit[MAX_SIMULTANEOUS_HITS];

        public void CheckHit()
        {
            Vector3 scaledSize = new Vector3(
                hitboxCollider.size.x * transform.lossyScale.x,
                hitboxCollider.size.y * transform.lossyScale.y,
                hitboxCollider.size.z * transform.lossyScale.z);

            float distance = scaledSize.y - _thickness;
            var direction = transform.up;
            var center = transform.TransformPoint(hitboxCollider.center);
            var start = center - direction * (distance / 2);
            var halfExtents = new Vector3(scaledSize.x, _thickness, scaledSize.z) / 2;
            Quaternion orientation = transform.rotation;

            HitData hitData = null;
            IHurtBox hurtBox = null;
            var size = Physics.BoxCastNonAlloc(start, halfExtents, direction, _raycastHitsBuffer, orientation, distance,
                hitBoxLayerMask);
            for (int i = 0; i < size; i++)
            {
                hurtBox = _raycastHitsBuffer[i].collider.GetComponent<IHurtBox>();
                if (hurtBox != null)
                {
                    if (hurtBox.Active)
                    {
                        hitData = new HitData
                        {
                            damage = HitResponder == null ? 0 : HitResponder.Damage,
                            hurtBox = hurtBox,
                            hitDetector = this
                        };
                        if (hitData.Validate())
                        {
                            hitData.hitDetector.HitResponder?.Response(hitData);
                            hitData.hurtBox.HurtResponder?.Response(hitData);
                        }
                    }
                }
            }
        }
    }
}