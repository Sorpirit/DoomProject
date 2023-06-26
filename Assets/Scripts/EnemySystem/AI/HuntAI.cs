#region

using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace EnemySystem.AI
{
    public class HuntAI : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float speed;
        [SerializeField] [CanBeNull] private Transform followTransform;
        private bool _stopped;
        
        private void Start()
        {
            if (followTransform == null) 
            {
                followTransform = Player.Instance.transform;
            }

            agent.speed = speed;
        }


        private void Update()
        {
            agent.SetDestination(followTransform!.position);
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