using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;

    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


}
