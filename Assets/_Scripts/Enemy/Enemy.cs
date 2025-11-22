using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("References")]
    public Transform player;
    public SpriteRenderer sr;

    [Header("Settings")]
    public float rotateSpeed = 2f;
    public float attackRange = 1f;
    public float sightRange = 10f;
    
    public StateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public StandbyState StandbyState { get; private set; }
    public TelegraphState TelegraphState { get; private set; }
    public AttackState AttackState  { get; private set; }
    
    public RecoverState RecoverState { get; private set; }

    private Collider2D playerCol;
    
    private void Awake()
    {
        StateMachine = new StateMachine();
        IdleState = new IdleState(this);
        StandbyState = new StandbyState(this);
        TelegraphState = new TelegraphState(this);
        AttackState = new AttackState(this);
        RecoverState = new RecoverState(this);
        
        playerCol = player.GetComponent<Collider2D>();
    }
    
    private void Start()
    {
        StateMachine.ChangeState(IdleState);
    }
    
    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }
    
    public void RotateTowardsPlayer()
    {
        RotateTowards(player.position);
    }

    public void RotateTowards(Vector2 target)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float smoothed = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Euler(0, 0, smoothed);
    }

    public bool PlayerInAttackRange()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }

    public bool PlayerInSightRange()
    {
        return Vector2.Distance(transform.position, player.position) <= sightRange;
    }
    
    public bool CanSeePlayer()
    {
        // False if out of sight range
        if (!PlayerInSightRange()) return false;

        // Get player collider bounds
        var playerBounds = playerCol.bounds;

        // Test rays to center and all corners
        var playerVisiblePoints = new Vector2[]
        {
            playerBounds.center,
            new (playerBounds.max.x, playerBounds.max.y),
            new (playerBounds.max.x, playerBounds.min.y),
            new (playerBounds.min.x, playerBounds.max.y),
            new (playerBounds.min.x, playerBounds.min.y)
        };

        foreach (var p in playerVisiblePoints)
        {
            Vector2 d = (p - (Vector2)transform.position).normalized;
            float dist = Vector2.Distance(transform.position, p);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, d, dist);

            if (hit.collider != null && hit.collider == playerCol)
                return true;
        }

        return false;
    }


    private void OnDrawGizmosSelected()
    {
        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (Vector2.Distance(transform.position, player.position) > sightRange) return;
        
        if (player == null) return;

        // Get player collider bounds
        var col = player.GetComponent<Collider2D>();
        if (col == null) return;

        var playerBounds = col.bounds;
        
        var playerVisiblePoints = new Vector2[]
        {
            playerBounds.center,
            new (playerBounds.max.x, playerBounds.max.y),
            new (playerBounds.max.x, playerBounds.min.y),
            new (playerBounds.min.x, playerBounds.max.y),
            new (playerBounds.min.x, playerBounds.min.y)
        };
        
        // Vision 
        Gizmos.color = Color.magenta;
        foreach (var p in playerVisiblePoints)
        {
            Gizmos.DrawLine(transform.position, p);
        }
    }
}
