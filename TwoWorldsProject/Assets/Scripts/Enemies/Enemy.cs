using System;
using PlayerScripts;
using ShootingScripts;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [Header("AI")] 
        [SerializeField] NavMeshAgent NavMeshAgent;
        [SerializeField] string PlayerTag;
        GameObject player;
        void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            NavMeshAgent.destination = player.transform.position; 
        }
    }
}
