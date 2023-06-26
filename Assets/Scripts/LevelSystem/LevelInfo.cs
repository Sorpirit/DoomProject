using System;
using UnityEngine;

namespace LevelSystem
{
    [Serializable]
    public struct LevelInfo
    {
        [SerializeField] private int enemyCount;
        [SerializeField] private GameObject levelGameObject;
        public int EnemyCount => enemyCount;

        public GameObject LevelGameObject => levelGameObject;
    }
}