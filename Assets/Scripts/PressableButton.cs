using UnityEngine;

public class PressableButton : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Pressed");
            Destroy(wall);
        }
    }
}
