using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement and Sight")]
    public float moveSpeed = 3f;
    public float returnSpeed = 2.7f;
    public float targetRange = 7f;
    public float loseTargetRange = 10f;
    public float stopDistance = 0.75f;

    [Header("Ledge Detection")]
    public Transform ledgeCheck;
    public float ledgeRayLength = 1f;
    public LayerMask groundLayer;

    [Header("Damage")]
    public int contactDamage = 5;
    public float damageCooldown = 1f;


    private Transform player;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private bool isChasing = false;
    private float lastDamageTime = -999f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Save the enemy's initial spawn point

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float totalDistance = Vector2.Distance(transform.position, player.position);

        if (!isChasing && totalDistance <= targetRange)
        {
            isChasing = true;
        }
        else if (isChasing && totalDistance > loseTargetRange)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            MoveTowardX(player.position.x, moveSpeed, stopDistance);
        }
        else
        {
            
            MoveTowardX(startPosition.x, returnSpeed, 0.1f);
        }
    }
        

    private void MoveTowardX(float targetX, float speed, float threshold)
    {
        float horizontalDistance = Mathf.Abs(targetX - transform.position.x);

        if (horizontalDistance > threshold)
        {
            float directionX = Mathf.Sign(targetX - transform.position.x);
            transform.localScale = new Vector3(directionX, 1, 1);

            bool isGroundAhead = Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeRayLength, groundLayer);

            if (isGroundAhead)
            {
                rb.linearVelocity = new Vector2(directionX * speed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.TakeDamage(contactDamage);
                }

                lastDamageTime = Time.time;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, targetRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, loseTargetRange);

        if (ledgeCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * ledgeRayLength);
        }
    }
}
