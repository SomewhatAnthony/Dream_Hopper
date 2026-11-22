using System.Collections;
using UnityEngine;

public class PlrMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.right; 
    public bool isDashing = false;
    private float nextDashTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextDashTime = Time.time;
    }

    void Update()
    {
        // Prevent normal movement from overriding velocity while dashing
        if (isDashing) return;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Remember the last direction moved so the player can still dash while standing still
        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }

        // Check for "Dash" button press and verify the cooldown has passed
        if (Input.GetButtonDown("Dash") && Time.time >= nextDashTime)
        {
            Debug.Log("Dash activated!");
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        // Keep physics velocity updates in FixedUpdate for smoother collisions
        if (!isDashing)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown; 

        // Dash in current input direction, or last faced direction if idle
        Vector2 dashDir = (moveInput != Vector2.zero) ? moveInput : lastMoveDir;
        rb.linearVelocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}