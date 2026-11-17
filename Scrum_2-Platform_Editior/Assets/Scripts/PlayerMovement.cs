using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float moveX;
    private bool isGrounded;
    private Vector3 spawnPoint;
    private bool ones = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        spawnPoint = transform.position;
    }

    void Update()
    {
        moveX = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveX = -1f;
            if (!ones)
            {
                GameManager.Instance.startTimer();
                ones = true;
            }
            
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveX = 1f;
            if (!ones)
            {
                GameManager.Instance.startTimer();
                ones = true;
            }

        if (moveX > 0f)
            sr.flipX = true;
        else if (moveX < 0f)
            sr.flipX = false;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            GameManager.Instance.startTimer();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    public void Die()
    {
        Debug.Log("Player died");
        transform.position = spawnPoint;
        rb.linearVelocity = Vector2.zero;
        GameManager.Instance.TimeReset();
    }
}