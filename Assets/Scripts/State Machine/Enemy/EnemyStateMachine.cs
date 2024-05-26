using UnityEngine;
using Pathfinding;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public float DetectionRange { get; private set; }
    [field: SerializeField] public float CannonRange { get; private set; }
    [field: SerializeField] public float CannonCooldown { get; private set; }
    [field: SerializeField] public LayerMask PlayerBaseLayer { get; private set; }

    [field: SerializeField] public Transform Target { get; private set; }

    public float CannonReadyTime { get; private set; } = 0f;

    public IAstarAI AI { get; private set; }
    public Rigidbody2D RigidBody { get; private set; }
    public Transform PlayerTransform { get; private set; }
    public Cannon Cannon { get; private set; }

    private void Awake()
    {
        AI = GetComponent<IAstarAI>();
        RigidBody = GetComponent<Rigidbody2D>();
        Cannon = GetComponentInChildren<Cannon>();
    }

    private void OnEnable()
    {
        GameManager.OnEnemyWin += HandleEnemyWin;
    }

    private void OnDisable()
    {
        GameManager.OnEnemyWin -= HandleEnemyWin;
    }

    private void Start()
    {
        PlayerTransform = PlayerController.Instance.gameObject.transform;

        SwitchState(new EnemyIdleState(this));
    }

    public void ResetCannonReadyTime()
    {
        CannonReadyTime = Time.time + CannonCooldown;
    }

    private void HandleEnemyWin()
    {
        SwitchState(new EnemyWinningState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CannonRange);
    }
}
