using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("EIDOLON");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}