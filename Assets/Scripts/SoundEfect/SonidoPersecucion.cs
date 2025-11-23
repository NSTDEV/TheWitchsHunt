using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPersecucion : MonoBehaviour
{
     [Header("Configuración de audio")]
    public AudioSource musicaPersecucion; // 2D, Loop ON, PlayOnAwake OFF
    public float volumenMaximo = 1f;
    public float fadeSpeed = 1.5f;

    [Header("Tag del enemigo")]
    public string enemyTag = "Enemigo"; // Asegurate de ponerle este tag al prefab

    private void Update()
    {
        ControlarMusica();
    }

    void ControlarMusica()
    {
        if (musicaPersecucion == null) return;

        bool enemigoActivo = HayEnemigosActivos();

        if (enemigoActivo)
        {
            // FADE IN
            if (!musicaPersecucion.isPlaying)
                musicaPersecucion.Play();

            musicaPersecucion.volume = Mathf.MoveTowards(
                musicaPersecucion.volume,
                volumenMaximo,
                Time.deltaTime * fadeSpeed
            );
        }
        else
        {
            // FADE OUT
            musicaPersecucion.volume = Mathf.MoveTowards(
                musicaPersecucion.volume,
                0f,
                Time.deltaTime * fadeSpeed
            );

            if (musicaPersecucion.volume <= 0f && musicaPersecucion.isPlaying)
                musicaPersecucion.Stop();
        }
    }

    bool HayEnemigosActivos()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemigo in enemigos)
        {
            if (enemigo.activeInHierarchy)
                return true;
        }

        return false;
    }
}
