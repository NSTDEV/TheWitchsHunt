

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBruja : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint;
    public float spawnInterval = 3f;
    public float detectionRadius = 1f; // para coomprobar colisiones con "Bosque"
    public int poolSize = 10; // cantidad e enemigos

    private List<GameObject> enemyPool;

    void Start()
    {
        // crea array de enemigos
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            // comprobar posicion de spawner NO overlap con "bosque"
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, detectionRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Bosque"))
                {
                    Debug.Log("No se puede instanciar");
                    StartCoroutine(RetrySpawn());
                    return;
                }
            }

            // instancia un enemigo no "instanciado del array"
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.transform.rotation = spawnPoint.rotation;
                    enemy.SetActive(true);
                    return;
                }
            }

            Debug.Log("Enemigos ON");
        }
    }

    IEnumerator RetrySpawn()
    {
        yield return new WaitForSeconds(1f);

        // cambia posicion de spawner para evitar "bosque"
        spawnPoint.position += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

        // se intentar instanciar (o reactivar) de nuevo
        SpawnEnemy();
    }
}

/*using System.Collections;
using UnityEngine;

public class SpawnerBruja : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint; 
    public float spawnInterval = 3f; 
    public float detectionRadius = 1f; 

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, detectionRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Bosque"))
                {
                    Debug.Log("No se puede instanciar");
                    StartCoroutine(RetrySpawn());
                    return;
                }
            }

            // Instanciar enemigo si la posición es válida
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    IEnumerator RetrySpawn()
    {
        yield return new WaitForSeconds(1f);

        spawnPoint.position += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

        SpawnEnemy();
    }
}*/