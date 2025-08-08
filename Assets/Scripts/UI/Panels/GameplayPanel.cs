using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPanel : MonoBehaviour
{
    [SerializeField] private Transform activeRuleIconsContainer;

    [SerializeField] private GameObject ruleIconPrefab;

    [Header("States")]
    [SerializeField] private PlayerState playerState;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }

    private void OnEnable()
    {
        Time.timeScale = 1f;

        if (RuleManager.instance == null)
        {
            StartCoroutine(WaitRuleManager());
        }
        else
        {
            RefreshUI();
        }

        playerState.OnGroundedChanged += ChangeAlpha;
    }

    private void OnDisable()
    {
        playerState.OnGroundedChanged -= ChangeAlpha;
    }

    private void ChangeAlpha(bool isGrounded)
    {
        canvasGroup.alpha = isGrounded ? 1f : 0.5f;
    }


    public IEnumerator WaitRuleManager()
    {
        yield return new WaitUntil(() => RuleManager.instance != null);
        RefreshUI();
    }

    public void RefreshUI()
    {

        foreach (Transform child in activeRuleIconsContainer)
            Destroy(child.gameObject);

        List<ScriptableRule> activeRules = RuleManager.instance.GetActiveRules();

        foreach (ScriptableRule rule in activeRules)
        {
            GameObject slot = Instantiate(ruleIconPrefab, activeRuleIconsContainer);
            slot.GetComponent<RuleIcon>().Setup(rule);
        }

        for (int i = activeRules.Count; i < 3; i++)
        {
            GameObject slot = Instantiate(ruleIconPrefab, activeRuleIconsContainer);
            slot.GetComponent<RuleIcon>().Setup();
        }

    }
}
