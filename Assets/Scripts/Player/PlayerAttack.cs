using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player state")]
    [SerializeField] private PlayerState playerState;

    [Header("Config")]
    [SerializeField] private AttackConfig attackConfig;

    [Header("Rules")]
    [SerializeField] private ScriptableRule playerCanAttackRule;

    [Header("Walkable layer")]
    [SerializeField] private LayerMask attackableLayer;

    [Header("Attack position")]
    [SerializeField] private Transform attackPoint;


    private RuleListener attackRuleListener;
    private bool canAttack;
    private void EnableAttack() => canAttack = true;
    private void DisableAttack() => canAttack = false;

    private RaycastHit2D[] hits;
    private Timer attackTimer;

    private void Awake()
    {
        attackRuleListener = new RuleListener(playerCanAttackRule, EnableAttack, DisableAttack);
        attackRuleListener.AddSubscription();
        attackTimer = new Timer(attackConfig.attackCooldown);
        PlayerInputHandler.instance.OnAttackPressed += TryAttack;
    }

    private void OnDisable()
    {
        attackRuleListener.RemoveSubscription();
        PlayerInputHandler.instance.OnAttackPressed -= TryAttack;
    }

    private void Update()
    {
        if (!canAttack) return;

        attackTimer.SubstractTime(Time.deltaTime);
    }

    private void TryAttack()
    {
        if (!canAttack || !playerState.IsGrounded || !attackTimer.TimerCompleted()) return;

        playerState.SetAttacking(true);
        hits = Physics2D.CircleCastAll(attackPoint.position, attackConfig.swingRange, transform.right, 0, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageble script = hits[i].collider.gameObject.GetComponent<IDamageble>();
            script?.TakeDamage(0);

        }
        attackTimer.ResetTimer();
        playerState.SetAttacking(false);
    }
}
