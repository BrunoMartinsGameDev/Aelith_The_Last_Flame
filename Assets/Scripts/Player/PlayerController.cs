using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private PlayerStatsSO stats;

    private Rigidbody2D _rb;
    private bool        _inputEnabled = true;
    private bool        _facingRight  = true;

    public float MoveInput { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_inputEnabled) return;
        MoveInput = Input.GetAxisRaw("Horizontal");
        HandleFlip();
    }

    void FixedUpdate()
    {
        if (!_inputEnabled) return;
        _rb.linearVelocity = new Vector2(
            MoveInput * stats.moveSpeed, _rb.linearVelocity.y);
    }

    private void HandleFlip()
    {
        if (MoveInput > 0 && !_facingRight) Flip();
        else if (MoveInput < 0 && _facingRight) Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(
            -transform.localScale.x,
             transform.localScale.y,
             transform.localScale.z);
    }

    public void SetInputEnabled(bool enabled) => _inputEnabled = enabled;
}