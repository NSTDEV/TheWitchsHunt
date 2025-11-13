using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private AudioClip screamDeath; // sonido al desactivarse
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        if (audioSource != null && screamDeath != null)
        {
            AudioSource.PlayClipAtPoint(screamDeath, transform.position);
        }
    }
}

