using UnityEngine;

public class CollarPickup : MonoBehaviour
{
    [SerializeField] private AudioClip necklaceSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            SoundManager.instance.EjecutarSonido(necklaceSound);
            ControlCollares control = other.GetComponent<ControlCollares>();
            if (control != null)
            {
                control.RecogerCollar();
            }

            gameObject.SetActive(false); //desactiva collar usado
        }
    }
}