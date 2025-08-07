using UnityEngine;

[CreateAssetMenu(fileName = "AttackConfig", menuName = "PlayerConfigs/AttackConfig")]
public class AttackConfig : ScriptableObject
{
    public float attackCooldown;
    public float swingRange;
}
