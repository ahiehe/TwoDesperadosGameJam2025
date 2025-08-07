using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/PlayerState")]
public class PlayerState : ScriptableObject
{
    public bool IsGrounded {  get; private set; }

    public void SetGrounded(bool value)
    {
        IsGrounded = value;
    }
}
