using UnityEngine;

public class MultiButtonDoor : MonoBehaviour
{
    public int buttonsRequired = 3;

    private int buttonsPressed = 0;
    private Door door;

    void Start()
    {
        door = GetComponent<Door>();
    }

    public void RegisterButton()
    {
        buttonsPressed++;

        Debug.Log("Botones activados: " +
                  buttonsPressed +
                  "/" +
                  buttonsRequired);

        if (buttonsPressed >= buttonsRequired)
        {
            door.OpenDoor();
        }
    }
}