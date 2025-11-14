using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CuevaBrujaIA : MonoBehaviour
{
    [SerializeField] private string targetTag = "Personaje";
    [SerializeField] private float witchSpeed = 2.5f;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform target;
    private NavMeshAgent agent;

    private bool canChase = false;   // 🔹 Nueva variable: control de persecución
        //Bruja mod
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = witchSpeed;
        agent.enabled = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;

            // 🔹 Nos suscribimos al trigger del personaje
            MovPersonaje trigger = target.GetComponent<MovPersonaje>();
            if (trigger != null)
            {
                trigger.OnCollarCollision += ActivateChase;
                
            }
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag: " + targetTag);
        }
    }

    private void ActivateChase()
    {
       StartCoroutine(Perseguir());
    }

    void Update()
    {
        if (!canChase || target == null) return;
        Debug.Log("Bruja Activada");
        Vector2 origin = transform.position;
        Vector2 direction = ((Vector2)target.position - origin).normalized;

        agent.SetDestination(target.position);
        agent.speed = witchSpeed;

        // --- Animaciones ---
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetBool("EnemigoMoviendoLado", true);
            animator.SetBool("EnemigoMoviendoArriba", false);
            animator.SetBool("EnemigoMoviendoAbajo", false);
            sr.flipX = direction.x < 0;
        }
        else if (direction.y > 0)
        {
            animator.SetBool("EnemigoMoviendoArriba", true);
            animator.SetBool("EnemigoMoviendoLado", false);
            animator.SetBool("EnemigoMoviendoAbajo", false);
        }
        else if (direction.y < 0)
        {
            animator.SetBool("EnemigoMoviendoAbajo", true);
            animator.SetBool("EnemigoMoviendoArriba", false);
            animator.SetBool("EnemigoMoviendoLado", false);
        }
    }
   private IEnumerator Perseguir()
    {
        yield return new WaitForSeconds(2f);
        canChase = true;  // 🔹 Ahora la bruja empieza a perseguir
        agent.enabled = true;

    }
}
