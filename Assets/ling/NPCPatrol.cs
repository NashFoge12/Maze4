using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    [Header("Patrulla")]
    public float wanderRadius = 10f;
    public float waitTimeMin = 1f;
    public float waitTimeMax = 3f;
    public float reachDistance = 0.5f;

    private NavMeshAgent agent;
    private Vector3 targetPoint;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    // Pequeño timer para evitar que detecte "llegó" demasiado pronto
    private float arrivalCheckDelay = 0.5f;
    private float arrivalCheckTimer = 0f;

    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        GoToRandomPoint();
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                arrivalCheckTimer = 0f; // Reinicia el timer al salir de espera
                GoToRandomPoint();
            }
        }
        else
        {
            // Espera un poco antes de empezar a verificar si llegó
            arrivalCheckTimer += Time.deltaTime;

            if (arrivalCheckTimer >= arrivalCheckDelay)
            {
                if (!agent.pathPending && agent.remainingDistance <= reachDistance)
                {
                    StartWaiting();
                }
            }
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (animator == null) return;
        float speed = isWaiting ? 0f : agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void GoToRandomPoint()
    {
        arrivalCheckTimer = 0f; // Reinicia el timer cada vez que busca un nuevo punto

        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
            {
                targetPoint = hit.position;
                agent.SetDestination(targetPoint);
                return;
            }
        }
    }

    void StartWaiting()
    {
        isWaiting = true;
        waitTimer = Random.Range(waitTimeMin, waitTimeMax);
        agent.ResetPath();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}