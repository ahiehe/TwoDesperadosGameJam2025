using System;
using UnityEngine;

public class RuleListener
{
    private ScriptableRule ruleToListen;
    private Action onRuleActive;
    private Action onRuleDeactive;

    public RuleListener(ScriptableRule rule, Action onRuleActivated, Action onRuleDeactivated)
    {
        ruleToListen = rule;

        onRuleActive = onRuleActivated;
        onRuleDeactive = onRuleDeactivated;

        if (rule.IsActive) 
        {
            onRuleActivated();
        }

    }

    public void AddSubscription()
    {
        ruleToListen.OnActivated += onRuleActive;
        ruleToListen.OnDeactivated += onRuleDeactive;
    }

    public void RemoveSubscription()
    {
        ruleToListen.OnActivated -= onRuleActive;
        ruleToListen.OnDeactivated -= onRuleDeactive;
    }
}
