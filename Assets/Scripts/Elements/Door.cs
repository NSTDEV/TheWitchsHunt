using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Si quieres cargar otra escena al escapar

public class Door : MonoBehaviour
{
    [SerializeField] private AudioClip playerVoice;
    [SerializeField] private float typingTime;
    [SerializeField] private int charsToPlaySound;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    //public ControlLlaves jujenioColeccionLlaves;
    private AudioSource audioSource;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playerVoice;
    }

    // Update is called once per frame
    void Update()
    {
        if (didDialogueStart && Input.GetButtonDown("Fire1"))
        {
            if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        //dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            //dialogueMark.SetActive(true);
            Time.timeScale = 1.0f;
            // Aquí puedes hacer una animación, desactivar la puerta o cargar otra escena
            // Por ejemplo:
            // SceneManager.LoadScene("NivelGanado");
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            if (charIndex % charsToPlaySound == 0)
            {
                audioSource.Play();
            }
            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            if (other.GetComponent<ControlLlaves>().llavesActuales >= 3)
            {
                StartDialogue();
                Debug.Log("¡Puerta abierta! Has escapado.");
            }
            else
            {
                Debug.Log("Necesitas 3 llaves para abrir esta puerta.");
            }
        }
    }
}