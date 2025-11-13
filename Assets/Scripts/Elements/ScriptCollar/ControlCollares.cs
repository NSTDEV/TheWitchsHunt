using UnityEngine;
using TMPro;
using System.Collections;

public class ControlCollares : MonoBehaviour
{
    public int maxCollares = 10;
    public int collaresActuales = 0;
    public TextMeshProUGUI textoUI;

    //Header("Raycast y distancia")]
    [SerializeField] private Transform jugador;        // referencia al jugador
    [SerializeField] private float distanciaMax = 5f; // ajustable en el Inspector
    [SerializeField] private LayerMask enemigoLayer;   // para filtrar solo enemigos en el raycast

    

void Start()
    {
        ActualizarTexto();

    }

    public void RecogerCollar()
    {
        if (collaresActuales < maxCollares)
        {
            collaresActuales++;
            ActualizarTexto();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && collaresActuales > 0)
        {
            GameObject enemigoCercano = BuscarEnemigoCercano();
            if (enemigoCercano != null)
            {
                
                collaresActuales--;
                ActualizarTexto();
                StartCoroutine(Blink(enemigoCercano));
            }
            /*GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo.activeInHierarchy)
                {
                    collaresActuales--;
                    ActualizarTexto();


                    StartCoroutine(Blink(enemigo));
                    break;
                }
            }*/
        }
    }

    /***********/
    GameObject BuscarEnemigoCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo.activeInHierarchy)
            {
                float distancia = Vector2.Distance(jugador.position, enemigo.transform.position);
                if (distancia <= distanciaMax)
                {
                    // Opcional: trazar raycast para depuración visual
                    Debug.DrawLine(jugador.position, enemigo.transform.position, Color.red, 0.5f);

                    return enemigo;
                }
            }
        }
        return null;
    }

    IEnumerator Blink(GameObject enemigo)
    {
        Animator anim = enemigo.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("EnemigoGolpeado");
        }
        
        yield return new WaitForSeconds(1f);
        enemigo.SetActive(false);
    }


    void ActualizarTexto()
    {
        if (textoUI != null)
        {
            textoUI.text = " : " + collaresActuales;
        }
    }
}
