using UnityEngine;

[CreateAssetMenu(fileName = "ButtonDisableObject", menuName = "Button functionality/ButtonDisableObject")]
public class ButtonDisableObject : ScriptableObject, IButtonFunctionality
{
    public void OnActivate(GameObject interactableObject)
    {
        interactableObject.SetActive(false);
    }
}
