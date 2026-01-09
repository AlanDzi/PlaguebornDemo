using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [Header("Treœæ notatki")]
    [TextArea(3, 10)]
    public string noteText;

    [Header("Opcjonalne ustawienia")]
    public string promptText = "Naciœnij E, aby przeczytaæ notatkê";

    private bool isRead = false;

    /// <summary>
    /// Zwraca tekst wyœwietlany przy komunikacie interakcji (prompt)
    /// </summary>
    public string GetPromptText()
    {
        return promptText;
    }

    /// <summary>
    /// Wywo³ywane, gdy gracz wciœnie E patrz¹c na notatkê
    /// </summary>
    public void Interact()
    {
        if (!isRead)
        {
            isRead = true;
        }

        UIManager.Instance.ShowNote(noteText);
    }
}