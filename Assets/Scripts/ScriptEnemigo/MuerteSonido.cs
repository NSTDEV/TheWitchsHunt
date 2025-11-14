using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSonido : MonoBehaviour
{
    [SerializeField] private AudioClip screamDeath; // sonido al desactivarse
    private AudioSource audioSource;

    private void Start()
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
