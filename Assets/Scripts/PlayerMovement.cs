using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float mouseSpeed = 0.1f;

    public InputAction MovementAction { get; private set; }
    public InputAction RotationAction { get; private set; }
    public InputAction RotationMouseAction { get; private set; }
    public InputAction JumpAction { get; private set; }
    public InputAction InteractAction { get; private set; }

    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;
    Rigidbody rb;
    bool isGrounded = true;
    bool lastGrounded = true;
    Action OnGrounded;
    float lastSpeedDir = 0f;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MovementAction = playerInput.actions["Movement"];
        RotationAction = playerInput.actions["Rotation"];
        RotationMouseAction = playerInput.actions["RotationMouse"];
        InteractAction = playerInput.actions["Interact"];
        JumpAction = playerInput.actions["Jump"];

        JumpAction.performed += (context) => {
            if (isGrounded)
            {
                rb.velocity += Vector3.up * jumpForce;
                animator.SetTrigger("StartJump");
            }
        };

        OnGrounded += () =>
        {
            animator.SetTrigger("EndJump");
        };
    }

    private void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x * 0.5f * Time.deltaTime, rb.velocity.y, rb.velocity.z * 0.5f * Time.deltaTime);

        Vector2 movementValue = MovementAction.ReadValue<Vector2>();
        lastSpeedDir = movementValue.x;

        if (movementValue.magnitude > 0.2f || movementValue.magnitude < -0.2f)
        {
            float velocityY = rb.velocity.y;
            rb.velocity = ((transform.forward * movementValue.y) + (transform.right * movementValue.x)) * movementSpeed;
            rb.velocity = new Vector3(rb.velocity.x, velocityY, rb.velocity.z);
        }

        float rotationValue = RotationAction.ReadValue<Vector2>().x + (RotationMouseAction.ReadValue<Vector2>().x * mouseSpeed);

        if (rotationValue > 0.2f || rotationValue < -0.2f)
        {
            rb.angularVelocity += Vector3.up * rotationValue * rotationSpeed;
        }

        isGrounded = Physics.Raycast(new Ray(transform.position - (transform.up * -0.05f), -transform.up), 0.15f);

        if (isGrounded != lastGrounded)
        {
            lastGrounded = isGrounded;

            if (isGrounded)
            {
                OnGrounded?.Invoke();
            }
        }
        animator.SetBool("Running", Mathf.Abs(rb.velocity.x + rb.velocity.z) > 0.1f);
        spriteRenderer.flipX = lastSpeedDir < 0f;
    }
}