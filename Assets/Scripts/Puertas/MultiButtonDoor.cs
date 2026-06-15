using UnityEngine;
using TMPro;

public class MultiButtonDoor : MonoBehaviour
{
    public int buttonsRequired = 3;
    private int buttonsPressed = 0;
    private Door door;

    [Header("Contador")]
    public string doorName = "Puerta A"; // Nombre para identificar la puerta

    void Start()
    {
        door = GetComponent<Door>();
        // Muestra el contador al iniciar
        FindFirstObjectByType<HUDManager>()
            ?.UpdateCounter(doorName, buttonsRequired, buttonsRequired);
    }

    public void RegisterButton()
    {
        buttonsPressed++;
        int remaining = buttonsRequired - buttonsPressed;

        FindFirstObjectByType<HUDManager>()
            ?.UpdateCounter(doorName, remaining, buttonsRequired);

        Debug.Log("Botones activados: " + buttonsPressed + "/" + buttonsRequired);

        if (buttonsPressed >= buttonsRequired)
        {
            door.OpenDoor();
        }
    }
}