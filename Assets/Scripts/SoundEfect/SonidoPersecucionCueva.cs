using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoPersecucionCueva : MonoBehaviour
{
    public Transform jugador;      

    [Header("Pooling (el mismo prefab que usa tu Spawner)")]
    public GameObject enemyPrefab;  

    [Header("Configuración de sonido")]
    public AudioSource audioSource;
    public float distanciaMaxima = 25f;     // a esta distancia el sonido es mínimo
    public float distanciaMinima = 3f;      // a esta distancia el sonido es máximo

    private List<GameObject> enemigosEnEscena = new List<GameObject>();

    void Start()
    {
        // Buscar todos los enemigos instanciados en la escena al iniciar
        GameObject[] encontrados = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in encontrados)
        {
            if (obj.name.Contains(enemyPrefab.name))
            {
                enemigosEnEscena.Add(obj);
            }
        }
    }

    void Update()
    {
        GameObject enemigoCercano = ObtenerEnemigoActivoMasCercano();

        if (enemigoCercano == null)
        {
            audioSource.volume = 0;
            return;
        }

        float distancia = Vector3.Distance(jugador.position, enemigoCercano.transform.position);

        // Normalizar volumen
        float volumen = 1f - Mathf.InverseLerp(distanciaMinima, distanciaMaxima, distancia);
        volumen = Mathf.Clamp(volumen, 0f, 1f);

        audioSource.volume = volumen;
    }

    GameObject ObtenerEnemigoActivoMasCercano()
    {
        float menorDistancia = Mathf.Infinity;
        GameObject enemigoMasCercano = null;

        foreach (GameObject enemigo in enemigosEnEscena)
        {
            if (enemigo != null && enemigo.activeInHierarchy)
            {
                float dist = Vector3.Distance(jugador.position, enemigo.transform.position);

                if (dist < menorDistancia)
                {
                    menorDistancia = dist;
                    enemigoMasCercano = enemigo;
                }
            }
        }

        return enemigoMasCercano;
    }
}
