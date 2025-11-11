using UnityEngine;
using UnityEngine.SceneManagement; 

public class MovPersonaje : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    public AudioSource pasos;          

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

         // Si te olvidaste de asignarlo en el inspector
        if (pasos == null) pasos = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Obtener dirección de entrada
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(inputX, inputY);

        // Normalizar para que moverse en diagonal no sea más rápido
        if (inputDir.magnitude > 1)
            inputDir = inputDir.normalized;

        // Aplicar movimiento
        transform.position += (Vector3)(inputDir * speed * Time.deltaTime);


        // -------- AUDIO DE PASOS --------
        bool estaCaminando = inputDir.sqrMagnitude > 0.001f;

        if (estaCaminando && !pasos.isPlaying)
            pasos.Play();
        else if (!estaCaminando && pasos.isPlaying)
            pasos.Stop();

        
         //animacion de moverse hacia derecha o izquierda
        if (Input.GetAxis("Horizontal") != 0) // se mueve hacia derecha/
        {
            animator.SetBool("MoviendoLado", true); // activar la animacion de caminar
            animator.SetBool("MoviendoArriba", false);
            animator.SetBool("QuietoLado", false);
            animator.SetBool("QuietoArriba", false);
            animator.SetBool("MoviendoAbajo", false);
        }
        else
        {
            animator.SetBool("MoviendoLado", false); // desactivar la animacion de caminar
            animator.SetBool("QuietoLado", true); // el personaje permanece en animacion quieto lado
            animator.SetBool("QuietoArriba", false);
        }

        //animacion de moverse hacia arriba
        if (Input.GetAxis("Vertical") > 0) // comparamos el valor del movimiento (-1 / 0 / 1)
        {
            animator.SetBool("QuietoArriba", false);
            animator.SetBool("MoviendoArriba", true); // activar la animacion de caminar
            animator.SetBool("MoviendoLado", false);
            animator.SetBool("QuietoLado", false);
            animator.SetBool("MoviendoAbajo", false);
        }
        else
        {
            animator.SetBool("QuietoArriba", true); // desactivar la animacion de caminar
            animator.SetBool("MoviendoArriba", false);
        }

        //animacion de moverse hacia abajo
        if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetBool("MoviendoAbajo", true); // activar la animacion de caminar
            animator.SetBool("QuietoArriba", false);
            animator.SetBool("QuietoLado", false);
            animator.SetBool("MoviendoLado", false);
        }
        else
        {
            animator.SetBool("MoviendoAbajo", false); // desactivar la animacion de caminar

        }

        if (inputX < 0 && isFacingRight)
        {
            Flip();
        }
        //giro del personaje si se mueve a la derecha
        else if (inputX > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    // cambiamos la escala en el eje X para voltear el personaje
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

      // --- Detectar colisión físicamente (colliders NO en trigger) ---
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemigo"))
        {
            SceneManager.LoadScene("EscenaLose");
        }
    }
}



