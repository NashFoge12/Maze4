using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("UI Text")]
    public string promptMessage = "Presiona [E] para interactuar";

    public virtual void Interact()
    {
        Debug.Log("Interacción base");
    }
}