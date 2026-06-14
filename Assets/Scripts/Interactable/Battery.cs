using UnityEngine;

public class Battery : Interactable
{
    public override void Interact()
    {
        PlayerInventory inventory =
            FindObjectOfType<PlayerInventory>();

        if (inventory.hasBattery)
            return;

        inventory.hasBattery = true;
        inventory.carriedBattery = gameObject;

        Transform holdPoint =
            Camera.main.transform.Find("HoldPoint");

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;

        FindObjectOfType<HUDManager>()
            .ShowMessage("Batería recogida");

        Debug.Log("Batería recogida");
    }
}