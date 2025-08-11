using UnityEngine;
using UnityEngine.UI;

public class RuleIcon : MonoBehaviour
{
    [SerializeField] protected Image ruleSprite;

    public ScriptableRule ruleInfo { get; private set; }

    public void Setup(ScriptableRule ruleInfo)
    {
        this.ruleInfo = ruleInfo;
        ruleSprite.sprite = ruleInfo.ruleSprite;

    }

    public void Setup()
    {
        ruleSprite.gameObject.SetActive(false);
    }
}
