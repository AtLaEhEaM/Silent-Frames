using UnityEngine;
using UnityEngine.AI;

public class YTWeepingAngel : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Camera playerCam;

    [Header("Movement")]
    public float aiSpeed;
    public float senseRange;
    public float walkPointRange;

    [Header("States")]
    public bool isChasing;
    public bool isPatrolling;
    public bool isLookedAt;

    Vector3 walkpoint;
    bool walkPointSet;
    float distance;

    public AudioSource audioSource;
    public AudioClip walkingsfx;

    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (distance < senseRange)
        {
            HandleStalking();
        }
        else
        {
            Patrolling();
        }

        HandleWalkingAudio();
    }

    void HandleStalking()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

        isChasing = true;
        isPatrolling = false;

        bool visible = false;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (GeometryUtility.TestPlanesAABB(planes, renderers[i].bounds))
            {
                visible = true;
                break;
            }
        }

        if (visible)
        {
            isLookedAt = true;

            ai.speed = 0f;

            if (ai.hasPath)
            {
                ai.ResetPath();
            }
        }
        else
        {
            isLookedAt = false;

            ai.speed = aiSpeed;
            ai.SetDestination(player.position);
        }
    }

    void Patrolling()
    {
        isPatrolling = true;
        isChasing = false;
        isLookedAt = false;

        ai.speed = aiSpeed;

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            ai.SetDestination(walkpoint);
        }

        Vector3 distanceToWalk = transform.position - walkpoint;

        if (distanceToWalk.magnitude < 2f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        Vector3 point =
            new Vector3(
                transform.position.x + randomX,
                transform.position.y,
                transform.position.z + randomZ
            );

        NavMeshHit hit;

        if (NavMesh.SamplePosition(point, out hit, walkPointRange, NavMesh.AllAreas))
        {
            walkpoint = hit.position;
            walkPointSet = true;
        }
    }

    void HandleWalkingAudio()
    {
        bool isMoving =
            ai.velocity.magnitude > 0.1f &&
            !isLookedAt;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkingsfx;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}