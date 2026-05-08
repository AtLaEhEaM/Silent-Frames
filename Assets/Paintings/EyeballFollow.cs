using UnityEngine;

public class MoveTowardsPlayerLocal : MonoBehaviour
{
    public Transform player;

    public float moveSpeed = 5f;
    public float maxDistance = 3f;

    Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 targetWorld = player.position;

        Vector3 localTarget = transform.parent != null
            ? transform.parent.InverseTransformPoint(targetWorld)
            : targetWorld;

        Vector3 current = transform.localPosition;

        Vector3 targetPos = new Vector3(
            localTarget.x,
            localTarget.y,
            current.z
        );

        Vector3 offsetFromStart = targetPos - startLocalPos;

        offsetFromStart = Vector3.ClampMagnitude(offsetFromStart, maxDistance);

        targetPos = startLocalPos + offsetFromStart;

        transform.localPosition = Vector3.MoveTowards(
            current,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }
}