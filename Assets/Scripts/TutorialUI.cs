using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public float duration = 5f;

    void Start()
    {
        tutorialPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Invoke(nameof(HideTutorial), duration);
    }

    void HideTutorial()
    {
        tutorialPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Llama esto desde el botón "Comenzar"
    public void SkipTutorial()
    {
        CancelInvoke(nameof(HideTutorial)); // Cancela el timer automático
        HideTutorial();
    }
}