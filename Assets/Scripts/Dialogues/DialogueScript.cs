using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [SerializeField] private AudioClip playerVoice;
    [SerializeField] private float typingTime;
    [SerializeField] private int charsToPlaySound;
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;

    private AudioSource audioSource;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = playerVoice;
    }

    void Update()
    {
        if (!didDialogueStart) return;

        bool input = Input.GetMouseButtonDown(0);

        // Celular
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
                input = true;
        }

        if (input)
        {
            if (dialogueText.text == dialogueLines[lineIndex])
                NextDialogueLine();
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
            Destroy(gameObject);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        int charIndex = 0;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            if(charIndex % charsToPlaySound == 0)
            {
                audioSource.Play();
            }
            charIndex++;
            yield return new WaitForSecondsRealtime(typingTime) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            StartDialogue();
            //isPlayerInRange = true;
            //dialogueMark.SetActive(true);
        }
    }
    /**
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }*/
}
