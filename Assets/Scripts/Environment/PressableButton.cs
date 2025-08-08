using UnityEngine;

public class PressableButton : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject interacableObject;
    [SerializeField] private ScriptableObject buttonFunctionalityAsset;

    private IButtonFunctionality buttonFunctionality;


    private Collider2D buttonCollider;

    private void Awake()
    {
        buttonCollider = GetComponent<Collider2D>();
        buttonFunctionality = buttonFunctionalityAsset as IButtonFunctionality;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Pressed");
            buttonFunctionality.OnActivate(interacableObject);
            buttonCollider.enabled = false;
        }
    }
}
