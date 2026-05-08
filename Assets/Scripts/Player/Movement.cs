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

    [Header("References")]
    public Rigidbody rb;

    public AudioSource audioSource;
    public AudioClip walkingClip;

    Vector3 movementInput;
    bool jumpRequested;

    int jumpsPerformed;

    public bool isGrounded;
    public bool isMoving;

    void Start()
    {
        if (groundCheck == null)
            groundCheck = transform;

        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (maxJumps < 1)
            maxJumps = 1;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleInput();

        if (isMoving && isGrounded)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkingClip;
                audioSource.loop = false;
                audioSource.Play();
            }
        }

        if (!isGrounded)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
        HandleJump();
    }

    void HandleInput()
    {
        if (!allowMovement)
        {
            movementInput = Vector3.zero;
            isMoving = false;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move =
            (transform.right * h + transform.forward * v).normalized;

        movementInput = move * moveSpeed;

        isMoving = move.sqrMagnitude > 0.001f;

        if (allowJumping && Input.GetButtonDown("Jump"))
        {
            if (isGrounded || jumpsPerformed < maxJumps)
            {
                jumpRequested = true;
            }
        }
    }

    void HandleMovement()
    {
        Vector3 velocity = rb.linearVelocity;

        velocity.x = movementInput.x;
        velocity.z = movementInput.z;

        rb.linearVelocity = velocity;
    }

    void HandleJump()
    {
        if (!jumpRequested)
            return;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;

        rb.linearVelocity = velocity;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        jumpsPerformed++;

        jumpRequested = false;
    }

    void CheckGrounded()
    {
        bool wasGrounded = isGrounded;

        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask,
            QueryTriggerInteraction.Ignore
        );

        if (isGrounded && !wasGrounded)
        {
            jumpsPerformed = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}