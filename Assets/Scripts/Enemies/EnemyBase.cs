using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LootDropper))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyDataSO data;

    protected int CurrentHealth;
    
    public bool IsDead { get; private set; }

    protected virtual void Start() => CurrentHealth = data.maxHealth;

    public virtual void TakeDamage(int amount, Vector2 knockback)
    {
        if (IsDead) return;
        
        CurrentHealth -= amount;
        GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        if (CurrentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        IsDead = true;
        GetComponent<LootDropper>().Drop(data.lootTable);
        Destroy(gameObject);
    }
}