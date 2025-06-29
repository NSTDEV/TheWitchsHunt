using UnityEngine;
using TMPro;
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
                enemigo.SetActive(false);
                collaresActuales--;
                ActualizarTexto();
                break;
            }
        }
    }
}

void ActualizarTexto()
{
    if (textoUI != null)
    {
        textoUI.text = "Collares: " + collaresActuales;
    }
}
}
