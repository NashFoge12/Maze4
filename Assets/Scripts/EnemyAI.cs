using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;

    [Header("Patrulla")]
    public Transform[] patrolPoints;

    [Header("Detección")]
    public float detectionRange = 10f;
    public float attackRange = 2f;

    [Header("Velocidades")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    private NavMeshAgent agent;
    private Animator animator;

    private int currentPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.speed = walkSpeed;

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(
                patrolPoints[currentPoint].position);
        }
    }

    void Update()
    {
        float distance =
            Vector3.Distance(
                transform.position,
                player.position);

        // ATAQUE
        if (distance <= attackRange)
        {
            agent.isStopped = true;
            animator.SetTrigger("Attack");
            animator.SetFloat("Speed", 0);
            return;
        }

        // PERSECUCIÓN
        if (distance <= detectionRange)
        {
            agent.isStopped = false;
            agent.speed = runSpeed;

            agent.SetDestination(player.position);

            animator.SetFloat("Speed", 5);
            return;
        }

        // PATRULLA
        Patrol();

        animator.SetFloat(
            "Speed",
            agent.velocity.magnitude);
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.isStopped = false;
        agent.speed = walkSpeed;

        if (!agent.pathPending &&
            agent.remainingDistance < 0.5f)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;

            agent.SetDestination(
                patrolPoints[currentPoint].position);
        }
    }
}