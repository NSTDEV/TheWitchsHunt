using UnityEngine;
using TMPro;
using System.Collections;

public class ControlCollares : MonoBehaviour
{
    public int maxCollares = 10;
    public int collaresActuales = 0;
    public TextMeshProUGUI textoUI;

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
