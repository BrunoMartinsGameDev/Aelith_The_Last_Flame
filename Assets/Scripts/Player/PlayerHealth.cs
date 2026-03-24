using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;

    public int  CurrentHealth { get; private set; }
    public bool IsDead        { get; private set; }
    public bool IsHurt        { get; private set; }

    void Start() => CurrentHealth = stats.maxHealth;

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (CurrentHealth <= 0)
        {
            IsDead = true;
            return;
        }

        StartCoroutine(HurtRoutine());
    }

    private IEnumerator HurtRoutine()
    {
        IsHurt = true;
        yield return new WaitForSeconds(0.3f);
        IsHurt = false;
    }
}