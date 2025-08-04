using Unity.Hierarchy;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "PlayerConfigs/MovementConfig")]
public class MovementConfig : ScriptableObject
{
    public float movementSpeed;

    public float jumpForce;
    public float groundCheckHeight;
}
