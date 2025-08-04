using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] MovementConfig movementConfig;

    private Rigidbody2D rb;
    
    private PlayerInputHandler playerInputHandler;

    private Vector2 moveInput;
    private bool canMove = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputHandler = GetComponent<PlayerInputHandler>();

        playerInputHandler.OnMovePerformed += GetMoveInput;
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
