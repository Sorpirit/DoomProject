using System;
using EnemySystem.AI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core
{
    public class WinScreenUI : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button exitButton;
        private void Start()
        {
            Debug.Assert(GameManager.Instance != null, "GameManager should be on the scene");
         
            menuButton.onClick.AddListener(()=>SceneLoader.LoadScene(SceneLoader.Scene.MenuScene));
            exitButton.onClick.AddListener(()=>
            {
                Debug.Log("Application Quit");
                Application.Quit();
            });
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