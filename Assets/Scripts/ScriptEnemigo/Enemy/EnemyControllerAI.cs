/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAI : MonoBehaviour
{
     [SerializeField] private Transform target;


   private UnityEngine.AI.NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {

       agent.SetDestination(target.position);
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAI : MonoBehaviour
{
    [SerializeField] private string targetTag = "Personaje"; // Asegúrate de que el objeto tenga esta etiqueta
     private Animator animator;  

    private Transform target;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el tag: " + targetTag);
        }
    }

    void Update()
    {
         Vector2 origin = transform.position;
            Vector2 direction = ((Vector2)target.position - origin).normalized;
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
         if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetBool("EnemigoMoviendoLado", true);
            animator.SetBool("EnemigoMoviendoArriba", false);
            animator.SetBool("EnemigoMoviendoAbajo", false);

            // Flip X si camina a la izquierda
           /* Vector3 scale = transform.localScale;
            scale.x = direction.x > 0 ? 1 : -1;
            transform.localScale = scale;*/
            // Flip X según la dirección, respetando la escala original
Vector3 scale = transform.localScale;
scale.x = Mathf.Abs(scale.x) * (direction.x >= 0 ? 1 : -1);
transform.localScale = scale;
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
}