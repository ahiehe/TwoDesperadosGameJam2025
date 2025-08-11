using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uimanager;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PlayerSpawner playerSpawner;


    public void EndLevel()
    {
        StartCoroutine(LevelEnding());
    }

    private IEnumerator LevelEnding()
    {
        playerState.SetInteraction(false);
        RuleManager.instance.DeactivateAllRules();
        ProgressManager.SaveLevel(SceneManager.GetActiveScene().buildIndex+1);
        playerSpawner.PlayerObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        uimanager.OpenWinMenu();
    }
}
