using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NewBehaviourScript : MonoBehaviour
{
    public string nombreEscena;
    public float tiempoEspera = 2f;

    public void AlPresionarBoton()
    {
        Time.timeScale = 1;
        StartCoroutine(EspereYCambie());
    }

    private IEnumerator EspereYCambie()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene(nombreEscena);
    }
}
