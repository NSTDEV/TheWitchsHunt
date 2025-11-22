using UnityEngine;

public class OrbitaSpawner2 : MonoBehaviour
{
    public Transform target; // personaje
    public float orbitRadius = 5f; // distancia de orbita
    public float orbitSpeed = 2f; // velocidad de orbita

    private float angle = 0f;

    void Update()
    {
        if (target != null)
        {
            angle += orbitSpeed * Time.deltaTime;

            float x = target.position.x + Mathf.Cos(angle) * orbitRadius;
            float y = target.position.y + Mathf.Sin(angle) * orbitRadius;

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
