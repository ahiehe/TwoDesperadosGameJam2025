using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] MovementConfig movementConfig;

    [Header("Rules")]
    [SerializeField] ScriptableRule movementRule;

    private Rigidbody2D rb;

    private Vector2 moveInput;


    private RuleListener movementRuleListener;
    private bool canMove;
    private void OnEnableMove() => canMove = true;
    private void OnDisableMove() => canMove = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        movementRuleListener = new RuleListener(movementRule, OnEnableMove, OnDisableMove);
        movementRuleListener.AddSubscription();

        PlayerInputHandler.instance.OnMovePerformed += GetMoveInput;
    }

    private void OnDestroy()
    {
        movementRuleListener.RemoveSubscription();
        PlayerInputHandler.instance.OnMovePerformed -= GetMoveInput;
    }



    private void FixedUpdate()
    {
        if (!canMove) return;

        rb.linearVelocity = new Vector2 (moveInput.x * movementConfig.movementSpeed, rb.linearVelocity.y);
    }

    private void GetMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }



}
