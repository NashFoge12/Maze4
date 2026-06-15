using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    [Header("Patrulla")]
    public float wanderRadius = 10f;
    public float waitTimeMin = 1f;
    public float waitTimeMax = 3f;
    public float reachDistance = 0.5f;

    [Header("Sonido de pasos")]
    public AudioClip footstepSound;
    public float footstepInterval = 0.4f; // Cada cuánto suena un paso

    private NavMeshAgent agent;
    private Vector3 targetPoint;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private float arrivalCheckDelay = 0.5f;
    private float arrivalCheckTimer = 0f;
    private Animator animator;
    private AudioSource audioSource;
    private float footstepTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
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
                arrivalCheckTimer = 0f;
                GoToRandomPoint();
            }
        }
        else
        {
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
        UpdateFootsteps();
    }

    void UpdateFootsteps()
    {
        if (isWaiting || footstepSound == null || audioSource == null) return;

        // Solo suena si el NPC se está moviendo
        if (agent.velocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                audioSource.PlayOneShot(footstepSound);
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    void UpdateAnimation()
    {
        if (animator == null) return;
        float speed = isWaiting ? 0f : agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void GoToRandomPoint()
    {
        arrivalCheckTimer = 0f;
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