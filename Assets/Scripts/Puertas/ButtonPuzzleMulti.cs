using UnityEngine;
using System.Collections;

public class ButtonPuzzleMulti : Interactable
{
    public MultiButtonDoor puzzle;
    public Animator animator;

    private bool activated = false;

    public override void Interact()
    {
        if (activated)
            return;

        activated = true;

        StartCoroutine(ButtonSequence());
    }

    IEnumerator ButtonSequence()
    {
        animator.SetTrigger("Press");

        yield return new WaitForSeconds(1f);

        puzzle.RegisterButton();
    }
}