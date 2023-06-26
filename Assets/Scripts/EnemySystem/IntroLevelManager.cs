using EnemySystem.AI;
using ObjectSystem;
using UnityEngine;

namespace EnemySystem
{
    public class IntroLevelManager : MonoBehaviour
    {
        private void Start()
        {
            HealthPill.OnPillPicked += HealthPillOnPillPicked;
        }

        private void HealthPillOnPillPicked()
        {
            HealthPill.OnPillPicked -= HealthPillOnPillPicked;
            SceneLoader.LoadScene(SceneLoader.Scene.Level0);
        }
    }
}