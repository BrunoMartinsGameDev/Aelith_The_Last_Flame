using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/Player Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed      = 6f;
    public float jumpForce      = 14f;
    public float fallMultiplier = 2.5f;

    [Header("Dash")]
    public float dashSpeed    = 20f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.8f;

    [Header("Combat")]
    public int   maxHealth      = 5;
    public float invincibleTime = 1f;
}