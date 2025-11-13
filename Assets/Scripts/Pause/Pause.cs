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
        if (Time.timeScale != 1f) { return; }
        juegoPausado = true;
        Pausa.SetActive(true);
        Time.timeScale = 0f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.Pause();
        }

    }

    public void Reanudar()
    {   
        juegoPausado = false;
        Pausa.SetActive(false);
        Time.timeScale = 1f;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource a in audios)
        {
            a.UnPause();
        }
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
