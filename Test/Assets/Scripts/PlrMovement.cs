using System.Collections;
using UnityEngine;

public class PlrMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float minX, maxX, minY, maxY;
    private float halfWidth, halfHeight;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private Vector2 lastMoveDir = Vector2.right; 
    public bool isDashing = false;
    private float nextDashTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x;
            halfHeight = sr.bounds.extents.y;
        }
        nextDashTime = Time.time;
        CalculateCameraBounds();
    }
    void CalculateCameraBounds()
    {
        Camera cam = Camera.main;

        // Viewport (0,0) is bottom-left; (1,1) is top-right
        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector2 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        // Add padding using the sprite's half-size
        minX = bottomLeft.x + halfWidth;
        maxX = topRight.x - halfWidth;
        minY = bottomLeft.y + halfHeight;
        maxY = topRight.y - halfHeight;
        
    }

    void Update()
    {



        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //rb.linearVelocity = moveInput * moveSpeed;

    }
    void FixedUpdate()
    {
        // Clamp the Rigidbody2D position directly to prevent physics jitter
        Vector2 clampedPos = rb.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        rb.position = clampedPos;

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