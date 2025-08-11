using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;

    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCountInBuildSettings == currentSceneIndex + 1)
        {
            ReturnToMainMenu();
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
