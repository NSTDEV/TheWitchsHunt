using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D))]
public class RangeControll : MonoBehaviour
{
    // ==========================================
    //   SISTEMA DE RANGO PARA ENEMIGOS (COLLAR)
    // ==========================================

    public static RangeControll instance; // acceso global al rango del jugador

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

    // ==========================================
    //   POST PROCESO
    // ==========================================

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

    // ==========================================
    //   INIT
    // ==========================================

    void Awake()
    {
        instance = this; // guardar instancia global del rango
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
    }

    // ==========================================
    //   TRIGGER
    // ==========================================

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
}
