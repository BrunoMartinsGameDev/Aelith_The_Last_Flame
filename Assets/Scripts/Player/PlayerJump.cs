using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO stats;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.12f;

    private Rigidbody2D _rb;
    private int _jumpsLeft;
    private float _coyoteTimer;

    public bool IsGrounded { get; private set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _jumpsLeft = 2;
    }

    void Update()
    {
        UpdateGrounded();

        if (Input.GetButtonDown("Jump") && _jumpsLeft > 0)
            Jump();

        ApplyFallMultiplier();
    }

    private void UpdateGrounded()
    {
        bool touchingGround = Physics2D.OverlapCircle(
            groundCheck.position, 0.1f, groundLayer)
            && _rb.linearVelocity.y <= 0.01f;

        if (touchingGround)
        {
            IsGrounded = true;
            _coyoteTimer = coyoteTime;
            _jumpsLeft = 2;
        }
        else
        {
            if (_coyoteTimer > 0f && _jumpsLeft == 2)
                _jumpsLeft = 1;
            _coyoteTimer -= Time.deltaTime;
            IsGrounded = _coyoteTimer > 0f;
        }
    }

    private void Jump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, stats.jumpForce);
        _coyoteTimer = 0f;
        _jumpsLeft--;
    }

    private void ApplyFallMultiplier()
    {
        if (_rb.linearVelocity.y < 0)
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y
                * (stats.fallMultiplier - 1) * Time.deltaTime;
    }
}