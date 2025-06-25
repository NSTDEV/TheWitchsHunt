using UnityEngine;

public class OrbitaSpawner : MonoBehaviour
{
    public Transform target; // personaje
    public float orbitRadius = 5f; // distancia de orbita
    public float orbitSpeed = 2f; // velocidad de orbita

    private float angle = 0f;

    void Update()
    {
        if (target != null)
        {
            // calculo movimiento de orbita
            angle += orbitSpeed * Time.deltaTime;

            // calcular posicion de usando seno y coseno
            float x = target.position.x + Mathf.Cos(angle) * orbitRadius;
            float z = target.position.z + Mathf.Sin(angle) * orbitRadius;

            // actualizar la posición del spawner
            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}