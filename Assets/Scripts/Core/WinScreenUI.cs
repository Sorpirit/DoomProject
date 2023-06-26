using System;
using UnityEngine;

namespace Core
{
    public class WinScreenUI : MonoBehaviour
    {
        private void Start()
        {
            Debug.Assert(GameManager.Instance != null, "GameManager should be on the scene");
            
            GameManager.Instance.OnLevelFinished += GameManagerOnLevelFinished;
            Hide();
        }

        private void GameManagerOnLevelFinished(object sender, EventArgs e)
        {
            Show();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
    }
}