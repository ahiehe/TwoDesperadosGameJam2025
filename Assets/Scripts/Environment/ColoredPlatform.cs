using UnityEngine;

public class ColoredPlatform : MonoBehaviour, IRuleDependendPlatform
{
    [SerializeField] private ScriptableRule rule;
    [SerializeField] private GameObject activeSprite;
    [SerializeField] private GameObject inactiveSprite;

    private Collider2D platformCollider;
    private RuleListener ruleListener;

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        ruleListener = new RuleListener(rule, WhenActive, WhenInactive);
        ruleListener.AddSubscription();
    }

    private void OnDisable()
    {
        ruleListener.RemoveSubscription();
    }

    public void WhenActive()
    {
        platformCollider.enabled = true;
        activeSprite.SetActive(true);
        inactiveSprite.SetActive(false);
    }

    public void WhenInactive()
    {
        platformCollider.enabled = false;
        activeSprite.SetActive(false);
        inactiveSprite.SetActive(true);
    }
}
