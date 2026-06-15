using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Configuración")]
    public string pauseSceneName = "Pause";
    public KeyCode pauseKey = KeyCode.Space;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        // Verifica que la escena no esté ya cargada
        if (SceneManager.GetSceneByName(pauseSceneName).isLoaded) return;

        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(pauseSceneName, LoadSceneMode.Additive);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.UnloadSceneAsync(pauseSceneName);
    }
}