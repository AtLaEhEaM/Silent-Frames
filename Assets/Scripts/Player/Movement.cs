using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    public bool allowMovement = true;

    [Header("Jumping")]
    public float jumpForce = 5f;

    public int maxJumps = 1;

    public bool allowJumping = true;

    [Header("Ground Check")]

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask = ~0;

    public Rigidbody rb;

    Vector3 movementInput;
    bool jumpRequested;
    int jumpsPerformed;
    bool isGrounded;
    public bool isMoving = false;

    void Start()
    {
        if (groundCheck == null) groundCheck = transform;
        if (maxJumps < 1) maxJumps = 1;
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (allowMovement)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 right = transform.right * h;
            Vector3 forward = transform.forward * v;
            Vector3 desired = (right + forward).normalized * moveSpeed;
            movementInput = desired;
            isMoving = movementInput.sqrMagnitude > 0.001f;
        }
        else
        {
            movementInput = Vector3.zero;
            isMoving = false;
        }

        if (allowJumping && Input.GetButtonDown("Jump"))
        {
            if (isGrounded || jumpsPerformed < maxJumps)
            {
                jumpRequested = true;
            }
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();

        Vector3 vel = rb.velocity;
        vel.x = movementInput.x;
        vel.z = movementInput.z;
        rb.velocity = vel;

        if (jumpRequested)
        {
            Vector3 v = rb.velocity;
            v.y = 0f;
            rb.velocity = v;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            jumpsPerformed++;
            jumpRequested = false;
        }
    }

    void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask, QueryTriggerInteraction.Ignore);
        if (isGrounded && !wasGrounded)
        {
            jumpsPerformed = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
