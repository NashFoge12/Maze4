using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player =
            other.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}