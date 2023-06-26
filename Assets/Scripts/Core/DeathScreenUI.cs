using System;
using EnemySystem.AI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class DeathScreenUI  : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        private void Start()
        {
            if (GameManager.Instance is null)
            {
                Debug.LogError("GameManager should be on the scene");
            }
            GameManager.Instance!.OnLevelLoose += GameManagerOnLevelFinished;
            restartButton.onClick.AddListener(SceneLoader.ReloadCurrentScene);
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