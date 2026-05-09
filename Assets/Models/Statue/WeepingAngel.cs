using UnityEngine;
using UnityEngine.AI;

public class YTWeepingAngel : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Camera playerCam;

    [Header("Movement")]
    public float aiSpeed = 3.5f;
    public float senseRange = 15f;
    public float rotationSpeed = 10f;

    [Header("States")]
    public bool isChasing;
    public bool isLookedAt;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip walkingsfx;

    Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        ai.speed = aiSpeed;

        ai.updateRotation = false;
    }

    void Update()
    {
        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance < senseRange)
        {
            HandleStalking();
        }

        HandleWalkingAudio();
    }

    void HandleStalking()
    {
        Plane[] planes =
            GeometryUtility.CalculateFrustumPlanes(playerCam);

        isChasing = true;

        bool visible = false;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (GeometryUtility.TestPlanesAABB(
                planes,
                renderers[i].bounds))
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

            RotateTowardsPlayer();
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 dir =
            player.position - transform.position;

        dir.y = 0f;

        if (dir.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(-dir);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
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