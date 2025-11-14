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

    [Header("Oscuridad (Sprite Renderer)")]
    public SpriteRenderer spriteRenderer;
    public Color normalColor = Color.black;
    public Color alertColor = new Color(0.6f, 0.1f, 0.1f);

    private float targetIntensity;
    private Color targetColor;

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

        if (spriteRenderer != null)
            spriteRenderer.color = targetColor = normalColor;

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

        if (spriteRenderer != null)
            spriteRenderer.color = Color.Lerp(
                spriteRenderer.color,
                targetColor,
                Time.deltaTime * smoothSpeed
            );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || triggerCollider == null) return;

        if (other.CompareTag("Enemigo"))
        {
            targetIntensity = alertGrain;
            targetColor = alertColor;

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
            targetColor = normalColor;

            if (splitToning != null)
                splitToning.active = false;
        }
    }
}
