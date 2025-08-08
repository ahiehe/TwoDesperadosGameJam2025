using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [Header("Config")]
    [SerializeField] private MovementConfig movementConfig;

    [Header("Rules")]
    [SerializeField] private ScriptableRule movementRule;
    [SerializeField] private ScriptableRule fastSpeedRule;

    private Rigidbody2D rb;

    private Vector2 moveInput;


    #region MovementRule
    private RuleListener movementRuleListener;
    private bool canMove;
    private void OnEnableMove() => canMove = true;
    private void OnDisableMove() => canMove = false;
    #endregion

    #region FastSpeedRule
    private RuleListener fastSpeedRuleListener;
    private bool isFastSpeedEnabled;
    private void OnEnableFastSpeed() => isFastSpeedEnabled = true;
    private void OnDisableFastSpeed() => isFastSpeedEnabled = false;
    #endregion

    private bool movementEnabled = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        movementRuleListener = new RuleListener(movementRule, OnEnableMove, OnDisableMove);
        movementRuleListener.AddSubscription();
        fastSpeedRuleListener = new RuleListener(fastSpeedRule, OnEnableFastSpeed, OnDisableFastSpeed);
        fastSpeedRuleListener.AddSubscription();

        PlayerInputHandler.instance.OnMovePerformed += GetMoveInput;
        playerState.InteractionDisabled += NullifyMovement;
    }

    private void OnDestroy()
    {
        movementRuleListener.RemoveSubscription();
        fastSpeedRuleListener.RemoveSubscription();

        PlayerInputHandler.instance.OnMovePerformed -= GetMoveInput;
        playerState.InteractionDisabled -= NullifyMovement;
    }



    private void FixedUpdate()
    {
        if (!canMove || !movementEnabled) return;

        rb.linearVelocity = new Vector2 (moveInput.x * movementConfig.movementSpeed * (isFastSpeedEnabled ? 2 : 1), rb.linearVelocity.y);
    }

    private void GetMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    private void NullifyMovement()
    {
        movementEnabled = false;
        rb.linearVelocity = Vector2.zero;
    }


}
