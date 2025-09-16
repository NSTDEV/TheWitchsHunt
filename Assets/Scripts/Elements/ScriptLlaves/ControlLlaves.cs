using UnityEngine;
using TMPro;

public class ControlLlaves : MonoBehaviour
{
    public int maxLlaves = 10;
    public int llavesActuales = 0;
    public TextMeshProUGUI textoLlavesUI;
    [SerializeField] private AudioClip keySound1;
    [SerializeField] private AudioClip keySound2;
    [SerializeField] private AudioClip keySound3;

    void Update()
    {
        // logica "puede abrir puerta al apretar E, si se tiene una llave"
        if (Input.GetKeyDown(KeyCode.E) && llavesActuales > 0)
        {
            llavesActuales--;
            ActualizarTexto();
            SonidoLlaves();
            Debug.Log("se uso llave : ");
        }
    }

    public void RecogerLlave()
    {
        if (llavesActuales < maxLlaves)
        {
            llavesActuales++;
            ActualizarTexto();
            Debug.Log(" " + llavesActuales);
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
            textoLlavesUI.text = " : " + llavesActuales;
        }
    }

    void SonidoLlaves()
    {
        if (llavesActuales == 1)
        {
            SoundManager.instance.EjecutarSonido(keySound1);
        }
        else if (llavesActuales == 2)
        {
            SoundManager.instance.EjecutarSonido(keySound2);
        }
        else if (llavesActuales == 3)
        {
            SoundManager.instance.EjecutarSonido(keySound3);
        }
    }
}
