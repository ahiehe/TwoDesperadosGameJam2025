using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.EndLevel();
        }
    }
}
