using UnityEngine;

public class LlavePickup : MonoBehaviour
{
    [SerializeField] private AudioClip keySound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            Debug.Log("Trigger detectado con el jugador");

            if (keySound != null && SoundManager.instance != null)
                SoundManager.instance.EjecutarSonido(keySound);

            if (ControlLlaves.instance != null)
                ControlLlaves.instance.RecogerLlave();
            else
                Debug.LogError("❌ ControlLlaves.instance es NULL");

            Destroy(gameObject);
        }

    }
}
