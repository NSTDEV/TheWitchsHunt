using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float movementSpeed = 5f;

    private UnityEngine.AI.NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = movementSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        agent.SetDestination(Player.position);
    }
}
