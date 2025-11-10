using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float movementSpeed = 5f;

    //private UnityEngine.AI.NavMeshAgent agent;
    private NavMeshAgent agent;

      private bool navigationActive = false;

    private void Awake()
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false; // Desactiva el NavMesh al inicio
    }

    private void Start()
    {
        agent.speed = movementSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        //agent.SetDestination(Player.position);
          if (navigationActive)
        {
            agent.SetDestination(Player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Personaje"))
        {
            agent.enabled = true;
            navigationActive = true;
        }
    }
}
