using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RuleDropSlot : RuleCard, IDropHandler
{

    private RuleSelectionPanel ruleSelectionPanel;

    public void Setup(RuleSelectionPanel ruleSelectionPanel)
    {
        ruleDescription.gameObject.SetActive(false);
        ruleSprite.gameObject.SetActive(false);
        isDragable = false;
        this.ruleSelectionPanel = ruleSelectionPanel;
    }

    public void Setup(ScriptableRule ruleInfo, RuleSelectionPanel ruleSelectionPanel)
    {
        Setup(ruleInfo);
        this.ruleSelectionPanel = ruleSelectionPanel;
    }

    //drop rulecard from unselected zone
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        RuleCard ruleCard = eventData.pointerDrag.GetComponent<RuleCard>();
        if (ruleCard == null) return;

        ScriptableRule scriptableRule = ruleCard.ruleInfo;
        if (ruleInfo != null) RuleManager.instance.DeactivateRule(ruleInfo.ruleName);

        RuleManager.instance.ActivateRule(scriptableRule.ruleName);
        Destroy(eventData.pointerDrag.gameObject);
        ruleSelectionPanel.RefreshUI();
        
    }

}
