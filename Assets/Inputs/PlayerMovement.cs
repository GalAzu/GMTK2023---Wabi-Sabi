using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed { get => player.moveSpeed; }
    public float jumpForce { get => player.jumpForce; }
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private bool isCrouching;
    private SpriteRenderer sr;
    public Rigidbody2D rb;
    public PlayerStats player;
    public bool isFacingRight;
    [Header("Movement")]
    private float horizontal;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float velPower;
    [Header("Jumping")]
    [SerializeField] private float midAirSlowFactor;

    private float lastGroundedeTime;
    private float lastJumpTime;
    void Awake()
    {
        player = GetComponent<PlayerStats>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (!isGrounded && isJumping)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed * midAirSlowFactor, rb.velocity.y);
            rb.gravityScale = 1.7f;
        }
        else
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            rb.gravityScale = 1;
        }
    }
    public void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (!isGrounded)
            animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);
    }
    private void Update()
    {
        IsGrounded();
        //RUNNING
        // float targetSpeed = horizontal * moveSpeed;
        // float speedDiff = targetSpeed - rb.velocity.x;
        // float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        // float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);
        // rb.AddForce(movement * Vector2.right);


        //Jumping
        lastGroundedeTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;

        if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            horizontal = context.ReadValue<Vector2>().x * 1;
            animator.SetBool("isMoving", Mathf.Abs(horizontal) > 0 ? true : false);
        }
        else
        {
            horizontal = context.ReadValue<Vector2>().x * midAirSlowFactor;
            animator.SetBool("isJumping", true);
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
        if (value.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            animator.SetBool("isJumping", false);
        }
    }

    public void OnCrouch(InputAction.CallbackContext value)
    {
        if (value.performed && isGrounded)
        {
            isCrouching = true;
        }
        if (value.canceled && isCrouching)
        {
            isCrouching = false;
        }
    }
}