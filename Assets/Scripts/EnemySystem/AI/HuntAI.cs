#region

using Core;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace EnemySystem.AI
{
    public class HuntAI : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private Transform _target;

        private void Start()
        {
            _target = Player.Instance.transform;
        }

        private void Update()
        {
            agent.SetDestination(_target.position);
        }

        public void StopAgent()
        {
            agent.isStopped = true;
        }
    }
}