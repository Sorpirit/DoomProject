using System;
using UnityEngine;

namespace Core
{
    public class DeathScreenUI  : MonoBehaviour
    {
        private void Start()
        {
            if (GameManager.Instance is null)
            {
                Debug.LogError("GameManager should be on the scene");
            }
            GameManager.Instance!.OnLevelLoose += GameManagerOnLevelFinished;
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