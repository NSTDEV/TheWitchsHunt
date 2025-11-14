using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private float movementSpeed = 5f;
      [SerializeField] private float stopDistance = 1.5f;

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
        /* if (navigationActive)
        {
            agent.SetDestination(Player.position);
        }*/

        if (navigationActive && Player != null)
        {
            float distance = Vector3.Distance(transform.position, Player.position);

            if (distance > stopDistance)
            {
                // 🔹 Calculamos un punto a la distancia adecuada
                Vector3 direction = (Player.position - transform.position).normalized;
                Vector3 targetPos = Player.position - direction * stopDistance;

                agent.SetDestination(targetPos);
            }
            else
            {
                // 🔹 Si ya está a la distancia deseada, frena
                agent.ResetPath();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            agent.enabled = true;
            navigationActive = true;

            GameObject targetObject = GameObject.FindGameObjectWithTag("Personaje");
            if (targetObject != null)
            {
                Player = targetObject.transform;
            }
        }
    }
}
