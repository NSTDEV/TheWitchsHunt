using UnityEngine;

public class LlavePickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            ControlLlaves control = other.GetComponent<ControlLlaves>();
            if (control != null)
            {
                control.RecogerLlave();
            }

            gameObject.SetActive(false); // desaparecer la llave
        }
    }
}
