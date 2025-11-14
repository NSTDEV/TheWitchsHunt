using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MovPersonaje : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    public AudioSource pasos;

    private bool isDead = false;
    private Vector2 lastMoveDir = Vector2.down;

     public System.Action OnCollarCollision;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (pasos == null) pasos = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(inputX, inputY);

        if (inputDir.magnitude > 1)
            inputDir.Normalize();

        // Movimiento
        transform.position += (Vector3)(inputDir * speed * Time.deltaTime);

        // -------- AUDIO DE PASOS --------
        bool estaCaminando = inputDir.sqrMagnitude > 0.001f;
        if (estaCaminando && !pasos.isPlaying)
            pasos.Play();
        else if (!estaCaminando && pasos.isPlaying)
            pasos.Stop();

        // -------- ANIMACIONES --------
        if (estaCaminando)
        {
            lastMoveDir = inputDir;

            if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
            {
                animator.SetBool("MoviendoLado", true);
                animator.SetBool("MoviendoArriba", false);
                animator.SetBool("MoviendoAbajo", false);
                animator.SetBool("QuietoLado", false);
                animator.SetBool("QuietoArriba", false);
            }
            else if (inputY > 0)
            {
                animator.SetBool("MoviendoArriba", true);
                animator.SetBool("MoviendoLado", false);
                animator.SetBool("MoviendoAbajo", false);
                animator.SetBool("QuietoLado", false);
                animator.SetBool("QuietoArriba", false);
            }
            else if (inputY < 0)
            {
                animator.SetBool("MoviendoAbajo", true);
                animator.SetBool("MoviendoArriba", false);
                animator.SetBool("MoviendoLado", false);
                animator.SetBool("QuietoLado", false);
                animator.SetBool("QuietoArriba", false);
            }
        }
        else
        {
            animator.SetBool("MoviendoLado", false);
            animator.SetBool("MoviendoArriba", false);
            animator.SetBool("MoviendoAbajo", false);

            if (Mathf.Abs(lastMoveDir.x) > Mathf.Abs(lastMoveDir.y))
            {
                animator.SetBool("QuietoLado", true);
                animator.SetBool("QuietoArriba", false);
            }
            else if (lastMoveDir.y > 0)
            {
                animator.SetBool("QuietoArriba", true);
                animator.SetBool("QuietoLado", false);
            }
            else
            {
                // quieto abajo
                animator.SetBool("QuietoArriba", false);
                animator.SetBool("QuietoLado", false);
            }
        }

        if (inputX < 0 && isFacingRight)
            Flip();
        else if (inputX > 0 && !isFacingRight)
            Flip();
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Llave"))
        {
            OnCollarCollision?.Invoke();
        }
        if (collision.collider.CompareTag("Enemigo"))
            StartCoroutine(Morir());

        if (collision.collider.CompareTag("Cueva") && ControlLlaves.instance.llavesActuales >= 1)
            SceneManager.LoadScene("Cueva");

        if (collision.collider.CompareTag("Exterior") && ControlLlaves.instance.llavesActuales >= 2)
        {
            SceneManager.LoadScene("BosqueParte2");
        }

        if (collision.collider.CompareTag("Salida") && ControlLlaves.instance.llavesActuales >= 3)
        {
            SceneManager.LoadScene("EscenaWin");
        }
    }
     private void OnTriggerEnter2D(Collider2D other)
{
   if (other.CompareTag("Llave"))
    {
        OnCollarCollision?.Invoke();
    }
}

    private IEnumerator Morir()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        isDead = true;
        pasos.Stop();

        animator.SetBool("MoviendoLado", false);
        animator.SetBool("MoviendoArriba", false);
        animator.SetBool("MoviendoAbajo", false);
        animator.SetBool("QuietoLado", false);
        animator.SetBool("QuietoArriba", false);
        animator.SetTrigger("Muerte");

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("EscenaLose");
    }
}
