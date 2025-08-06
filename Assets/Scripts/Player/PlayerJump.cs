using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] MovementConfig movementConfig;

    [Header("Rules")]
    [SerializeField] ScriptableRule jumpRule;
    [SerializeField] ScriptableRule invertedGravityRule;

    [Header("Player sprite")]
    [SerializeField] GameObject playerSpriteObject;

    private Rigidbody2D rb;
    private Collider2D colider;


    [SerializeField] LayerMask groundMask;
    private Vector2 boxCenter;
    private Vector2 boxSize;
    private bool checkForGroundEnabled = true;

    private RuleListener jumpRuleListener;
    private bool canJump;
    private void EnableJump() => canJump = true;
    private void DisableJump() => canJump = false;


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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();

        jumpRuleListener = new RuleListener(jumpRule, EnableJump, DisableJump);
        jumpRuleListener.AddSubscription();

        gravityInvertedRuleListener = new RuleListener(invertedGravityRule, InvertGravity, MakeGravityNormal);
        gravityInvertedRuleListener.AddSubscription();

        PlayerInputHandler.instance.OnJumpPressed += TryJump;
    }

    private void OnDestroy()
    {
        jumpRuleListener.RemoveSubscription();
        gravityInvertedRuleListener.RemoveSubscription();
        PlayerInputHandler.instance.OnJumpPressed -= TryJump;
    }

    private void TryJump()
    {
        if (!canJump || !IsGrounded()) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, gravityInverted ? -movementConfig.jumpForce : movementConfig.jumpForce);
        StartCoroutine(DisableGroundCheckAfterJump());
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
