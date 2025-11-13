using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NewBehaviourScript : MonoBehaviour
{
   // Nombre de la escena a la que vas a cambiar
    public string nombreEscena;
    // Tiempo de espera en segundos
    public float tiempoEspera = 6f;

    // Este método se puede llamar desde el botón
    public void AlPresionarBoton(string nombre)
    {
        StartCoroutine(EspereYCambie(nombre));
    }

    private IEnumerator EspereYCambie(string nombre)
    {
        // Espera el tiempo indicado
        yield return new WaitForSeconds(tiempoEspera);

        // Cambia de escena

    SceneManager.LoadScene(nombre);

    }
}
