using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(menuName = "Create EnemySpawnerDataSO", fileName = "EnemySpawnerDataSO", order = 0)]
    public class EnemySpawnerDataSO : ScriptableObject
    {
        public float spawnDelay;
        public float spawnRate;
        public int enemiesCount;
    }
}