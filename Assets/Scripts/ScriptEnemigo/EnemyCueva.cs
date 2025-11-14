using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [Header("Scripts a activar al detectar al jugador")]
    [SerializeField] private MonoBehaviour[] scriptsAActivar;

    [Header("Scripts a desactivar al detectar al jugador (opcional)")]
    [SerializeField] private MonoBehaviour[] scriptsADesactivar;

    [Header("Tiempo de retraso")]
    [SerializeField] private float delay = 0f;

    private bool activado = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activado) return;

        if (collision.CompareTag("Personaje"))
        {
            activado = true;
            StartCoroutine(ActivarDespuesDeDelay());
        }
    }

    private System.Collections.IEnumerator ActivarDespuesDeDelay()
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        // activar
        foreach (var s in scriptsAActivar)
            if (s != null) s.enabled = true;

        // desactivar
        foreach (var s in scriptsADesactivar)
            if (s != null) s.enabled = false;

        Debug.Log("🔥 EnemyActivator → Scripts activados/desactivados correctamente");
    }
}
