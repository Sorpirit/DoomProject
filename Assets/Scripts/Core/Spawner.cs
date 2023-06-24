using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private EnemyAndProbability[] spawnVariants;

        private void Start()
        {
            SetValuesNormalized();
        }

        private Enemy GetNextEnemy()
        {
            float randomValue = Random.value;
            float currentValue = randomValue;
            for (int i = 0; i < spawnVariants.Length; i++)
            {
                float spawnProbability = spawnVariants[i].ValueNormalized;
                if (currentValue < spawnProbability)
                {
                    return spawnVariants[i].enemyPrefab;
                }

                currentValue -= spawnProbability;
            }

            return spawnVariants[^1].enemyPrefab;
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

    [Serializable]
    public struct EnemyAndProbability
    {
        public Enemy enemyPrefab;
        public float value;
        public float ValueNormalized { get; set; }
    }
}