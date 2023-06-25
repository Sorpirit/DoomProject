using System;
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

        private void Awake()
        {
            Instance = this;
        }
    }
}