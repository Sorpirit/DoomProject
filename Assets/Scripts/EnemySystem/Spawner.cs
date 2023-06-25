using System;
using System.Linq;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyAndProbability[] spawnVariants;

        [SerializeField] private float minSpawnRadius;
        [SerializeField] private float maxSpawnRadius;

        private readonly Collider[] _overlapColliderBuffer = new Collider[1];

        public event EventHandler<SpawnerEventArgs> OnEnemySpawned;

        private void Start()
        {
            SetValuesNormalized();
            SetColliders();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                SpawnEnemy();
        }

        public Enemy SpawnEnemy()
        {
            Enemy enemy = null;
            bool spawned = false;
            var nextEnemy = GetNextEnemy();
            while (!spawned)
            {
                Vector3 point = GenerateRandomPointInCircle(Player.Instance.transform.position, minSpawnRadius,
                    maxSpawnRadius);
                point.y = 0.2f;
                spawned = TrySpawnInPoint(point, nextEnemy.collider, nextEnemy.enemyPrefab, out enemy);
            }

            OnEnemySpawned?.Invoke(this, new SpawnerEventArgs() { spawnPoint = enemy.transform.position });

            return enemy;
        }

        private void SetColliders()
        {
            for (var i = 0; i < spawnVariants.Length; i++)
            {
                spawnVariants[i].collider = spawnVariants[i].enemyPrefab.BodyCollider as CapsuleCollider;
            }
        }

        private Vector3 GenerateRandomPointInCircle(Vector3 position, float minRadius, float maxRadius)
        {
            float angle = Random.value * 2 * Mathf.PI;
            float distance = Random.Range(minRadius, maxRadius);

            float x = position.x + distance * Mathf.Cos(angle);
            float z = position.z + distance * Mathf.Sin(angle);

            return new Vector3(x, 0f, z);
        }

        private bool TrySpawnInPoint(Vector3 point, CapsuleCollider capsuleCollider, Enemy enemyPrefab, out Enemy enemy)
        {
            var direction = new Vector3 { [capsuleCollider.direction] = 1 };
            float offset = capsuleCollider.height / 2 - capsuleCollider.radius;
            Vector3 localPoint0 = capsuleCollider.center - direction * offset;
            Vector3 localPoint1 = capsuleCollider.center + direction * offset;

            //Transforms
            var point0 = transform.TransformPoint(localPoint0);
            var point1 = transform.TransformPoint(localPoint1);
            var r = transform.TransformVector(capsuleCollider.radius, capsuleCollider.radius, capsuleCollider.radius);
            var radius = Enumerable.Range(0, 3).Select(xyz => xyz == capsuleCollider.direction ? 0 : r[xyz])
                .Select(Mathf.Abs).Max();
            //
            int collidersNumber = Physics.OverlapCapsuleNonAlloc(point0, point1, radius, _overlapColliderBuffer);

            if (collidersNumber == 0)
            {
                enemy = Instantiate(enemyPrefab, point, Quaternion.identity, transform);
                return true;
            }

            enemy = null;
            return false;
        }

        private EnemyAndProbability GetNextEnemy()
        {
            float randomValue = Random.value;
            float currentValue = randomValue;
            for (int i = 0; i < spawnVariants.Length; i++)
            {
                float spawnProbability = spawnVariants[i].ValueNormalized;
                if (currentValue < spawnProbability)
                {
                    return spawnVariants[i];
                }

                currentValue -= spawnProbability;
            }

            return spawnVariants[^1];
        }

        private void SetValuesNormalized()
        {
            float totalValue = 0f;
            for (var i = 0; i < spawnVariants.Length; i++)
            {
                totalValue += spawnVariants[i].value;
            }

            for (var i = 0; i < spawnVariants.Length; i++)
            {
                spawnVariants[i].ValueNormalized = spawnVariants[i].value / totalValue;
            }
        }
    }

    public class SpawnerEventArgs : EventArgs
    {
        public Vector3 spawnPoint;
    }

    [Serializable]
    public struct EnemyAndProbability
    {
        public Enemy enemyPrefab;
        public CapsuleCollider collider;
        public float value;
        public float ValueNormalized { get; set; }
    }
}