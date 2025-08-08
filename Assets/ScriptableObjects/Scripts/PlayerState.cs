using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/PlayerState")]
public class PlayerState : ScriptableObject
{
    public bool IsGrounded {  get; private set; }
    public event Action<bool> IsGroundedChanged;
    public bool InteractionEnabled { get; private set; }
    public event Action InteractionDisabled;

    public void SetGrounded(bool value)
    {
        if (IsGrounded != value) IsGroundedChanged?.Invoke(value);
        IsGrounded = value;
    }
    public void SetInteraction(bool newInteraction)
    {
        InteractionEnabled = newInteraction;
        if (!InteractionEnabled) InteractionDisabled?.Invoke();
    }
}
