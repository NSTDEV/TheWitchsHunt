using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlCollares : MonoBehaviour
{
    public static ControlCollares instance;

    [Header("Sistema de carga")]
    public bool cargado = true;
    public float tiempoRecarga = 10f;
    public float minTiempo = 15f;
    public float maxTiempo = 20f;
    private bool recargando = false;

    [Header("UI")]
    public Image iconoCollar;
    public TextMeshProUGUI contadorUI;

    public AudioSource sonidoCollar;

    string[] escenasBloqueadas = { "MenuPrincipal2", "EscenaWin", "EscenaLose", "Cueva" };

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

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool bloqueada = false;
        foreach (string s in escenasBloqueadas)
            if (scene.name == s) bloqueada = true;

        if (bloqueada)
        {
            Resetear();
            OcultarUI();
        }
        else
        {
            MostrarUI();
        }

        ActualizarUI();
    }

    void Resetear()
    {
        cargado = true;
        recargando = false;
        StopAllCoroutines();
    }

    void OcultarUI()
    {
        if (iconoCollar != null)
        {
            cargado = false;
            iconoCollar.transform.gameObject.SetActive(false);
        }

        if (contadorUI != null)
            contadorUI.gameObject.SetActive(false);
    }

    void MostrarUI()
    {
        if (iconoCollar != null)
        {
            cargado = true;
            iconoCollar.transform.gameObject.SetActive(true);
        }

        if (contadorUI != null)
            contadorUI.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Time.timeScale != 1) {return;}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IntentarUsarCollar();
        }
    }

    void IntentarUsarCollar()
    {
        if (!cargado || recargando)
        {
            Debug.Log("❌ Collar no está listo");
            return;
        }

        DispararCollar();
    }

    void DispararCollar()
    {
        cargado = false;

        tiempoRecarga = Random.Range(minTiempo, maxTiempo);

        if (sonidoCollar != null)
            sonidoCollar.Play();

        if (RangeControll.instance != null && RangeControll.instance.HayEnemigos())
        {
            GameObject enemigo = RangeControll.instance.ObtenerPrimerEnemigo();
            if (enemigo != null)
                StartCoroutine(Blink(enemigo));
        }
        else
        {
            Debug.Log("📌 Collar gastado, pero NO había enemigo en rango");
        }

        StartCoroutine(Recargar());
        ActualizarUI();
    }

    IEnumerator Recargar()
    {
        recargando = true;

        float restante = tiempoRecarga;

        while (restante > 0f)
        {
            restante -= Time.deltaTime;

            if (contadorUI != null)
                contadorUI.text = Mathf.Ceil(restante).ToString();

            if (iconoCollar != null)
            {
                Color c = iconoCollar.color;
                c.a = 0.3f;
                iconoCollar.color = c;
            }

            yield return null;
        }

        cargado = true;
        recargando = false;
        ActualizarUI();
    }

    IEnumerator Blink(GameObject enemigo)
    {
        Animator anim = enemigo.GetComponent<Animator>();
        if (anim != null)
            anim.Play("EnemigoGolpeado");

        yield return new WaitForSeconds(1f);

        enemigo.SetActive(false);
    }

    void ActualizarUI()
    {
        if (contadorUI != null)
            contadorUI.text = cargado ? "" : tiempoRecarga.ToString("0");

        if (iconoCollar != null)
        {
            Color c = iconoCollar.color;
            c.a = cargado ? 1f : 0.3f;
            iconoCollar.color = c;
        }
    }
}
