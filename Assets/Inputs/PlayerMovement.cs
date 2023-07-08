using UnityEngine;
using UnityEngine.InputSystem;
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
    public float horizontal;
    private SpriteRenderer sr;
    public Rigidbody2D rb;
    public PlayerStats player;
    private bool isFacingRight;

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

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
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
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (value.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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