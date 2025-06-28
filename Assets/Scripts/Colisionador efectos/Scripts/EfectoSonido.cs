using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    [Tooltip("Clips que se reproducirán aleatoriamente")]
    public AudioClip[] clips;

    [Tooltip("AudioSource opcional: si lo dejas vacío, se creará uno temporal")]
    public AudioSource source;

    [Tooltip("Volumen lineal 0‑1 (1 = 100 %)")]      // NUEVO
    [Range(0f, 1f)]                                  // NUEVO: deslizador en el Inspector
    public float volume = 2f;                         // NUEVO

    [Tooltip("Tiempo de enfriamiento entre disparos (segundos)")]
    public float cooldown = 30f;

    private bool ready = true;    // ¿Puede dispararse ahora?

    private void OnTriggerEnter(Collider other)
    {
        // 1️⃣  Salimos si aún está en enfriamiento
        if (!ready) return;

        // 2️⃣  Nos aseguramos de que sea el jugador
        if (!other.CompareTag("Player")) return;

        // 3️⃣  Elegimos un clip al azar
        if (clips.Length == 0) return;                   // Evita excepciones
        AudioClip elegido = clips[Random.Range(0, clips.Length)];

        // 4️⃣  Lo reproducimos
        if (source == null)
        {
            // Crea una fuente de audio “descartable” en la posición del trigger
            AudioSource.PlayClipAtPoint(elegido, transform.position);
        }
        else
        {
            source.clip = elegido;
            source.Play();
        }

        // 5️⃣  Iniciamos el enfriamiento
        StartCoroutine(Cooldown());
    }

    private System.Collections.IEnumerator Cooldown()
    {
        ready = false;                    // Bloquea nuevas reproducciones
        yield return new WaitForSeconds(cooldown);
        ready = true;                     // ¡Listo para el siguiente paso!
    }
}
