using UnityEngine;

public class CollarPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            ControlCollares control = other.GetComponent<ControlCollares>();
            if (control != null)
            {
                control.RecogerCollar();
            }

            gameObject.SetActive(false); //desactiva collar usado
        }
    }
}