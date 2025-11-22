using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // o usando UnityEngine.UI si usas texto normal

public class NotasManager : MonoBehaviour
{
    public GameObject panelNota;
    public TextMeshProUGUI textoNotaTMP;

    [Header("Efectos de sonido")]
    public AudioSource audioSource;       // ← arrastrá un AudioSource aquí
    public AudioClip sonidoAbrir;         // ← sonido de hoja abriéndose
    public AudioClip sonidoCerrar;        // ← opcional: hoja cerrándose

    private bool notaAbierta = false;
    private NotaInteractiva notaActual;

    void Start()
    {
        panelNota.SetActive(false);
    }

    public void MostrarNota(string texto, NotaInteractiva nota)
    {
        if (notaAbierta) return;

        notaAbierta = true;
        notaActual = nota;

        panelNota.SetActive(true);
        textoNotaTMP.text = texto;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.Pause();
        }

        if (audioSource != null && sonidoAbrir != null)
            audioSource.PlayOneShot(sonidoAbrir);

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (notaAbierta && Input.GetMouseButtonDown(0))
        {
            CerrarNota();
        }
    }

    public void CerrarNota()
    {
        notaAbierta = false;
        panelNota.SetActive(false);
        Time.timeScale = 1f;

        // 🎵 Reproducir sonido de cerrar (opcional)
        if (audioSource != null && sonidoCerrar != null)
            audioSource.PlayOneShot(sonidoCerrar);

        if (notaActual != null)
        {
            Destroy(notaActual.gameObject);
            notaActual = null;
        }

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.UnPause();
        }
    }
}
