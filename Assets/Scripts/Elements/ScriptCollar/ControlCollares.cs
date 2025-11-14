using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;   // ← IMPORTANTE

public class ControlCollares : MonoBehaviour
{
    public static ControlCollares instance; // 🔹 acceso global
    
    public int maxCollares = 10;
    public int collaresActuales = 0;
    public TextMeshProUGUI textoUI;

    void Awake()
    {
        // 🔹 Si ya hay una instancia, eliminar duplicado
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 🔹 se mantiene entre escenas
    }

    public void RecogerCollar()
    {
        if (collaresActuales < maxCollares)
        {
            collaresActuales++;
            ActualizarTexto();
        }
        else
        {
            Debug.Log("No puedo llevar más collares");
        }
    }

    void Update()
    {
        // 🚫 NO FUNCIONA EL COLLAR EN LA ESCENA “Cueva”
        if (SceneManager.GetActiveScene().name == "Cueva")
            return;

        // ✔️ Funciona normalmente en cualquier otra escena
        if (Input.GetKeyDown(KeyCode.J) && collaresActuales > 0)
        {
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo.activeInHierarchy)
                {
                    collaresActuales--;
                    ActualizarTexto();
                    StartCoroutine(Blink(enemigo));
                    break;
                }
            }
        }
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
