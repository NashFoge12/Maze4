using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Scene Configuration")]
    public string LevelName;

    [Header("UI Elements")]
    public TextMeshProUGUI progressText;
    public Image fillImage;
    public float MinLoadTime = 2f;
    public GameObject LoadingUI;
    public GameObject MenuUI;

    [Header("Other Panels")]
    public GameObject OptionsUI;
    public GameObject CreditsUI;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Play()
    {
        if (MenuUI != null) MenuUI.SetActive(false);
        if (LoadingUI != null) LoadingUI.SetActive(true);
        StartCoroutine(startLoad());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        if (MenuUI != null) MenuUI.SetActive(false);
        if (OptionsUI != null) OptionsUI.SetActive(true);
    }

    public void OpenCredits()
    {
        if (MenuUI != null) MenuUI.SetActive(false);
        if (CreditsUI != null) CreditsUI.SetActive(true);
    }

    public void BackToMenu()
    {
        if (OptionsUI != null) OptionsUI.SetActive(false);
        if (CreditsUI != null) CreditsUI.SetActive(false);
        if (MenuUI != null) MenuUI.SetActive(true);
    }

    IEnumerator startLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelName);
        operation.allowSceneActivation = false;
        float timer = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (fillImage != null)
            {
                fillImage.fillAmount = progress;
            }
            if (progressText != null)
            {
                progressText.text = Mathf.RoundToInt(progress * 100f).ToString() + "%";
            }

            if (operation.progress >= 0.9f && timer >= MinLoadTime)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}