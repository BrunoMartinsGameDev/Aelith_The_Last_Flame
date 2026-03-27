using System.Collections;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AbilityHandler))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;

    private Rigidbody2D _rb;
    private AbilityHandler _ability;

    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsHurt { get; private set; }
    public bool IsInvencible { get; private set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        CurrentHealth = stats.maxHealth;
        _ability = GetComponent<AbilityHandler>();
    }

    public void TakeDamage(int amount, Vector2 knockbackDir)
    {
        if (IsDead || IsInvencible) return;
        if (_ability != null && _ability.IsShielding) return; // não pode ser atingido estando com o escudo levantado

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (CurrentHealth <= 0)
        {
            IsDead = true;
            PlayerController.Instance.enabled = false; // desativa controle do jogador ao morrer
            return;
        }

        StartCoroutine(HurtRoutine(knockbackDir));
    }

    private IEnumerator HurtRoutine(Vector2 knockbackDir)
    {
        IsHurt = true;
        IsInvencible = true;

        // aplica knockback
        _rb.linearVelocity = Vector2.zero; // reseta velocidade atual
        _rb.AddForce(knockbackDir.normalized * 8f, ForceMode2D.Impulse);

        //trava input brevemente
        PlayerController.Instance.SetInputEnabled(false);

        yield return new WaitForSeconds(0.2f); // duração do knockback

        PlayerController.Instance.SetInputEnabled(true);

        IsHurt = false;
        // invencibilidade dura um pouco mais que o knockback
        yield return new WaitForSeconds(stats.invincibleTime - 0.2f);
        IsInvencible = false;
    }
}