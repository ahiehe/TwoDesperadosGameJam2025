using UnityEngine;
using UnityEngine.EventSystems;

public class InactiveRuleZone : MonoBehaviour, IDropHandler
{
    //drop rulecard from selected zone
    public void OnDrop(PointerEventData eventData)
    {
        ScriptableRule scriptableRule = eventData.pointerDrag.GetComponent<RuleCard>().ruleInfo;
        RuleManager.instance.DeactivateRule(scriptableRule.ruleName);
        Destroy(eventData.pointerDrag.gameObject);
        RuleSelectionPanel.instance.RefreshUI();
    }
}
