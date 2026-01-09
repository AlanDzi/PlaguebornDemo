using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Ustawienia interakcji")]
    [Tooltip("Maksymalna odleg³oœæ, z jakiej mo¿na wchodziæ w interakcjê")]
    public float interactDistance = 3f;

    [Tooltip("Warstwy, które s¹ interaktywne (ustaw np. tylko 'Interactable')")]
    public LayerMask interactableLayer;

    private Camera playerCamera;
    private IInteractable currentTarget;

    void Start()
    {
        playerCamera = Camera.main;

        // INFO: jeœli nic nie ustawisz w Inspectorze, warstwa zostanie automatycznie wykryta
        if (interactableLayer.value == 0)
        {
            int layer = LayerMask.NameToLayer("Interactable");
            if (layer >= 0)
            {
                interactableLayer = 1 << layer;
                Debug.Log("Ustawiono warstwê 'Interactable' automatycznie.");
            }
            else
            {
                Debug.LogWarning(" Brak warstwy 'Interactable'! Utwórz j¹ w edytorze Unity.");
            }
        }
    }

    void Update()
    {
        CheckForInteractable();

        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Tu jest kluczowe: Raycast uwzglêdnia tylko warstwy z maski
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentTarget != interactable)
                {
                    currentTarget = interactable;
                    UIManager.Instance.ShowInteractionPrompt(true, interactable.GetPromptText());
                }
                return;
            }
        }

        // Jeœli nie ma ju¿ ¿adnego obiektu przed nami
        if (currentTarget != null)
        {
            currentTarget = null;
            UIManager.Instance.ShowInteractionPrompt(false);
        }
    }
}