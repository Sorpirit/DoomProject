using System;
using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class HuntAI : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        
        [SerializeField] private Transform target;

        private void Update()
        {
            agent.SetDestination(target.position);
        }
    }
}