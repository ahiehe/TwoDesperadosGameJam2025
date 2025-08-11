using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class ProgressManager
{
    public static void SaveLevel(int level)
    {
        int biggestLevel = GetLastLevel();
        PlayerPrefs.SetInt("Level", Mathf.Max(level, biggestLevel));
        PlayerPrefs.Save();
    }

    public static int GetLastLevel()
    {
        return PlayerPrefs.GetInt("Level", 1); 
    }

    public static void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
