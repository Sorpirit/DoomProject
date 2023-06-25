using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField] private float spawnDelay = 5.0f;
        [SerializeField] private float spawnRate = 2.0f;
        
        public event Action OnAllEnemiesKilled; 

        private Spawner _spawner;
        private HashSet<Enemy> _enemies = new ();

        private int _spawnTarget;
        
        private int _spawnedEnemies;
        private int _killedEnemies;
        private float _spawnTimer;

        private void Start()
        {
            _spawnTimer = spawnDelay;
        }

        private void Update()
        {
            if(_spawnTimer > 0)
                _spawnTimer -= Time.deltaTime;
            
            if (_spawnTimer <= 0 && _spawnedEnemies < _spawnTarget)
            {
                var enemy = _spawner.SpawnEnemy();
                enemy.HealthSystem.OnDead += OnEnemyKilled;
                _enemies.Add(enemy);
                _spawnedEnemies++;
                _spawnTimer = spawnRate;
            }

            //Kill all
            if (Input.GetKeyDown(KeyCode.P))
            {
                foreach (var enemy in _enemies)
                {
                    enemy.HealthSystem.TakeDamage(new DamageInfo(1000, 0));
                }
            }
        }

        public void Init(Spawner enemySpawner)
        {
            _spawner = enemySpawner;
        }

        public void LevelStarted(int arg1, LevelInfo arg2)
        {
            _spawnTarget = arg2.EnemyCount;
            RestCounter();
        }
        
        private void OnEnemyKilled(object sender, EventArgs e)
        {
            _killedEnemies++;
            if(_killedEnemies == _spawnTarget)
                OnAllEnemiesKilled?.Invoke();
        }

        private void RestCounter()
        {
            _spawnedEnemies = 0;
            _killedEnemies = 0;
        }
    }
}