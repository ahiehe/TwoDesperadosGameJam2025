using UnityEngine;

public class RulePickup : MonoBehaviour
{
    [SerializeField] private ScriptableRule rule;
    [SerializeField] private SpriteRenderer ruleShownSprite;

    private void Awake()
    {
        ruleShownSprite.sprite = rule.ruleSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RuleManager.instance.AddRule(rule);
            Destroy(gameObject);
        }
    }
}
