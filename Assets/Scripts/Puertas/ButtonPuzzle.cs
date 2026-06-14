using UnityEngine;
using System.Collections;

public class ButtonPuzzle : Interactable
{
    public Door door;
    public Animator animator;

    private bool activated = false;

    public override void Interact()
    {
        if (activated)
            return;

        activated = true;

        StartCoroutine(OpenDoorSequence());
    }

    IEnumerator OpenDoorSequence()
    {
        // Reproduce la animación del botón
        animator.SetTrigger("Press");

        // Espera a que termine la animación
        yield return new WaitForSeconds(1f);

        // Abre la puerta
        door.OpenDoor();
    }
}