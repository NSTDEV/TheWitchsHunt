using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicController : MonoBehaviour
{
    [Header("Timeline")]
    public PlayableDirector director;

    [Header("Configuración")]
    public KeyCode teclaParaSaltar = KeyCode.Space;
    public float delayParaMostrarMensaje = 1.5f;

    [Header("UI Opcional")]
    public GameObject mensajeSkip;

    bool puedeSaltar = false;

    void Start()
    {
        if (mensajeSkip != null)
            mensajeSkip.SetActive(false);

        StartCoroutine(ActivarMensajeSkip());
        director.stopped += OnTimelineEnd;
    }

    IEnumerator ActivarMensajeSkip()
    {
        yield return new WaitForSeconds(delayParaMostrarMensaje);

        puedeSaltar = true;

        if (mensajeSkip != null)
            mensajeSkip.SetActive(true);
    }

    void Update()
    {
        if (!puedeSaltar) return;

        if (Input.GetKeyDown(teclaParaSaltar))
            SaltarCinematica();
    }

    void SaltarCinematica()
    {
        if (director == null) return;

        director.time = director.duration;
        director.Evaluate();
        director.Stop();
        
        OnTimelineEnd(director);
    }

    void OnTimelineEnd(PlayableDirector d)
    {

        Debug.Log("Cinemática finalizada o saltada.");

        if (mensajeSkip != null)
            mensajeSkip.SetActive(false);
    }
}
