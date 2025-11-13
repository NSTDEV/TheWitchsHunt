using UnityEngine;

public class LlavePickup : MonoBehaviour
{
    [SerializeField] private AudioClip keySound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            if (keySound != null)
                SoundManager.instance.EjecutarSonido(keySound);

            if (ControlLlaves.instance != null)
                ControlLlaves.instance.RecogerLlave();

            Destroy(gameObject);
        }
    }
}
