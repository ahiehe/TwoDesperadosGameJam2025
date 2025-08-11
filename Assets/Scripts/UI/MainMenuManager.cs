using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsPanel;

    [Header("Levels UI")]
    [SerializeField] private Transform levelButtonsParent; 
    [SerializeField] private Button levelButtonPrefab; 

    private MainMenuUIState mainMenuState;

    private void Awake()
    {
        GenerateLevelButtons();
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(ProgressManager.LoadLastLevel());
    }

    public void OpenMainMenu()
    {
        OpenPanel(MainMenuUIState.MainMenu);
    }

    public void OpenLevelsPanel()
    {
        OpenPanel(MainMenuUIState.Levels);
    }

    public void OpenLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void OpenPanel(MainMenuUIState newState)
    {
        mainMenuState = newState;
        mainMenu.SetActive(mainMenuState == MainMenuUIState.MainMenu);
        levelsPanel.SetActive(mainMenuState == MainMenuUIState.Levels);
    }

    private void GenerateLevelButtons()
    {


        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            int levelIndex = i; 
            Button btn = Instantiate(levelButtonPrefab, levelButtonsParent);
            btn.GetComponentInChildren<Text>().text = i.ToString();

            btn.onClick.AddListener(() => OpenLevelByIndex(levelIndex));
        }
    }

    public enum MainMenuUIState
    {
        MainMenu,
        Levels
    }
}
