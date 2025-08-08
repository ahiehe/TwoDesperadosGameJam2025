using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/PlayerState")]
public class PlayerState : ScriptableObject
{
    public bool IsGrounded {  get; private set; }
    public event Action<bool> OnGroundedChanged;

    public bool IsIdle { get; private set; }
    public event Action<bool> OnIdleChanged;

    public bool IsAttacking { get; private set; }
    public event Action<bool> OnAttackingChanged;

    public event Action InteractionDisabled;

    public void SetGrounded(bool value)
    {
        if (IsGrounded != value) OnGroundedChanged?.Invoke(value);
        IsGrounded = value;
    }

    public void SetIdle(bool value)
    {
        if (IsIdle != value) OnIdleChanged?.Invoke(value);
        IsIdle = value;
    }

    public void SetAttacking(bool value)
    {
        if (IsAttacking != value) OnAttackingChanged?.Invoke(value);
        IsAttacking = value;
    }

    public void SetInteraction(bool newInteraction)
    {
        if (!newInteraction) InteractionDisabled?.Invoke();
    }
}
