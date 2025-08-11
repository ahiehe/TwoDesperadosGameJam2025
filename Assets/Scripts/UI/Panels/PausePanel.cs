using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private UIManager uimanager;
    private void OnEnable()
    {
        Time.timeScale = 0f;

    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        uimanager.OpenGameplayPanel();
        gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
