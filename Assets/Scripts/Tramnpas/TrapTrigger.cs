using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public WallMoveIn[] walls;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var w in walls)
                w.CloseWall();
        }
    }
}