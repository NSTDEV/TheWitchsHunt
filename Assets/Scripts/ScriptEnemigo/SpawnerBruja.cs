using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerBruja2 : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint;
    public float spawnMinInterval = 10f;
    public float spawnMaxInterval = 15f;
    private float spawnInterval;
    public float detectionRadius = 1f; // para coomprobar colisiones con "Bosque"
    public int poolSize = 10; // cantidad e enemigos

    private List<GameObject> enemyPool;
    private void Awake()
    {
        spawnInterval = 7f;
    }

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
        //InvokeRepeating("SpawnEnemy", 5f, Random.Range(spawnMinInterval, spawnMaxInterval));

        InvokeRepeating("SpawnEnemy", 5f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            spawnInterval = Random.Range(spawnMinInterval, spawnMaxInterval);
            Debug.Log(spawnInterval);
            // comprobar posicion de spawner NO overlap con "bosque"
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, detectionRadius);
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Bosque"))
                {
                //Debug.Log("no se puede instanciar sobre bosque");
                
                StartCoroutine(RetrySpawn());
                return;
                }
            }

            /*Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, detectionRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Bosque"))
                {
                    Debug.Log("No se puede instanciar");
                    StartCoroutine(RetrySpawn());
                    return;
                }
            }*/

            // instancia un enemigo no "instanciado del array"
            foreach (GameObject enemy in enemyPool)
            {
                if (!enemy.activeInHierarchy)
                {
                    //enemy.transform.position = spawnPoint.position;
                    
                    enemy.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0f);
                    enemy.transform.rotation = spawnPoint.rotation;
                    enemy.SetActive(true);
                    return;
                }
            }

            //Debug.Log("Enemigos ON");
        }
    }

    IEnumerator RetrySpawn()
    {
        yield return new WaitForSeconds(1f);

        // cambia posicion de spawner para evitar "bosque"
        spawnPoint.position += new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
        //spawnPoint.position += new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        

        // se intentar instanciar (o reactivar) de nuevo
        SpawnEnemy();
    }
}

