using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D))]
public class RangeControll : MonoBehaviour
{
    [Header("Audio - Latido del corazón")]
    public AudioSource heartAudio;

    public float minVolume = 0.2f;
    public float maxVolume = 0.8f;
    public float aceleracionLatido = 0.15f;
    float latidoProgreso = 0f;

    [Header("Latido por ritmo")]
    public float latidoMinInterval = 1.2f;
    public float latidoMaxInterval = 0.35f;

    float latidoTimer = 0f;

    public static RangeControll instance;
    private List<GameObject> enemigosEnRango = new List<GameObject>();

    public bool HayEnemigos()
    {
        return enemigosEnRango.Count > 0;
    }

    public GameObject ObtenerPrimerEnemigo()
    {
        if (enemigosEnRango.Count > 0)
            return enemigosEnRango[0];
        return null;
    }

    [Header("Post-proceso (Grain)")]
    public Volume globalVolume;
    public float normalGrain = 0.15f;
    public float alertGrain = 0.7f;
    public float smoothSpeed = 3f;

    private FilmGrain filmGrain;

    [Header("Split Toning (Activar / Desactivar)")]
    private SplitToning splitToning;

    private float targetIntensity;

    [Header("DEBUG")]
    public Collider2D triggerCollider;

    void Awake()
    {
        instance = this;
    }

    void Reset()
    {
        triggerCollider = GetComponent<Collider2D>();
        triggerCollider.isTrigger = true;
    }

    void Start()
    {
        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out filmGrain);
            globalVolume.profile.TryGet(out splitToning);
        }

        if (filmGrain != null)
            filmGrain.intensity.value = targetIntensity = normalGrain;

        if (splitToning != null)
            splitToning.active = false;

        if (triggerCollider == null)
            triggerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (filmGrain != null)
        {
            filmGrain.intensity.value = Mathf.Lerp(
                filmGrain.intensity.value,
                targetIntensity,
                Time.deltaTime * smoothSpeed
            );
        }

        ActualizarLatido();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || triggerCollider == null) return;

        if (other.CompareTag("Enemigo"))
        {
            // agregar enemigo al rango
            if (!enemigosEnRango.Contains(other.gameObject))
                enemigosEnRango.Add(other.gameObject);

            // post-proceso
            targetIntensity = alertGrain;

            if (splitToning != null)
                splitToning.active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null || triggerCollider == null) return;

        if (other.CompareTag("Enemigo"))
        {
            if (enemigosEnRango.Contains(other.gameObject))
                enemigosEnRango.Remove(other.gameObject);

            if (enemigosEnRango.Count == 0)
            {
                targetIntensity = normalGrain;

                if (splitToning != null)
                    splitToning.active = false;
            }
        }
    }

    void ActualizarLatido()
    {
        if (heartAudio == null) return;

        if (enemigosEnRango.Count == 0)
        {
            latidoTimer = 0f;
            latidoProgreso = 0f;

            if (heartAudio.isPlaying)
                heartAudio.Stop();

            return;
        }

        latidoProgreso += Time.deltaTime * aceleracionLatido;
        latidoProgreso = Mathf.Clamp01(latidoProgreso);

        float intervalo = Mathf.Lerp(latidoMinInterval, latidoMaxInterval, latidoProgreso);
        heartAudio.volume = Mathf.Lerp(minVolume, maxVolume, latidoProgreso);
        latidoTimer += Time.deltaTime;

        if (latidoTimer >= intervalo)
        {
            heartAudio.Stop();
            heartAudio.Play();
            latidoTimer = 0f;
        }
    }

}
