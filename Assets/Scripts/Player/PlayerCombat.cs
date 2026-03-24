using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private AttackDataSO attackData;
    [SerializeField] private Transform    hitboxOrigin;
    [SerializeField] private LayerMask    enemyLayer;
    [SerializeField] private PlayerAnimator  animator;

    public bool IsAttacking { get; private set; }

    private int   _comboStep;
    private float _lastAttackTime;
    private float _lastInputTime;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            TryAttack();

        ResetComboIfExpired();
    }

    private void TryAttack()
    {
        if (IsAttacking) 
        {
            // registra input durante ataque pra encadear
            _lastInputTime = Time.time;
            return;
        }

        if (Time.time - _lastAttackTime < attackData.cooldown) return;

        StartCoroutine(AttackRoutine(_comboStep));
    }

    private IEnumerator AttackRoutine(int step)
    {
        IsAttacking     = true;
        _lastAttackTime = Time.time;
        _lastInputTime  = 0f;

        var combo = attackData.comboSteps[step];
        
        animator.Play(combo.animationName);

        // espera hitbox duration pra aplicar dano no timing certo
        yield return new WaitForSeconds(combo.hitboxDuration);

        ApplyHitbox(combo);

        // espera animação terminar antes de liberar próximo ataque
        yield return new WaitForSeconds(combo.hitboxDuration);

        IsAttacking = false;

        // avança o combo se houve input durante o ataque
        if (_lastInputTime > 0f)
        {
            _comboStep = (_comboStep + 1) % attackData.comboSteps.Length;
            StartCoroutine(AttackRoutine(_comboStep));
        }
        else
        {
            _comboStep = (_comboStep + 1) % attackData.comboSteps.Length;
        }
    }

    private void ApplyHitbox(AttackDataSO.ComboStep combo)
    {
        var hits = Physics2D.OverlapCircleAll(
            hitboxOrigin.position, combo.hitboxRadius, enemyLayer);

        foreach (var hit in hits)
        {
            var dir = (hit.transform.position - transform.position).normalized;
            hit.GetComponent<EnemyBase>()?.TakeDamage(
                combo.damage, dir * combo.knockbackForce);
        }
    }

    private void ResetComboIfExpired()
    {
        if (!IsAttacking &&
            Time.time - _lastAttackTime > attackData.comboResetTime)
            _comboStep = 0;
    }
}