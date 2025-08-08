using UnityEngine;

[CreateAssetMenu(fileName = "ButtonEnableObject", menuName = "Button functionality/ButtonEnableObject")]
public class ButtonEnableObject : ScriptableObject, IButtonFunctionality
{
    public void OnActivate(GameObject interactableObject)
    {
        interactableObject.SetActive(true);
    }
}
