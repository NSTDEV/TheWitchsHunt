using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject Pausa;
    [SerializeField] private GameObject bottonPause;
    private bool juegoPausado = false;

    public void Pausar()
    { 
        if (Time.timeScale != 1f) { return; }
        bottonPause.SetActive(false);
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
        bottonPause.SetActive(true);
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
        ControlLlaves.instance.llavesActuales = 0;
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
