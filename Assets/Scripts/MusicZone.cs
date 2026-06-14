using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioClip zoneMusic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<MusicManager>().PlayMusic(zoneMusic);
        }
    }
}