using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlCollares : MonoBehaviour
{
    public static ControlCollares instance;
    
    public int maxCollares = 10;
    public int collaresActuales = 0;
    public TextMeshProUGUI textoUI;

    public AudioSource sonidoCollar; // 🔊 AUDIO DEL COLLAR

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
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
        if (Input.GetKeyDown(KeyCode.Space) && collaresActuales > 0)
        {

            // 🔊 REPRODUCIR SONIDO DEL COLLAR 
            if (sonidoCollar != null) 
            sonidoCollar.Play();
            collaresActuales--;

            ActualizarTexto();

            if (RangeControll.instance != null && RangeControll.instance.HayEnemigos())
            {
                GameObject enemigo = RangeControll.instance.ObtenerPrimerEnemigo();

                if (enemigo != null)
                {
                    StartCoroutine(Blink(enemigo));
                }
            }
            else
            {
                Debug.Log("No hay enemigos en rango, pero se gastó 1 collar.");
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
