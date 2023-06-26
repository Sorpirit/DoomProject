using System;
using EnemySystem;
using UnityEngine;

namespace LevelSystems
{
    [Serializable]
    public struct LevelInfo
    {
        // [SerializeField] private int enemyCount;
        [SerializeField] private GameObject levelGameObject;
        [SerializeField] private EnemySpawnerDataSO enemySpawnerDataSO;
        public int EnemyCount => enemySpawnerDataSO.enemiesCount;

        public GameObject LevelGameObject => levelGameObject;
    }
}