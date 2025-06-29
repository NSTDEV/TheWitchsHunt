/*using UnityEngine;

public class MovEnemigo : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 15f;
    private Transform target;

    void Update()
    {

        GameObject personaje = GameObject.FindGameObjectWithTag("Personaje");
        if (personaje != null)
        {
            target = personaje.transform;

            Vector3 direction = target.position - transform.position;
            direction.y = 0f; //pegar enemigo al suelo

            // raycast desde el enemigo hacia el personaje
            if (Physics.Raycast(transform.position, direction.normalized, out RaycastHit hit, detectionRange))
            {
                if (hit.transform.CompareTag("Personaje")) //raycast impacta al personaje, seguirlo
                {
                    transform.position += direction.normalized * speed * Time.deltaTime;
                    transform.LookAt(target); // enemigo avanza hacia el personaje
                }
            }
        }
    }
}*/

using UnityEngine;

public class MovEnemigo : MonoBehaviour
{
    public float speed = 3f;
    public float detectionRange = 10f;
    private Transform target;

    void Update()
    {
        GameObject personaje = GameObject.FindGameObjectWithTag("Personaje");
        if (personaje != null)
        {
            target = personaje.transform;

            Vector2 origin = transform.position;
            Vector2 direction = ((Vector2)target.position - origin).normalized;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectionRange);

            if (hit.collider != null && hit.collider.CompareTag("Personaje"))
            {
                Debug.DrawLine(origin, hit.point, Color.red); 

                
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }
}