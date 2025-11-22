using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuInicial : MonoBehaviour
{
   private AudioSource music;
   public AudioClip clickAudio;
   public AudioClip switchAudio;

   public void Start( )
    {
      music = GetComponent<AudioSource>();
    }
     public void CambiarEscena(string nombre){

    SceneManager.LoadScene(nombre);

   }

   public void CerrarJuego()
   {
      Application.Quit();


   }

   public void ClickAudioOn()
   {
      music.PlayOneShot(clickAudio);
   }
    
    public void SwitchAudioOn()
    {
      music.PlayOneShot(switchAudio);
    }
}
