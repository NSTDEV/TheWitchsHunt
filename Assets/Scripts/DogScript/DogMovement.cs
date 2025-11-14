using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private string playerTag = "Personaje";

    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float movementThreshold = 0.05f;
    [SerializeField] private float idleToSleepTime = 2f;

    private NavMeshAgent agent;
    private bool navigationActive = false;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    private float idleTimer = 0f;
    private Vector2 lastFacing = Vector2.right;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    private void Start()
    {
        GameObject t = GameObject.FindGameObjectWithTag(playerTag);
        if (t != null) Player = t.transform;

        SetSleeping();
    }

    private void Update()
    {
        if (!navigationActive || Player == null || agent == null)
            return;

        float distance = Vector3.Distance(transform.position, Player.position);

        Vector3 direction = (Player.position - transform.position).normalized;
        Vector3 targetPos = Player.position - direction * stopDistance;

        agent.isStopped = false;      // 🔥 FIX
        agent.SetDestination(targetPos); // 🔥 FIX

        // --- decidir por velocidad ---
        float speed = agent.velocity.magnitude;

        if (speed > movementThreshold)
        {
            idleTimer = 0f;
            SetWalking();

            Vector2 moveDir = new Vector2(agent.velocity.x, agent.velocity.y).normalized;
            if (moveDir.sqrMagnitude > 0.01f)
            {
                lastFacing = moveDir;
                sr.flipX = moveDir.x < 0;
            }
        }
        else
        {
            // está quieto = Idle
            SetIdle();

            idleTimer += Time.deltaTime;
            if (idleTimer >= idleToSleepTime)
                SetSleeping();

            sr.flipX = lastFacing.x < 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            navigationActive = true;

            if (Player == null)
            {
                GameObject t = GameObject.FindGameObjectWithTag(playerTag);
                if (t != null) Player = t.transform;
            }

            if (!agent.enabled) agent.enabled = true;

            idleTimer = 0f;
            SetIdle();
        }
    }

    private void SetWalking()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isSleeping", false);
    }

    private void SetIdle()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", true);
        animator.SetBool("isSleeping", false);
    }

    private void SetSleeping()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isSleeping", true);
    }

}
