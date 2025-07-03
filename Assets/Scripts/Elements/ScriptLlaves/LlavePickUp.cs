using UnityEngine;

public class LlavePickup : MonoBehaviour
{
    [SerializeField] private AudioClip keySound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            SoundManager.instance.EjecutarSonido(keySound);
            ControlLlaves control = other.GetComponent<ControlLlaves>();
            if (control != null)
            {
                control.RecogerLlave();
            }
            Destroy(gameObject);
            //gameObject.SetActive(false); // desaparecer la llave
        }
    }
}
