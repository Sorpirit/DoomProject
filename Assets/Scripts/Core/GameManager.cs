using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public event EventHandler OnLevelStarted;
        public event EventHandler OnLevelFinished;
        public event EventHandler OnLevelLoose; 
        public static GameManager Instance { get; private set; }

        public void AllEnemiesFinished()
        {
            OnLevelFinished?.Invoke(this, EventArgs.Empty);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            OnLevelStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}