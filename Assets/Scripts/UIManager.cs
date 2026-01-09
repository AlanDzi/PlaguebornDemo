using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public GameObject notePanel;
    public TMP_Text noteText;
    public GameObject interactionPrompt;
    public TMP_Text interactionPromptText; 

    private bool isNoteOpen = false;

    private void Awake()
    {
        Instance = this;
        notePanel.SetActive(false);
        interactionPrompt.SetActive(false);
    }

    
    public void ShowInteractionPrompt(bool show)
    {
        if (!isNoteOpen)
            interactionPrompt.SetActive(show);
    }

    
    public void ShowInteractionPrompt(bool show, string text)
    {
        if (!isNoteOpen)
        {
            interactionPrompt.SetActive(show);
            if (interactionPromptText != null)
                interactionPromptText.text = text;
        }
    }

    public void ShowNote(string text)
    {
        isNoteOpen = true;
        notePanel.SetActive(true);
        interactionPrompt.SetActive(false);
        noteText.text = text;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // pauza gry
    }

    public void CloseNote()
    {
        isNoteOpen = false;

        
        notePanel.SetActive(false);

        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (isNoteOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseNote();
        }
    }
}
