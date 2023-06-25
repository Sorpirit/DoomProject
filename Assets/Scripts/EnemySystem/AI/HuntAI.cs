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
        [SerializeField] private float speed;
        private bool _stopped;

        private Transform _target;

        private void Start()
        {
            _target = Player.Instance.transform;
            agent.speed = speed;
        }

        private void Update()
        {
                agent.SetDestination(_target.position);

        }

        public void StopAgent()
        {
            agent.isStopped = true;
        }

        public void SetSpeedToZero()
        {
            agent.speed = 0;
        }

        public void StartMoving()
        {
            agent.speed = speed;
            agent.isStopped = false;
        }
    }
}