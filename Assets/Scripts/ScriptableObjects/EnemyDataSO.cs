using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Stats")]
    public int   maxHealth      = 3;
    public float moveSpeed      = 2.5f;
    public int   damage         = 1;
    public float knockbackForce = 5f;

    [Header("AI")]
    public float detectionRange = 5f;
    public float attackRange    = 1f;
    public float patrolDistance = 4f;

    [Header("Loot")]
    public LootTableSO lootTable;
}