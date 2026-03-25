using System.Collections;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private AbilitySO     dashAbility;
    [SerializeField] private AbilitySO     shieldAbility;

    private Rigidbody2D _rb;
    private bool        _isDashing;
    private float       _lastDashTime;

    public bool IsDashing  { get; private set; }
    public bool IsShielding { get; private set; }

    public bool CanDash   => dashAbility   != null && dashAbility.isUnlocked;
    public bool CanShield => shieldAbility != null && shieldAbility.isUnlocked;

    void Start() => _rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash && !_isDashing)
            if (Time.time - _lastDashTime >= stats.dashCooldown)
                StartCoroutine(DashRoutine());
        HandleShield();
    }

    private void HandleShield()
    {
        if(!CanShield) return;
        if(_isDashing) return;

        IsShielding = Input.GetKey(KeyCode.Mouse1);
    }

    private IEnumerator DashRoutine()
    {
        IsDashing     = true;
        _isDashing    = true;
        _lastDashTime = Time.time;

        float direction = transform.localScale.x > 0 ? 1f : -1f;
        float elapsed   = 0f;

        PlayerController.Instance.SetInputEnabled(false);

        yield return new WaitForFixedUpdate(); // garante que o dash comece no próximo frame de física

        // desativa gravidade durante o dash
        float originalGravity  = _rb.gravityScale;
        _rb.gravityScale       = 0f;
        _rb.linearVelocity     = new Vector2(direction * stats.dashSpeed, 0f);

        while (elapsed < stats.dashDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        PlayerController.Instance.SetInputEnabled(true);

        _rb.gravityScale   = originalGravity;
        _rb.linearVelocity = new Vector2(0f, _rb.linearVelocity.y);
        IsDashing          = false;
        _isDashing         = false;
    }
}