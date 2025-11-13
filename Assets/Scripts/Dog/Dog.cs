using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float runSpeed = 2f;
    private float walkSpeed = 1.5f;
    private float farRadius = 10f;
    private float closeRadius = 2f;
    private float stopRadius = 1.2f;

    private bool isFollowingPlayer = false;

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Activar seguimiento si entra en el radio lejano
        if (!isFollowingPlayer && distance <= farRadius)
        {
            isFollowingPlayer = true;
        }

        if (isFollowingPlayer)
        {
            if (distance > stopRadius)
            {
                Vector2 direction = (player.position - transform.position).normalized;

                float currentSpeed = (distance <= closeRadius) ? walkSpeed : runSpeed;

                transform.position += (Vector3)(direction * currentSpeed * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, farRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, closeRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopRadius);
    }
}
