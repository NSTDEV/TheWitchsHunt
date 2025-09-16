using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;

    public void CambiarVolumen(float volumen){
        AudioMixer.SetFloat("Volumen", volumen);
    }
}
