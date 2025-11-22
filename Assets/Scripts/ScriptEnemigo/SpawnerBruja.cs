using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBruja2 : MonoBehaviour
{
    [Header("Prefab y punto de spawn")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [Header("Intervalos de aparición")]
    public float spawnMinInterval = 18f;
    public float spawnMaxInterval = 25f;

    [Header("Detección de colisiones en el punto de spawn")]
    public float detectionRadius = 1f;

    [Header("Pool de enemigos")]
    public int poolSize = 10;

    private List<GameObject> enemyPool;

    private void Start()
    {
        enemyPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        StartCoroutine(IniciarSpawner());
    }

    IEnumerator IniciarSpawner()
    {
        yield return new WaitForSeconds(20f);

        while (true)
        {
            SpawnEnemy();

            float intervalo = Random.Range(spawnMinInterval, spawnMaxInterval);
            yield return new WaitForSeconds(intervalo);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
            return;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, detectionRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Bosque"))
            {
                StartCoroutine(RetrySpawn());
                return;
            }
        }

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

    }

    IEnumerator RetrySpawn()
    {
        yield return new WaitForSeconds(1f);
        SpawnEnemy();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnPoint.position, detectionRadius);
    }
}
