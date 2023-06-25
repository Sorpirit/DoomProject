using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;

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