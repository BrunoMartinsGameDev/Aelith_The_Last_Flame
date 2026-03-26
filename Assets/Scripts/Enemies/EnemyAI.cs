using UnityEngine;

public enum EnemyState { Patrol, Chase, Attack, Dead }

[RequireComponent(typeof(EnemyBase))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyDataSO data;

    private EnemyState _state = EnemyState.Patrol;
    private Rigidbody2D _rb;
    private EnemyBase _base;
    private Transform _player;
    private Vector2 _startPos;
    private float _patrolDir = 1f;
    private float _lastAttackTime;
    private float _lastPatrolFlipTime;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _base = GetComponent<EnemyBase>();
        _startPos = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (_base.IsDead) return;
        if (_player == null) return;
        UpdateState();
        HandleState();
        HandleFlip();
    }

    private void UpdateState()
    {
        float dist = Vector2.Distance(transform.position, _player.position);

        _state = dist <= data.attackRange ? EnemyState.Attack :
                 dist <= data.detectionRange ? EnemyState.Chase :
                                                EnemyState.Patrol;
    }
    private void HandleState()
    {
        switch (_state)
        {
            case EnemyState.Patrol: Patrol(); break;
            case EnemyState.Chase: Chase(); break;
            case EnemyState.Attack: Attack(); break;
        }
    }

    private void Patrol()
    {

        //inverte direção ao chegar no limite
        float distFromStart = transform.position.x - _startPos.x;

        // Se esta muito longe do ponto inicial, volta para ele primeiro
        if (Mathf.Abs(distFromStart) >= data.patrolDistance)
        {
            float dirToStart = distFromStart > 0 ? -1f : 1f;
            _rb.linearVelocity = new Vector2(dirToStart * data.moveSpeed * 0.5f, _rb.linearVelocity.y);
            _patrolDir = dirToStart;
            return;
        }

        // patrulha normal
        _rb.linearVelocity = new Vector2(_patrolDir * data.moveSpeed * 0.5f,
            _rb.linearVelocity.y);
        if (Mathf.Abs(distFromStart) >= data.patrolDistance && Time.time - _lastPatrolFlipTime > 0.5f)
        {
            _patrolDir *= -1f;
            _lastPatrolFlipTime = Time.time;
        }
    }

    private void Chase()
    {
        float dir = (_player.position.x - transform.position.x) > 0 ? 1f : -1f;
        _rb.linearVelocity = new Vector2(dir * data.moveSpeed, _rb.linearVelocity.y);
    }

    private void Attack()
    {
        // para ao atacar
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);

        if (Time.time - _lastAttackTime < 1f) return;
        _lastAttackTime = Time.time;

        var health = _player.GetComponent<PlayerHealth>();
        if (health == null) return;

        Vector2 knockbackDir = (_player.position - transform.position).normalized;
        health.TakeDamage(data.damage, knockbackDir);
    }

    private void HandleFlip()
    {
        if (_rb.linearVelocity.x > 0.1f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (_rb.linearVelocity.x < -0.1f) transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}