using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        // Animaciones
        animator.SetBool("MoviendoLado", inputX != 0);
        animator.SetBool("MoviendoArriba", inputY > 0);
        animator.SetBool("MoviendoAbajo", inputY < 0);
        animator.SetBool("QuietoLado", inputX == 0 && inputY == 0);
        animator.SetBool("QuietoArriba", inputX == 0 && inputY == 0);

        // Flip
        if (inputX < 0 && isFacingRight)
        {
            Flip();
        }
        else if (inputX > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }
}
