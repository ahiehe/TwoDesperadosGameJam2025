using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        playerState.OnGroundedChanged += OnGroundedChanged;
        playerState.OnIdleChanged += OnIdleChanged;
        playerState.OnAttackingChanged += OnAttackingChanged;
    }

    private void OnDisable()
    {
        playerState.OnGroundedChanged -= OnGroundedChanged;
        playerState.OnIdleChanged -= OnIdleChanged;
        playerState.OnAttackingChanged -= OnAttackingChanged;
    }

    private void OnGroundedChanged(bool isGrounded)
    {
        animator.SetBool("InAir", !isGrounded);
    }

    private void OnIdleChanged(bool isIdle)
    {
        animator.SetBool("Idle", isIdle);
    }

    private void OnAttackingChanged(bool isAttacking)
    {
        if (isAttacking) animator.SetTrigger("Attacking"); 
    }


}
