using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlLlaves : MonoBehaviour
{
    public static ControlLlaves instance;

    public int maxLlaves = 10;
    public int llavesActuales = 0;

    public TextMeshProUGUI textoLlavesUI;

    [SerializeField] private AudioClip keySound1;
    [SerializeField] private AudioClip keySound2;
    [SerializeField] private AudioClip keySound3;

    string[] escenasBloqueadas = { "MenuPrincipal2", "EscenaWin", "EscenaLose" };

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool esBloqueada = false;

        foreach (string s in escenasBloqueadas)
        {
            if (scene.name == s)
            {
                esBloqueada = true;
                break;
            }
        }

        if (esBloqueada)
        {
            ResetearLlaves();
            OcultarUI();
        }
        else
        {
            MostrarUI();
        }

        ActualizarTexto();
    }

    void ResetearLlaves()
    {
        Debug.Log("ControlLlaves → Reiniciando porque se cargó menú o final.");
        llavesActuales = 0;
    }

    void OcultarUI()
    {
        if (textoLlavesUI != null)
            textoLlavesUI.transform.gameObject.SetActive(false);
    }

    void MostrarUI()
    {
        if (textoLlavesUI != null)
            textoLlavesUI.transform.gameObject.SetActive(true);
    }

    public void RecogerLlave()
    {
        if (llavesActuales < maxLlaves)
        {
            llavesActuales++;
            ActualizarTexto();
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
