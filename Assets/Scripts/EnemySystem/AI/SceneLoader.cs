using Core;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace EnemySystem.AI
{
    public static class SceneLoader
    {
        public enum Scene
        {
            MenuScene,
            IntroScene,
            SampleScene,
            LoadingScene
        }

        private static Scene _currentScene = Scene.MenuScene;
        public static void LoadScene(Scene scene)
        {
            _currentScene = scene;
            SceneManager.LoadScene(scene.ToString());
        }

        public static void ReloadCurrentScene()
        {
            SceneManager.LoadScene(_currentScene.ToString());
        }
    }
}