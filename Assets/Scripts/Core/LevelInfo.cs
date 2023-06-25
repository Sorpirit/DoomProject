using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct LevelInfo
    {
        [SerializeField] private int enemyCount;

        public int EnemyCount => enemyCount;
    }
}