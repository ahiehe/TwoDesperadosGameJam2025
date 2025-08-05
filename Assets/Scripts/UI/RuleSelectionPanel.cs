using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RuleSelectionPanel : MonoBehaviour
{
    public static RuleSelectionPanel instance;

    [Header("UI References")]
    [SerializeField] private Transform activeRulesContainer;
    [SerializeField] private Transform availableRulesContainer;

    [Header("Prefabs")]
    [SerializeField] private GameObject ruleDropSlotPrefab;
    [SerializeField] private GameObject ruleCardPrefab; 

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        RefreshUI();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void RefreshUI()
    {

        foreach (Transform child in activeRulesContainer)
            Destroy(child.gameObject);
        foreach (Transform child in availableRulesContainer)
            Destroy(child.gameObject);

        List<ScriptableRule> activeRules = RuleManager.instance.GetActiveRules();

        foreach (ScriptableRule rule in activeRules)
        {
            GameObject slot = Instantiate(ruleDropSlotPrefab, activeRulesContainer);
            slot.GetComponent<RuleDropSlot>().Setup(rule);
        }

        for (int i = activeRules.Count; i < 3; i++)
        {
            GameObject slot = Instantiate(ruleDropSlotPrefab, activeRulesContainer);
            slot.GetComponent<RuleDropSlot>().Setup();
        }

        List<ScriptableRule> inactiveRules = RuleManager.instance.GetInactiveRules();

        foreach (ScriptableRule rule in inactiveRules)
        {
            GameObject card = Instantiate(ruleCardPrefab, availableRulesContainer);
            card.GetComponent<RuleCard>().Setup(rule);
        }
    }

}
