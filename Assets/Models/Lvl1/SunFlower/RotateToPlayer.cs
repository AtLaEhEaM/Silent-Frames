using UnityEngine;

public class FlowerLookTarget : MonoBehaviour
{
    public Transform head;
    public Transform player;
    public Transform idleTarget;

    public float lookDistance = 10f;
    public float rotationSpeed = 5f;

    public bool lookAtPlayer = false;

    float lookDistanceSqr;

    Quaternion idleRotation;
    bool isIdleLocked;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerLookAtPos").transform;

        lookDistanceSqr = lookDistance * lookDistance;

        idleRotation = head.rotation;
    }

    void Update()
    {
        if (!lookAtPlayer)
        {
            ReturnToIdle();
            return;
        }

        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        isIdleLocked = false;

        Vector3 dir = player.position - head.position;

        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        head.rotation = Quaternion.Slerp(
            head.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    void ReturnToIdle()
    {
        if (isIdleLocked) return;

        head.rotation = Quaternion.Slerp(
            head.rotation,
            idleRotation,
            rotationSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(head.rotation, idleRotation) < 0.5f)
        {
            head.rotation = idleRotation;
            isIdleLocked = true;
        }
    }

    public void ForceLook(bool state)
    {
        lookAtPlayer = state;
    }
}