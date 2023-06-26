using System;
using EnemySystem.AI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        private void Start()
        {
            startButton.onClick.AddListener(()=>SceneLoader.LoadScene(SceneLoader.Scene.IntroScene));
            exitButton.onClick.AddListener(Application.Quit);
        }
    }
}