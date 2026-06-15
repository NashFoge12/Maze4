using UnityEngine;
using TMPro;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI counterText; // Texto permanente del contador
    Coroutine messageRoutine;

    public void ShowMessage(string message, float time = 2f)
    {
        if (messageRoutine != null)
            StopCoroutine(messageRoutine);
        messageRoutine = StartCoroutine(Show(message, time));
    }

    IEnumerator Show(string message, float time)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        yield return new WaitForSeconds(time);
        messageText.gameObject.SetActive(false);
    }

    public void UpdateCounter(string doorName, int remaining, int total)
    {
        if (counterText == null) return;
        if (remaining <= 0)
        {
            counterText.gameObject.SetActive(false);
            return;
        }
        counterText.gameObject.SetActive(true);
        counterText.text = doorName + "\n🔘 " + remaining + "/" + total + " botones restantes";
    }
}