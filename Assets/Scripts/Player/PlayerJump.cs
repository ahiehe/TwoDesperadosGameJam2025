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

    [Header("Walkable layer")]
    [SerializeField] LayerMask groundLayer;

    private Vector2 boxCenter;
    private Vector2 boxSize;
    private bool checkForGroundEnabled = true;
    private Timer groundCheckTimer = new Timer(0.05f);
    private Timer coyoteTimer = new Timer(0.1f);

    private bool isJumped = false;

    #region DoubleJumpRule
    private RuleListener doubleJumpRuleListener;
    private CounterWithDefault jumpCounter;
    private void SetDoubleJump()
    {
        jumpCounter.SetDefault(2);
        jumpCounter.SetToDefault();
    }
    private void SetOneJump()
    {
        jumpCounter.SetDefault(1);
        jumpCounter.SetToDefault();
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
        transform.localScale = new Vector3(transform.localScale.x, -1f, transform.localScale.z);
    }
    private void MakeGravityNormal()
    {
        gravityInverted = false;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
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
        PlayerInputHandler.instance.OnJumpReleased += StopJump;

        playerState.OnGroundedChanged += OnGroundedChanged;
    }
    private void OnDisable()
    {
        jumpRuleListener.RemoveSubscription();
        gravityInvertedRuleListener.RemoveSubscription();
        doubleJumpRuleListener.RemoveSubscription();

        PlayerInputHandler.instance.OnJumpPressed -= TryJump;
        PlayerInputHandler.instance.OnJumpReleased -= StopJump;

        playerState.OnGroundedChanged -= OnGroundedChanged;
    }

    private void OnGroundedChanged(bool isGrounded)
    {
        ManageJumpCounter(isGrounded);
        coyoteTimer.ResetTimer();
    }

    private void ManageJumpCounter(bool isGrounded)
    {
        if (isGrounded)
        {
            jumpCounter.SetToDefault();
            isJumped = false;
        }

        if (!isGrounded && !isJumped)
        {
            jumpCounter.ChangeCounter(-1);
        }

    }

    private void Update()
    {
        groundCheckTimer.SubstractTime(Time.deltaTime);
        coyoteTimer.SubstractTime(Time.deltaTime);
        if (groundCheckTimer.TimerCompleted())
        {
            playerState.SetGrounded(IsGrounded());
            groundCheckTimer.ResetTimer();
        } 
    }

    private void TryJump()
    {
        if (canJump && (!coyoteTimer.TimerCompleted() || jumpCounter.CurrentValueBigerThanZero() ))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, gravityInverted ? -movementConfig.jumpForce : movementConfig.jumpForce);
            jumpCounter.ChangeCounter(-1);
            StartCoroutine(DisableGroundCheckAfterJump());
            isJumped = true;
        }
    }

    private void StopJump()
    {
        if (!canJump || rb.linearVelocity.y <= 0) return;
        
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        
    }


    private bool IsGrounded()
    {
        if (!checkForGroundEnabled) return false;


        boxCenter = new Vector2(colider.bounds.center.x, colider.bounds.center.y) + 
            ( (gravityInverted ? Vector2.up : Vector2.down) * (colider.bounds.extents.y + movementConfig.groundCheckHeight / 2f));
        boxSize = new Vector2(colider.bounds.size.x * 0.8f, movementConfig.groundCheckHeight);

        var groundCheckbox = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);
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
