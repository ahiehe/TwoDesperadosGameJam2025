using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI panels")]
    [SerializeField] private GameObject GameplayPanel;
    [SerializeField] private GameObject RuleSelectionPanel;

    [Header("States")]
    [SerializeField] private PlayerState playerState;

    private UIState curState;
    private void Start()
    {
        PlayerInputHandler.instance.OnRuleMenuToggled += OpenRuleSelectionPanel;
    }

    private void OnDestroy()
    {
        PlayerInputHandler.instance.OnRuleMenuToggled -= OpenRuleSelectionPanel;
    }


    private void OpenRuleSelectionPanel()
    {
        if (!playerState.IsGrounded && curState != UIState.RuleSelection) return;
        SetActiveUI(UIState.RuleSelection);
    }

    private void SetActiveUI(UIState state)
    {
        if (curState == state)
        {
            curState = UIState.Gameplay;
        }
        else
        {
            curState = state;
        }
        GameplayPanel.SetActive(curState == UIState.Gameplay);
        RuleSelectionPanel.SetActive(curState == UIState.RuleSelection);
    }
}


public enum UIState
{
    Gameplay,
    Pause,
    Win,
    RuleSelection
}
