using UnityEngine;
using TMPro;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
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
}