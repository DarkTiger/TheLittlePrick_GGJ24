using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float movementDrag = 10f;

    [SerializeField] private Interacter InteractionPoint;

    public InputAction MovementAction { get; private set; }
    public InputAction RotationAction { get; private set; }
    public InputAction JumpAction { get; private set; }
    public InputAction InteractAction { get; private set; }

    PlayerInput playerInput;
    Rigidbody rb;
    bool isGrounded  = false;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        MovementAction = playerInput.actions["Movement"];
        RotationAction = playerInput.actions["Rotation"];
        InteractAction = playerInput.actions["Interact"];
        JumpAction = playerInput.actions["Jump"];

        JumpAction.performed += (context) => { if (isGrounded) { rb.velocity += Vector3.up * jumpForce; } };
    }

    private void Update()
    {
        rb.velocity -= new Vector3(rb.velocity.x, 0f, rb.velocity.z) * Time.deltaTime * movementDrag;


        Vector2 movementValue = MovementAction.ReadValue<Vector2>();

        if (movementValue.magnitude > 0.2f || movementValue.magnitude < -0.2f)
        {
            float velocityY = rb.velocity.y;
            rb.velocity = ((transform.forward * movementValue.y) + (transform.right * movementValue.x)) * movementSpeed;
            rb.velocity = new Vector3(rb.velocity.x, velocityY, rb.velocity.z);
        }

        float rotationValue = RotationAction.ReadValue<Vector2>().x;

        if (rotationValue > 0.2f || rotationValue < -0.2f)
        {
            rb.angularVelocity += Vector3.up * rotationValue * rotationSpeed;
        }

        isGrounded = Physics.Raycast(new Ray(transform.position -(transform.up * 0.95f), -transform.up), 0.25f);


    }

    //private void Interaction()
    //{
    //    InteractionPoint.Interaction(this);
    //}
}