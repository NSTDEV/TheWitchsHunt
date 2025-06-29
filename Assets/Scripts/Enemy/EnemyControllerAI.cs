using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAI : MonoBehaviour
{
     [SerializeField] private Transform target;
   [SerializeField] private float speedAI = 2.3f;


   private UnityEngine.AI.NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        //agent.speed = speedAI;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {

        //agent.SetDestination(new Vector2(target.position.x, target.position.y));
       agent.SetDestination(target.position);
    }
}
