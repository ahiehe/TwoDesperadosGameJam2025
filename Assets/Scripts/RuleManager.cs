
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager instance { get; private set; }

    [SerializeField] private List<ScriptableRule> allRulesOnThisLevel;
    [SerializeField] private List<ScriptableRule> startingRules;

    private readonly int maxRules = 3;
    private int activeCount = 0;
    private Dictionary<string, ScriptableRule> existingRules = new ();


    private void Awake()
    {
        instance = this;

        DeactivateAllRules();
        foreach(var rule in startingRules)
        {
            AddRule(rule);
            ActivateRule(rule.ruleName);
        }
    }

    private void DeactivateAllRules()
    {
        foreach (var rule in allRulesOnThisLevel)
        {
            rule.Deactivate();
        }
    }

    public void AddRule(ScriptableRule newRule)
    {
        existingRules.Add(newRule.ruleName, newRule);
    }

    public void ActivateRule(string ruleName)
    {
        if (activeCount >= maxRules || HasRuleActivated(ruleName)) return;
        ScriptableRule rule = existingRules[ruleName];
        rule.Activate();
        activeCount++;

    }

    public void DeactivateRule(string ruleName)
    {
        if (!HasRuleActivated(ruleName)) return;

        ScriptableRule rule = existingRules[ruleName];
        rule.Deactivate();
        activeCount--;

    }

    public bool HasRuleActivated(string ruleName)
    {
        return existingRules.TryGetValue(ruleName, out ScriptableRule rule) && rule.IsActive;
    }

}
