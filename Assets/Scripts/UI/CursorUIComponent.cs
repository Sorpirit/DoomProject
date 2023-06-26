using System;
using Core;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace UI
{
    public class CursorUIComponent: MonoBehaviour
    {
        private void Start()
        {
            if (GameManager.Instance is null)
            {
                Debug.LogError("GameManager should be initialized");
            }
            
            GameManager.Instance!.OnLevelStarted += GameManagerOnLevelStarted;
            GameManager.Instance!.OnLevelLoose += GameManagerOnLevelFinished;
            GameManager.Instance!.OnLevelFinished += GameManagerOnLevelFinished;
        }

        private void GameManagerOnLevelFinished(object sender, EventArgs e)
        {
            Unlock();
        }

        private void GameManagerOnLevelStarted(object sender, EventArgs e)
        {
            Lock();
        }

        public void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}