
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float mouseSpeed = 0.1f;
    [SerializeField] float powerUpDuration = 15f;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] AudioClip[] attackWhooshClips;
    [SerializeField] AudioClip[] stepsClips;
    [SerializeField] AudioClip[] jumpClips;
    [SerializeField] VisualEffect powerfulVFX = null;

    public InputAction MovementAction { get; private set; }
    public InputAction RotationAction { get; private set; }
    public InputAction RotationMouseAction { get; private set; }
    public InputAction JumpAction { get; private set; }
    public InputAction InteractAction { get; private set; }
    public InputAction AttackAction { get; private set; }

    public Animator Animator { get; private set; }

    public static PlayerMovement Instance;

    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;
    Rigidbody rb;
    bool isGrounded = true;
    bool lastGrounded = true;
    System.Action OnGrounded;
    float lastSpeedDir = 0f;
    float lastStepTime = 0f;
    float attackRange = 0.5f;
    float attackOffset1 = 0.15f;
    float attackOffset2 = 0.20f;
    float attackOffset3 = 0.25f;



    private void Awake()
    {
        Instance = this;

        Animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        MovementAction = playerInput.actions["Movement"];
        RotationAction = playerInput.actions["Rotation"];
        RotationMouseAction = playerInput.actions["RotationMouse"];
        InteractAction = playerInput.actions["Interact"];
        JumpAction = playerInput.actions["Jump"];
        AttackAction = playerInput.actions["Attack"];

        JumpAction.performed += (context) => 
        {
            if (isGrounded)
            {
                rb.velocity += Vector3.up * jumpForce;
                Animator.SetTrigger("StartJump");

                AudioManager.instance.PlayAudioClip(jumpClips[Random.Range(0, jumpClips.Length)], transform.position);
            }
        };

        AttackAction.performed += (context) =>
        {
            Attack();   
        };

        OnGrounded += () =>
        {
            Animator.SetTrigger("EndJump");
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

        bool running = Mathf.Abs(rb.velocity.x + rb.velocity.z) > 0.1f;

        Animator.SetBool("Running", running);

        if (running && lastStepTime > 0.25f && isGrounded)
        {
            AudioManager.instance.PlayAudioClip(stepsClips[Random.Range(0, stepsClips.Length)], transform.position);
            lastStepTime = 0;
        }
        else
        {
            lastStepTime += Time.deltaTime;
        }

        if (lastSpeedDir <= -0.5f)
        {
            spriteRenderer.flipX = true;
        }
        else if (lastSpeedDir >= 0.5f)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Attack()
    {
        Animator.SetTrigger("Attack");

        AudioManager.instance.PlayAudioClip(attackWhooshClips[Random.Range(0, attackWhooshClips.Length)], transform.position);
    }

    public void AttackAnimEvent()
    {
        Ray ray1 = new Ray(transform.position + (Vector3.up * 0.3f) + (transform.forward * attackOffset1), (transform.right * (spriteRenderer.flipX ? -1f : 1f)));
        Ray ray2 = new Ray(transform.position + (Vector3.up * 0.3f) + (transform.forward * attackOffset2), (transform.right * (spriteRenderer.flipX ? -1f : 1f)));
        Ray ray3 = new Ray(transform.position + (Vector3.up * 0.3f) + (transform.forward * attackOffset3), (transform.right * (spriteRenderer.flipX ? -1f : 1f)));
        
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, attackRange, -1, QueryTriggerInteraction.Ignore) ||
            Physics.Raycast(ray2, out hit, attackRange, -1, QueryTriggerInteraction.Ignore) ||
            Physics.Raycast(ray3, out hit, attackRange, -1, QueryTriggerInteraction.Ignore))
        {
            Destructible destructible = hit.collider.GetComponent<Destructible>();

            if (destructible)
            {
                Destroy(Instantiate(hitEffect, hit.point, Quaternion.identity), 1f);
                destructible.GetHitDamage();
            }
        }
    }

    public void PlayPowerUpAttackLoop()
    {
        StartCoroutine(PowerUpAttackLoop());
    }

    IEnumerator PowerUpAttackLoop()
    {
        powerfulVFX.enabled = true;
        float duration = 0f;
        float attackRate = 0.075f;
        float lastAttackTime = 0f;
        attackRange = 1f;
        attackOffset1 = 0.0f;
        attackOffset2 = 0.2f;
        attackOffset3 = 0.4f;


        while (duration < powerUpDuration)
        {
            duration += Time.deltaTime;

            if (lastAttackTime + attackRate < duration)
            {
                Attack();
                lastAttackTime = duration;
            }

            yield return null;
        }

        attackRange = 0.5f;
        attackOffset1 = 0.15f;
        attackOffset2 = 0.20f;
        attackOffset3 = 0.25f;

        powerfulVFX.enabled = false;
        inventory.Instance.SetFunnyObject(null);
        Animator.SetInteger("AttackIndex", 0);
    }
}