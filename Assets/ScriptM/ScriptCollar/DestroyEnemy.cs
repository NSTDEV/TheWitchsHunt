
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Personaje"))
        {
            //crea array de enemigos activos
            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
            foreach (GameObject enemigo in enemigos)
            {
                if (enemigo.activeInHierarchy)
                {
                    enemigo.SetActive(false);
                    //break; // desactiva de a uno, para desactivar en grupo comentar break
                }
            }

            // desactiva collar al ser recogido
            gameObject.SetActive(false);
        }
    }
}
/*using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Personaje"))
        {
            GameObject enemigo = GameObject.FindGameObjectWithTag("Enemigo");
            if (enemigo != null)
            {
                Destroy(this.gameObject);
                Destroy(enemigo);
            }
        }
    }
}*/
