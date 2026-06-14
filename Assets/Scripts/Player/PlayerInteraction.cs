using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interacción")]
    public float interactDistance = 3f;

    [Header("UI")]
    public TextMeshProUGUI interactionText;

    Interactable currentInteractable;

    void Update()
    {
        CheckLookingAtObject();
        HandleInteractionInput();
    }

    void CheckLookingAtObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // ignorar jugador
            if (hit.collider.CompareTag("Player"))
            {
                HideUI();
                currentInteractable = null;
                return;
            }

            // ✔️ FIX IMPORTANTE AQUÍ
            Interactable interactable =
                hit.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;

                interactionText.gameObject.SetActive(true);
                interactionText.text =
                    interactable.promptMessage;

                return;
            }
        }

        HideUI();
        currentInteractable = null;
    }

    void HandleInteractionInput()
    {
        if (currentInteractable == null)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    void HideUI()
    {
        interactionText.gameObject.SetActive(false);
    }
}