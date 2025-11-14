using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D))]
public class EnemyGrainTrigger : MonoBehaviour
{
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
            filmGrain.intensity.value = Mathf.Lerp(
                filmGrain.intensity.value,
                targetIntensity,
                Time.deltaTime * smoothSpeed
            );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || triggerCollider == null) return;

        if (other.CompareTag("Enemigo"))
        {
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
            targetIntensity = normalGrain;

            if (splitToning != null)
                splitToning.active = false;
        }
    }
}
