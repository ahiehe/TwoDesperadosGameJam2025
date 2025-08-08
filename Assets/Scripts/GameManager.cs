using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uimanager;
    [SerializeField] private PlayerState playerState;

    public void EndLevel()
    {
        StartCoroutine(LevelEnding());
    }

    private IEnumerator LevelEnding()
    {
        playerState.SetInteraction(false);
        RuleManager.instance.DeactivateAllRules();
        yield return new WaitForSeconds(2.0f);
        uimanager.OpenWinMenu();
    }
}
