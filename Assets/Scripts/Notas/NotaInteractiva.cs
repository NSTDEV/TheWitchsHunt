using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaInteractiva : MonoBehaviour
{
    [TextArea(3, 6)]
    public string textoNota;

    private NotasManager notasManager;

    void Start()
    {
        notasManager = FindObjectOfType<NotasManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            notasManager.MostrarNota(textoNota, this);
        }
    }
}
