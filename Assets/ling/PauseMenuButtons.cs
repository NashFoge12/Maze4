using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    [Header("Configuración")]
    public string mainMenuSceneName = "Menu"; // Ponlo aquí en vez del botón

    private PauseMenu pauseMenu;

    void Start()
    {
        foreach (PauseMenu pm in Resources.FindObjectsOfTypeAll<PauseMenu>())
        {
            if (pm.gameObject.scene.isLoaded)
            {
                pauseMenu = pm;
                break;
            }
        }
    }

    public void Resume()
    {
        if (pauseMenu != null) pauseMenu.Resume();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Guarda el índice antes de descargar nada
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void GoToMainMenu()
    {
        Debug.Log("Intentando ir al menú: " + mainMenuSceneName); // Verifica en consola
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Solo en el editor
#else
    Application.Quit(); // En el juego exportado
#endif
    }
}