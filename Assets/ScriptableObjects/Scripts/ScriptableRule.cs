using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableRule", menuName = "Rule/ScriptableRule")]
public class ScriptableRule : ScriptableObject
{
    public string ruleName;
    public string ruleDescription;
    public Sprite ruleSprite;

    public bool IsActive { get; private set; }
    public event Action OnActivated;
    public event Action OnDeactivated;


    public void Activate()
    {
        //if (IsActive) return;
        IsActive = true;
        OnActivated?.Invoke();
    }

    public void Deactivate()
    {
        //if (!IsActive) return;
        IsActive = false;
        OnDeactivated?.Invoke();
    }
}
