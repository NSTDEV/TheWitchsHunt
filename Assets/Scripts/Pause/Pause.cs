using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pausa;
   // [SerializeField] private GameObject Panel;
    private bool juegoPausado = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {  
        juegoPausado = true;
        Pausa.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Reanudar()
    {   
        juegoPausado = false;
        Pausa.SetActive(false);
        Time.timeScale = 1f;
    }
     

    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void VolverMenu(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    public void Cerrar()
    {
        Application.Quit();
    }
}
