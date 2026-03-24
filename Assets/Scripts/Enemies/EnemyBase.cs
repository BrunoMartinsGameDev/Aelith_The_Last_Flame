using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyDataSO data;

    protected int CurrentHealth;

    protected virtual void Start() => CurrentHealth = data.maxHealth;

    public virtual void TakeDamage(int amount, Vector2 knockback)
    {
        CurrentHealth -= amount;
        GetComponent<Rigidbody2D>()?.AddForce(knockback, ForceMode2D.Impulse);
        if (CurrentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        GetComponent<LootDropper>()?.Drop(data.lootTable);
        Destroy(gameObject);
    }
}