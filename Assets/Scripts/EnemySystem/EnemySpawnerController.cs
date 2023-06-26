using System;
using System.Collections.Generic;
using Core;
using LevelSystems;
using UnityEngine;

namespace EnemySystem
{
    public class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField] private EnemySpawnerDataSO enemySpawnerDataSO;
        [SerializeField] private Spawner spawner;

        public static EnemySpawnerController Instance { get; private set; }
        public event Action OnAllEnemiesKilled;

        // private Spawner _spawner;
        private HashSet<Enemy> _enemies = new();

        private int _spawnTarget;

        private int _spawnedEnemies;
        private int _killedEnemies;
        private float _spawnTimer;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _spawnTimer = enemySpawnerDataSO.spawnDelay;
            // GameManager.Instance.OnLevelStarted += GameMasterOnLevelStarted;
            LevelSystem.Instance.OnNextLevelStarted += LevelSystemOnNextLevelStarted;
        }

        private void LevelSystemOnNextLevelStarted(LevelInfo obj)
        {
            _spawnTarget = obj.EnemyCount;
            ResetCounter();
        }

        private void GameMasterOnLevelStarted(object sender, EventArgs e)
        {
            _spawnTarget = enemySpawnerDataSO.enemiesCount;
            ResetCounter();
        }

        private void Update()
        {
            if (_spawnTimer > 0)
                _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0 && _spawnedEnemies < _spawnTarget)
            {
                var enemy = spawner.SpawnEnemy();
                enemy.HealthSystem.OnDead += OnEnemyKilled;
                _enemies.Add(enemy);
                _spawnedEnemies++;
                _spawnTimer = enemySpawnerDataSO.spawnRate;
            }

            //Kill all
            if (Input.GetKeyDown(KeyCode.P))
            {
                foreach (var enemy in _enemies)
                {
                    enemy.HealthSystem.TakeDamage(new DamageInfo(1000, Vector3.zero));
                }
            }
        }

        // public void Init(Spawner enemySpawner)
        // {
        //     _spawner = enemySpawner;
        // }

        // public void LevelStarted(int arg1, LevelInfo arg2)
        // {
        //     _spawnTarget = arg2.EnemyCount;
        //     RestCounter();
        // }

        private void OnEnemyKilled()
        {
            _killedEnemies++;
            if (_killedEnemies == _spawnTarget)
            {
                // GameManager.Instance.AllEnemiesFinished();

                OnAllEnemiesKilled?.Invoke();
            }
        }

        private void ResetCounter()
        {
            _spawnedEnemies = 0;
            _killedEnemies = 0;
        }
    }
}