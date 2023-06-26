using UnityEngine;

namespace Core
{
    public class SceneManager: MonoBehaviour
    {
        [SerializeField] private string[] sceneNames;

        public void LoadScene(int index)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames[index]);
        }
        
        public void QuitGame()
        {
            UnityEngine.Debug.Log("Quit!");
            Application.Quit();
        }
    }
}