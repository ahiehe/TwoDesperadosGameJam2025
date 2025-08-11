using UnityEngine;

public class ProgressManager
{
    public static void SaveLevel(int level)
    {
        int biggestLevel = LoadLastLevel();
        PlayerPrefs.SetInt("Level", Mathf.Max(level, biggestLevel));
        PlayerPrefs.Save();
    }

    public static int LoadLastLevel()
    {
        return PlayerPrefs.GetInt("Level", 1); 
    }
}
