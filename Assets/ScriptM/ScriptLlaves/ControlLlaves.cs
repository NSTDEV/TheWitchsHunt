using UnityEngine;
using TMPro;

public class ControlLlaves : MonoBehaviour
{
    public int maxLlaves = 10;
    public int llavesActuales = 0;
    public TextMeshProUGUI textoLlavesUI;

    void Update()
    {
        // logica "puede abrir puerta al apretar E, si se tiene una llave"
        if (Input.GetKeyDown(KeyCode.E) && llavesActuales > 0)
        {
            llavesActuales--;
            ActualizarTexto();
            Debug.Log("se uso llave : ");
        }
    }

    public void RecogerLlave()
    {
        if (llavesActuales < maxLlaves)
        {
            llavesActuales++;
            ActualizarTexto();
            Debug.Log("Llaves Total: " + llavesActuales);
        }
        else
        {
            Debug.Log("no puedo llevar mas llaves");
        }
    }

    void ActualizarTexto()
    {
        if (textoLlavesUI != null)
        {
            textoLlavesUI.text = "Llaves: " + llavesActuales;
        }
    }
}
