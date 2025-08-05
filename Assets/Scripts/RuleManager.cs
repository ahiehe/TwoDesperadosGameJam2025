
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public static RuleManager instance { get; private set; }

    [SerializeField] private List<ScriptableRule> allRules;
    [SerializeField] private List<ScriptableRule> startingRules;
    private readonly int maxRules = 3;

    private Dictionary<string, ScriptableRule> appliedRules = new ();

    private void Awake()
    {
        instance = this;

        DeactivateAllRules();
        foreach(var rule in startingRules)
        {
            ApplyRule(rule);
        }
    }

    private void DeactivateAllRules()
    {
        foreach (var rule in allRules)
        {
            rule.Deactivate();
        }
    }

    public void ApplyRule(ScriptableRule newRule)
    {
        if (appliedRules.Count >= maxRules || HasRuleActivated(newRule.ruleName)) return;
        appliedRules.Add(newRule.ruleName, newRule);
        newRule.Activate();
    }

    public void DeactivateRule(string ruleName)
    {
        if (!HasRuleActivated(ruleName)) return;

        appliedRules[ruleName].Deactivate();
        appliedRules.Remove(ruleName);

    }

    public bool HasRuleActivated(string ruleName)
    {
        return appliedRules.TryGetValue(ruleName, out ScriptableRule rule) && rule.IsActive;
    }

}
