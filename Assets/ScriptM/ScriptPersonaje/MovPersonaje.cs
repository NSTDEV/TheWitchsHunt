
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator; // referencia al componente Animator del Personaje
    private bool isFacingRight = true; //respresenta el valor de mirar a la derecha

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // obtenemos la referencia al componente Animator
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveX * speed, moveY * speed);

        //animacion de moverse hacia derecha o izquierda
         if (Input.GetAxis("Horizontal") != 0 ) // se mueve hacia derecha/
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
         if (Input.GetAxis("Vertical") > 0 ) // comparamos el valor del movimiento (-1 / 0 / 1)
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
         if (Input.GetAxis("Vertical") < 0 ) 
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

        // giro del personaje si se mueve hacia la izquierda
        if (moveX < 0 && isFacingRight)
        {
            Flip();
        }
        //giro del personaje si se mueve a la derecha
        else if (moveX >0 && !isFacingRight)
        {
            Flip();
        }
        
        
        // "salto"
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

     // cambiamos la escala en el eje X para voltear el personaje
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }
}

/*using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}*/