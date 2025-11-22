using UnityEngine;

public class CollarPickup : MonoBehaviour
{
    [SerializeField] private AudioClip necklaceSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            if (necklaceSound != null && SoundManager.instance != null)
                SoundManager.instance.EjecutarSonido(necklaceSound);

            //if (ControlCollares.instance != null)
            //    ControlCollares.instance.RecogerCollar();
            //else
            //    Debug.LogError("❌ ControlLlaves.instance es NULL");

            //Destroy(gameObject);
        }
    }
}
