using UnityEngine;

public class Door : MonoBehaviour
{
    public float openDistance = 3f;
    public float speed = 1f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;

        openPosition =
            closedPosition +
            transform.right * openDistance;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position =
                Vector3.Lerp(
                    transform.position,
                    openPosition,
                    speed * Time.deltaTime
                );
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }
}