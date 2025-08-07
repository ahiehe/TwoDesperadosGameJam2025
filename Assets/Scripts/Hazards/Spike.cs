using System.Data;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private ScriptableRule spikeCantDamageRule;


    private RuleListener spikeCantDamageRuleListener;
    private void OnEnableRule() => gameObject.layer = 6;
    private void OnDisableRule() => gameObject.layer = 8;

    private void Awake()
    {
        spikeCantDamageRuleListener = new RuleListener(spikeCantDamageRule, OnEnableRule, OnDisableRule);
        spikeCantDamageRuleListener.AddSubscription();
    }

    private void OnDisable()
    {
        spikeCantDamageRuleListener.RemoveSubscription();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}
