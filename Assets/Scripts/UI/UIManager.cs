using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject GameplayPanel;
    [SerializeField] private GameObject RuleSelectionPanel;

    private UIState curState;
    private void Start()
    {
        PlayerInputHandler.instance.OnRuleMenuToggled += () => SetActiveUI(UIState.RuleSelection);
    }

    private void OnDestroy()
    {
        PlayerInputHandler.instance.OnRuleMenuToggled -= () => SetActiveUI(UIState.RuleSelection);
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
