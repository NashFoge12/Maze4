using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static PlayerEmotions emotions;

    void Awake()
    {
        emotions = FindFirstObjectByType<PlayerEmotions>();
    }

    public static void AddFear(float value)
    {
        if (emotions == null) return;
        emotions.fear += value;
    }

    public static void AddStress(float value)
    {
        if (emotions == null) return;
        emotions.stress += value;
    }

    public static void AddRage(float value)
    {
        if (emotions == null) return;
        emotions.rage += value;
    }
}