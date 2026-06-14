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

    [Header("Options Elements")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown qualityDropdown;

    [Header("Credits")]
    public CreditsScroll creditsScroll;

    void Start()
    {
        // Volumen: carga el valor guardado, si no hay ninguno usa 1 (máximo)
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
            AudioListener.volume = volumeSlider.value;
        }

        // Pantalla completa: carga el valor guardado, si no hay ninguno usa el estado actual
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
            Screen.fullScreen = fullscreenToggle.isOn;
        }

        // Calidad: carga el valor guardado, si no hay ninguno usa la calidad actual de Unity
        if (qualityDropdown != null)
        {
            qualityDropdown.value = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }
    }

    void Update()
    {

    }

    // ── Opciones ──────────────────────────────────────────────
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void OnFullscreenChanged(bool isOn)
    {
        Screen.fullScreen = isOn;
        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
    }

    public void OnQualityChanged(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", index);
    }

    // ── Navegación ────────────────────────────────────────────
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
        if (creditsScroll != null) creditsScroll.ResetScroll(); // Reinicia desde abajo
    }

    public void BackToMenu()
    {
        if (OptionsUI != null) OptionsUI.SetActive(false);
        if (CreditsUI != null) CreditsUI.SetActive(false);
        if (MenuUI != null) MenuUI.SetActive(true);
    }

    // ── Carga de escena ───────────────────────────────────────
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
                fillImage.fillAmount = progress;

            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progress * 100f).ToString() + "%";

            if (operation.progress >= 0.9f && timer >= MinLoadTime)
                operation.allowSceneActivation = true;

            yield return null;
        }
    }
}