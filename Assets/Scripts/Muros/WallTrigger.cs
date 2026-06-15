using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public MovingWall wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wall.ActivateWall();
        }
    }
}