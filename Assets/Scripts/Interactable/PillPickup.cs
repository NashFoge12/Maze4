using UnityEngine;

public class PillPickup : Interactable
{
    private void Awake()
    {
        promptMessage = "Recoger pastilla";
    }

    public override void Interact()
    {
        PlayerEmotions emotions =
            FindFirstObjectByType<PlayerEmotions>();

        if (emotions != null)
        {
            emotions.UsePill();
        }

        FindFirstObjectByType<HUDManager>()
            .ShowMessage("Has recogido una pastilla");

        Destroy(gameObject);
    }
}