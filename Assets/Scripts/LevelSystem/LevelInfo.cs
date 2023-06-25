using System;
using UnityEngine;

namespace LevelSystem
{
    [Serializable]
    public struct LevelInfo
    {
        [SerializeField] private int enemyCount;

        public int EnemyCount => enemyCount;
    }
}