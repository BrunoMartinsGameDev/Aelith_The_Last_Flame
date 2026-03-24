using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerJump   jump;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private AbilityHandler ability;

    private Animator     _animator;
    private Rigidbody2D  _rb;
    private string       _currentState;

    private const string IDLE = "Idle";
    private const string RUN  = "Run";
    private const string JUMP = "Jump";
    private const string FALL = "Fall";
    private const string DASH = "Dash";
    private const string HURT = "Hurt";
    private const string DIE  = "Die";

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb       = GetComponent<Rigidbody2D>();
    }

    void Update() => UpdateState();

    private void UpdateState()
    {
        if (health.IsDead)      { Play(DIE);  return; }
        if (health.IsHurt)      { Play(HURT); return; }
        if (ability.IsDashing) { Play(DASH); return; }

        // ataque — usa o nome direto do SO via combo step
        if (combat.IsAttacking) return; // animator já foi setado pelo Combat

        if (!jump.IsGrounded)
        {
            Play(_rb.linearVelocity.y > 0 ? JUMP : FALL);
            return;
        }

        Play(Mathf.Abs(_rb.linearVelocity.x) > 0.1f ? RUN : IDLE);
    }

    public void Play(string state)
    {
        if (state == _currentState) return;
        _currentState = state;
        _animator.Play(state);
    }
}