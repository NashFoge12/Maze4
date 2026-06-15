using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;

    [Header("Patrulla")]
    public Transform[] patrolPoints;

    [Header("Detección")]
    public float detectionRange = 10f;
    public float attackRange = 2f;

    [Header("Velocidades")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    [Header("Ataque")]
    public float attackCooldown = 1.5f;
    public int damage = 1;

    private NavMeshAgent agent;
    private Animator animator;

    private int currentPoint;
    private float attackTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");

            if (p != null)
                player = p.transform;
        }

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        attackTimer += Time.deltaTime;

        // ATAQUE
        if (distance <= attackRange)
        {
            agent.isStopped = true;

            animator.SetFloat("Speed", 0f);

            Vector3 lookPos = player.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);

            if (attackTimer >= attackCooldown)
            {
                animator.SetTrigger("Attack");

                PlayerStats stats = player.GetComponent<PlayerStats>();

                if (stats != null)
                {
                    stats.TakeDamage(damage);
                }

                attackTimer = 0f;
            }

            return;
        }

        // PERSECUCIÓN
        if (distance <= detectionRange)
        {
            agent.isStopped = false;
            agent.speed = runSpeed;
            agent.SetDestination(player.position);

            animator.SetFloat("Speed", 5f);

            return;
        }

        // PATRULLA
        Patrol();

        animator.SetFloat("Speed", 1f);
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.isStopped = false;
        agent.speed = walkSpeed;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;

            agent.SetDestination(patrolPoints[currentPoint].position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}