
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
    private List<ScriptableRule> activeRules = new();


    private void Awake()
    {
        instance = this;

        DeactivateAllRules();
        for (int i = 0; i < startingRules.Count; i++)
        {
            AddRule(startingRules[i]);
            ActivateRule(startingRules[i].ruleName);
        }
    }

    public void DeactivateAllRules()
    {
        foreach (var rule in allRulesOnThisLevel)
        {
            rule.Deactivate();
        }
    }

    public void AddRule(ScriptableRule newRule)
    {
        if (existingRules.ContainsKey(newRule.ruleName)) return;
        existingRules.Add(newRule.ruleName, newRule);
    }

    public void ActivateRule(string ruleName)
    {
        if (activeCount >= maxRules || HasRuleActivated(ruleName) || !existingRules.ContainsKey(ruleName)) return;

        ScriptableRule rule = existingRules[ruleName];
        rule.Activate();
        activeRules.Add(rule);
        activeCount++;
    }

    public void DeactivateRule(string ruleName)
    {
        if (!HasRuleActivated(ruleName)) return;

        ScriptableRule rule = existingRules[ruleName];
        rule.Deactivate();
        activeRules.Remove(rule);
        activeCount--;
    }

    public List<ScriptableRule> GetActiveRules()
    {
        return new List<ScriptableRule>(activeRules);
    }


    public bool HasRuleActivated(string ruleName)
    {
        return existingRules.TryGetValue(ruleName, out ScriptableRule rule) && rule.IsActive;
    }


    public List<ScriptableRule> GetInactiveRules()
    {
        return existingRules.Values.Where(x => !x.IsActive).ToList();
    }

}
