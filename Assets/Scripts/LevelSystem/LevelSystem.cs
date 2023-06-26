using System;
using Core;
using EnemySystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystems
{
    public class LevelSystem : MonoBehaviour
    {
        [SerializeField] private LevelInfo[] levelInfos;

        public static LevelSystem Instance { get; private set; }
        private int _currentLevelIndex;

        public int MaxLevel => levelInfos.Length;

        public int CurrentLevelIndex => _currentLevelIndex;

        // public event Action<int> OnLevelCleared;
        // public event Action<int, LevelInfo> OnLevelStarted;
        public event Action<LevelInfo> OnNextLevelStarted;
        public event Action OnGameEnd;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameManager.Instance.OnLevelStarted += GameManagerOnLevelStarted;
            EnemySpawnerController.Instance.OnAllEnemiesKilled += EnemySpawnerControllerOnAllEnemiesKilled;
        }

        private void EnemySpawnerControllerOnAllEnemiesKilled()
        {
            GoToNextLevel();
        }

        private void GameManagerOnLevelStarted(object sender, EventArgs e)
        {
            StartFirstLevel();
        }

        private void StartFirstLevel()
        {
            // OnLevelStarted?.Invoke(_currentLevelIndex, levelInfos[_currentLevelIndex]);
            OnNextLevelStarted?.Invoke(levelInfos[_currentLevelIndex]);
        }

        private bool GoToNextLevel()
        {
            if (_currentLevelIndex >= MaxLevel - 1)
            {
                OnGameEnd?.Invoke();
                return false;
            }
            
            _currentLevelIndex++;
            
            levelInfos[_currentLevelIndex - 1].LevelGameObject.SetActive(false);
            levelInfos[_currentLevelIndex].LevelGameObject.SetActive(true);
            
            OnNextLevelStarted?.Invoke(levelInfos[_currentLevelIndex]);
            return true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                GoToNextLevel();
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}