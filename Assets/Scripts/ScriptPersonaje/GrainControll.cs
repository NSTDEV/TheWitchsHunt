using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyGrainTrigger : MonoBehaviour
{
    [Header("Post-proceso (Grain)")]
    public Volume globalVolume;
    public float normalGrain = 0.15f;
    public float alertGrain = 0.7f;
    public float smoothSpeed = 3f;

    [Header("Oscuridad (Sprite Renderer)")]
    public SpriteRenderer spriteRenderer;
    public Color normalColor = Color.black;
    public Color alertColor = new Color(0.6f, 0.1f, 0.1f);
    private FilmGrain filmGrain;
    private float targetIntensity;
    private Color targetColor;

    void Start()
    {
        if (globalVolume != null && globalVolume.profile.TryGet(out filmGrain))
        {
            filmGrain.intensity.value = normalGrain;
            targetIntensity = normalGrain;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = normalColor;
            targetColor = normalColor;
        }
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

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(
                spriteRenderer.color,
                targetColor,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            targetIntensity = alertGrain;
            targetColor = alertColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            targetIntensity = normalGrain;
            targetColor = normalColor;
        }
    }
}
