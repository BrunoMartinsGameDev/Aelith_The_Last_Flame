using UnityEngine;

public enum EnemyState { Patrol, Chase, Attack }

[RequireComponent(typeof(EnemyBase))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyDataSO data;

    private EnemyState _state = EnemyState.Patrol;
    private Transform  _player;
    private Vector2    _startPos;

    void Start()
    {
        _startPos = transform.position;
        _player   = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (_player == null) return;
        float dist = Vector2.Distance(transform.position, _player.position);

        _state = dist <= data.attackRange    ? EnemyState.Attack :
                 dist <= data.detectionRange  ? EnemyState.Chase  :
                                                EnemyState.Patrol;
        HandleState();
    }

    private void HandleState()
    {
        // movimento por estado — implementar no Sáb W1
    }
}