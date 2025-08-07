using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] MovementConfig movementConfig;

    [Header("Rules")]
    [SerializeField] ScriptableRule jumpRule;
    [SerializeField] ScriptableRule invertedGravityRule;
    [SerializeField] ScriptableRule doubleJumpRule;

    [Header("Player sprite")]
    [SerializeField] GameObject playerSpriteObject;

    [Header("Player state")]
    [SerializeField] PlayerState playerState;

    private Rigidbody2D rb;
    private Collider2D colider;


    [SerializeField] LayerMask groundMask;
    private Vector2 boxCenter;
    private Vector2 boxSize;
    private bool checkForGroundEnabled = true;

    #region DoubleJumpRule
    private RuleListener doubleJumpRuleListener;
    private CounterWithDefault jumpCounter;
    private void SetDoubleJump()
    {
        jumpCounter.SetDefault(2);
    }
    private void SetOneJump()
    {
        jumpCounter.SetDefault(1);
    }
    #endregion

    #region JumpAvaibleRule
    private RuleListener jumpRuleListener;
    private bool canJump;
    private void EnableJump() => canJump = true;
    private void DisableJump() => canJump = false;
    #endregion

    #region GravityInvertedRule
    private RuleListener gravityInvertedRuleListener;
    private bool gravityInverted;
    private void InvertGravity() {
        gravityInverted = true;
        rb.gravityScale = -Mathf.Abs(rb.gravityScale);
        transform.localScale = new Vector3(1f, -1f, 1f);
    }
    private void MakeGravityNormal()
    {
        gravityInverted = false;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();

        jumpRuleListener = new RuleListener(jumpRule, EnableJump, DisableJump);
        jumpRuleListener.AddSubscription();

        gravityInvertedRuleListener = new RuleListener(invertedGravityRule, InvertGravity, MakeGravityNormal);
        gravityInvertedRuleListener.AddSubscription();

        jumpCounter = new CounterWithDefault(doubleJumpRule.IsActive ? 2 : 1);
        doubleJumpRuleListener = new RuleListener(doubleJumpRule, SetDoubleJump, SetOneJump);
        doubleJumpRuleListener.AddSubscription();

        PlayerInputHandler.instance.OnJumpPressed += TryJump;
    }

    private void Update()
    {
        playerState.SetGrounded(IsGrounded());
        if (playerState.IsGrounded) jumpCounter.SetToDefault();
    }

    private void OnDestroy()
    {
        jumpRuleListener.RemoveSubscription();
        gravityInvertedRuleListener.RemoveSubscription();
        doubleJumpRuleListener.RemoveSubscription();

        PlayerInputHandler.instance.OnJumpPressed -= TryJump;
    }

    private void TryJump()
    {
        if (canJump && (playerState.IsGrounded || jumpCounter.CurrentValueBigerThanZero() ))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, gravityInverted ? -movementConfig.jumpForce : movementConfig.jumpForce);
            jumpCounter.ChangeCounter(-1);
            StartCoroutine(DisableGroundCheckAfterJump());
        }
    }


    private bool IsGrounded()
    {
        if (!checkForGroundEnabled) return false;

        boxCenter = new Vector2(colider.bounds.center.x, colider.bounds.center.y) + 
            ( (gravityInverted ? Vector2.up : Vector2.down) * (colider.bounds.extents.y + movementConfig.groundCheckHeight / 2f));
        boxSize = new Vector2(colider.bounds.size.x * 0.8f, movementConfig.groundCheckHeight);

        var groundCheckbox = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundMask);
        if (groundCheckbox != null) return true;
        return false;
    }

    private IEnumerator DisableGroundCheckAfterJump()
    {
        checkForGroundEnabled = false;
        yield return new WaitForSeconds(0.1f);
        checkForGroundEnabled = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = !checkForGroundEnabled ? Color.red : Color.green;

        Gizmos.DrawWireCube(boxCenter, boxSize);

    }

#endif
}
