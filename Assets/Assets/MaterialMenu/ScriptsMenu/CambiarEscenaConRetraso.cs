using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NewBehaviourScript : MonoBehaviour
{
   // Nombre de la escena a la que vas a cambiar
    public string nombreEscena;
    // Tiempo de espera en segundos
    public float tiempoEspera = 2f;

    // Este método se puede llamar desde el botón
    public void AlPresionarBoton()
    {
        StartCoroutine(EspereYCambie());
    }

    private IEnumerator EspereYCambie()
    {
        // Espera el tiempo indicado
        yield return new WaitForSeconds(tiempoEspera);

        // Cambia de escena
        SceneManager.LoadScene(nombreEscena);
    }
}
