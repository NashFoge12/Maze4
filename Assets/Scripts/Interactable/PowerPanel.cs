using UnityEngine;

public class PowerPanel : Interactable
{
    public Door door;
    public Transform batterySlot;

    public override void Interact()
    {
        PlayerInventory inventory =
            FindObjectOfType<PlayerInventory>();

        if (!inventory.hasBattery)
        {
            FindObjectOfType<HUDManager>()
                .ShowMessage("Necesitas una batería");

            Debug.Log("Necesitas una batería");
            return;
        }

        GameObject battery =
            inventory.carriedBattery;

        if (battery != null)
        {
            battery.transform.SetParent(batterySlot);
            battery.transform.localPosition = Vector3.zero;
            battery.transform.localRotation = Quaternion.identity;
        }

        inventory.hasBattery = false;
        inventory.carriedBattery = null;

        FindObjectOfType<HUDManager>()
            .ShowMessage("Batería instalada");

        Debug.Log("Batería instalada");

        door.OpenDoor();
    }
}