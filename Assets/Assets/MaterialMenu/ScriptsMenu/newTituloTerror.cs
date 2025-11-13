using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class newTituloTerror : MonoBehaviour
{
    public TextMeshProUGUI textoTMP;
    public Text textoUI;

    [Header("Animación general")]
    public float intensidadTemblor = 0.02f;
    public float velocidadPulso = 2f;

    [Header("Efecto de color")]
    public Color colorBase = new Color(0.7f, 0.9f, 1f); // azul grisáceo
    public Color colorDestello = new Color(1f, 1f, 1f); // blanco fuerte
    public float velocidadColor = 2f;

    private Vector3 posicionOriginal;
    private float tiempo;

    void Start()
    {
        if (textoTMP == null) textoTMP = GetComponent<TextMeshProUGUI>();
        if (textoUI == null) textoUI = GetComponent<Text>();

        posicionOriginal = transform.localPosition;

        // Aplica el color inicial
        SetColor(colorBase);
    }

    void Update()
    {
        // 💓 Pulso leve (como si respirara)
        tiempo += Time.deltaTime * velocidadPulso;
        float escala = 1f + Mathf.Sin(tiempo) * 0.02f;
        transform.localScale = new Vector3(escala, escala, 1f);

        // 💀 Temblor sutil
        transform.localPosition = posicionOriginal + (Vector3)Random.insideUnitCircle * intensidadTemblor;

        // 🌈 Cambio de color pulsante
        float t = (Mathf.Sin(Time.time * velocidadColor) + 1f) / 2f; // valor oscilante entre 0 y 1
        Color colorActual = Color.Lerp(colorBase, colorDestello, t);
        SetColor(colorActual);

        // 👁️ Parpadeo ocasional
        if (Random.value < 0.003f)
        {
            StartCoroutine(Parpadeo());
        }
    }

    private IEnumerator Parpadeo()
    {
        SetAlpha(0.3f);
        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        SetAlpha(1f);
    }

    private void SetColor(Color c)
    {
        if (textoTMP != null)
            textoTMP.color = c;
        else if (textoUI != null)
            textoUI.color = c;
    }

    private void SetAlpha(float alpha)
    {
        if (textoTMP != null)
        {
            Color c = textoTMP.color;
            c.a = alpha;
            textoTMP.color = c;
        }
        else if (textoUI != null)
        {
            Color c = textoUI.color;
            c.a = alpha;
            textoUI.color = c;
        }
    }
}
