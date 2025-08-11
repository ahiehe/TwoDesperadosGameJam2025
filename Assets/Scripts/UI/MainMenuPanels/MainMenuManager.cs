using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject lorePanel;

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
        int savedLevel = ProgressManager.GetLastLevel();
        if (savedLevel >= SceneManager.sceneCountInBuildSettings) savedLevel = SceneManager.sceneCountInBuildSettings-1;
        SceneManager.LoadScene(savedLevel);
    }

    public void OpenMainMenu()
    {
        OpenPanel(MainMenuUIState.MainMenu);
    }

    public void OpenLevelsPanel()
    {
        OpenPanel(MainMenuUIState.Levels);
    }

    public void OpenLorePanel()
    {
        OpenPanel(MainMenuUIState.Lore);
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
        lorePanel.SetActive(mainMenuState == MainMenuUIState.Lore);
    }

    private void GenerateLevelButtons()
    {

        int lastLevel = ProgressManager.GetLastLevel();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            int levelIndex = i; 
            Button btn = Instantiate(levelButtonPrefab, levelButtonsParent);
            btn.GetComponentInChildren<Text>().text = i.ToString();

            if (i <= lastLevel)
            {
                btn.onClick.AddListener(() => OpenLevelByIndex(levelIndex));
            }
            else
            {
                btn.gameObject.GetComponent<CanvasGroup>().alpha = 0.5f;
                btn.enabled = false;
            }
            
        }
    }

    public enum MainMenuUIState
    {
        MainMenu,
        Levels,
        Lore
    }
}
