using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class InactiveRuleZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private RuleSelectionPanel ruleSelectionPanel;

    //drop rulecard from selected zone
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        RuleCard ruleCard = eventData.pointerDrag.GetComponent<RuleCard>();
        if (ruleCard == null) return;

        ScriptableRule scriptableRule = ruleCard.ruleInfo;
        if (scriptableRule == null) return;

        RuleManager.instance.DeactivateRule(scriptableRule.ruleName);
        Destroy(eventData.pointerDrag.gameObject);
        ruleSelectionPanel.RefreshUI();
    }
}
