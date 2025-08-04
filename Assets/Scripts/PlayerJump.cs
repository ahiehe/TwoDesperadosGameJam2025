using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] MovementConfig movementConfig;

    private Rigidbody2D rb;
    private Collider2D colider;
    private PlayerInputHandler playerInputHandler;

    private bool canJump = true;

    [SerializeField] LayerMask groundMask;
    private Vector2 boxCenter;
    private Vector2 boxSize;
    private bool checkForGroundEnabled = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colider = GetComponent<Collider2D>();
        playerInputHandler = GetComponent<PlayerInputHandler>();

        playerInputHandler.OnJumpPressed += TryJump;
    }

    private void TryJump()
    {
        if (!canJump || !IsGrounded()) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, movementConfig.jumpForce);
        StartCoroutine(DisableGroundCheckAfterJump());
    }


    private bool IsGrounded()
    {
        if (!checkForGroundEnabled) return false;

        boxCenter = new Vector2(colider.bounds.center.x, colider.bounds.center.y) + (Vector2.down * (colider.bounds.extents.y + movementConfig.groundCheckHeight / 2f));
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
