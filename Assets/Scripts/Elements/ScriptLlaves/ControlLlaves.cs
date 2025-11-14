using UnityEngine;
using TMPro;

public class ControlLlaves : MonoBehaviour
{
    public static ControlLlaves instance; // 🔹 acceso global
    public int maxLlaves = 10;
    public int llavesActuales = 0;
    public TextMeshProUGUI textoLlavesUI;
    [SerializeField] private AudioClip keySound1;
    [SerializeField] private AudioClip keySound2;
    [SerializeField] private AudioClip keySound3;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && llavesActuales > 0)
        {
            llavesActuales--;
            ActualizarTexto();
            SonidoLlaves();
            Debug.Log("Se usó una llave");
        }
    }

    public void RecogerLlave()
    {
        if (llavesActuales < maxLlaves)
        {
            llavesActuales++;
            ActualizarTexto();
            Debug.Log("Llaves: " + llavesActuales);
        }
        else
        {
            Debug.Log("No puedo llevar más llaves");
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
        switch (llavesActuales)
        {
            case 1: SoundManager.instance.EjecutarSonido(keySound1); break;
            case 2: SoundManager.instance.EjecutarSonido(keySound2); break;
            case 3: SoundManager.instance.EjecutarSonido(keySound3); break;
        }
    }
}
