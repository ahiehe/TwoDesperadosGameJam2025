using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI panels")]
    [SerializeField] private GameObject GameplayPanel;
    [SerializeField] private GameObject RuleSelectionPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject WinPanel;

    [Header("States")]
    [SerializeField] private PlayerState playerState;

    private UIState curState;

    private Dictionary<UIState, GameObject> panels;

    private void Awake()
    {
        panels = new()
        {
            { UIState.Gameplay, GameplayPanel },
            { UIState.RuleSelection, RuleSelectionPanel },
            { UIState.Pause, PausePanel },
            { UIState.Win, WinPanel },
        };
    }


    private void Start()
    {
        PlayerInputHandler.instance.OnRuleMenuToggled += OpenRuleSelectionPanel;
        PlayerInputHandler.instance.OnPauseToggled += OpenPauseMenu;
    }

    private void OnDestroy()
    {
        PlayerInputHandler.instance.OnRuleMenuToggled -= OpenRuleSelectionPanel;
        PlayerInputHandler.instance.OnPauseToggled -= OpenPauseMenu;
    }

    private void OpenPauseMenu()
    {
        SetActiveUI(UIState.Pause);
    }

    public void OpenWinMenu()
    {
        SetActiveUI(UIState.Win);
    }

    public void OpenRuleSelectionPanel()
    {
        if (!playerState.IsGrounded && curState != UIState.RuleSelection) return;
        SetActiveUI(UIState.RuleSelection);
    }

    private void SetActiveUI(UIState state)
    {
        curState = curState == state ? curState = UIState.Gameplay : state;

        foreach (var panel in panels) 
        { 
            panel.Value.SetActive(curState == panel.Key);
        }

    }
}


public enum UIState
{
    Gameplay,
    Pause,
    Win,
    RuleSelection
}
