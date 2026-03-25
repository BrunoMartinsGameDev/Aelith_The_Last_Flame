using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        var health = col.GetComponent<PlayerHealth>();
        if (health == null) return;

        // knockback vem da direção do inimigo pro player
        Vector2 knockbackDir = (col.transform.position
            - transform.position).normalized;

        health.TakeDamage(damage, knockbackDir);
    }
}