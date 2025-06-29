using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobanEfectoSonido : MonoBehaviour
{
   public AudioClip[] clips;
    public AudioSource source;
    [Range(0f, 100f)]
    public float volume = 100f;
    public float cooldown = 30f;

    private static bool globalReady = true;  // ⬅ NUEVO: compartido entre todos

    private void OnTriggerEnter(Collider other)
    {
        if (!globalReady) return;                        // ⬅ NUEVO: todos comparten esto
        if (!other.CompareTag("Player")) return;
        if (clips.Length == 0) return;

        AudioClip elegido = clips[Random.Range(0, clips.Length)];

        if (source == null)
        {
            AudioSource.PlayClipAtPoint(elegido, transform.position, volume);
        }
        else
        {
            source.volume = volume;
            source.PlayOneShot(elegido);
        }

        StartCoroutine(GlobalCooldown());               // ⬅ NUEVO
    }

    private System.Collections.IEnumerator GlobalCooldown()
    {
        globalReady = false;
        yield return new WaitForSeconds(cooldown);
        globalReady = true;
    }
}
