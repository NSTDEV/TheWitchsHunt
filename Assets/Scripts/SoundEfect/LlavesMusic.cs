using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlavesMusic : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    private AudioSource musicSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
                musicSource.PlayOneShot(musicClip);
        }
    }
}
