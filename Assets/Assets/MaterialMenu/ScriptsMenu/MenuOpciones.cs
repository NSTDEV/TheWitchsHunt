using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeParam = "Volumen"; // nombre del parámetro en tu mixer

    // Este método lo llamás desde el slider (OnValueChanged)
    public void CambiarVolumen(float volumen)
    {
        // Evitar valores negativos o cero para el logaritmo
        volumen = Mathf.Clamp(volumen, 0.0001f, 1f);

        // Conversión lineal → logarítmica (dB)
        float dB = Mathf.Log10(volumen) * 20f;

        // Aplicar al mixer
        audioMixer.SetFloat(volumeParam, dB);
    }
}
