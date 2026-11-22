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

    [Header("Bounce Settings")]
    public Transform visualChild;
    public float bounceSpeed = 12f;
    public float bounceHeight = 0.15f;

    private Vector3 initialChildPos;
    private float bounceTimer;


    [Header("Background Parallax")]
    public Transform background;
    [Range(0f, 0.5f)]
    public float parallaxStrength = 0.05f;
    private Vector3 initialBgPos;
    private Vector3 camCenter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Checks parent and visualChild so camera clamping stays accurate
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x;
            halfHeight = sr.bounds.extents.y;
        }

        nextDashTime = Time.time;
        CalculateCameraBounds();

        if (visualChild != null)
        {
            initialChildPos = visualChild.localPosition;
        }

        if (background != null)
        {
            initialBgPos = background.position;
        }
    }

    void CalculateCameraBounds()
    {
        Camera cam = Camera.main;

        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, cam.nearClipPlane));
        Vector2 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, cam.nearClipPlane));

        minX = bottomLeft.x + halfWidth;
        maxX = topRight.x - halfWidth;
        minY = bottomLeft.y + halfHeight;
        maxY = topRight.y - halfHeight;
    }

    void Update()
    {
        // Read all player inputs in Update() so no button presses are ever dropped
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput != Vector2.zero)
        {
            lastMoveDir = moveInput;
        }

        if (Input.GetButtonDown("Dash") && !isDashing && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
        }

        // Bounce animation logic
        if (visualChild == null) return;

        if (moveInput != Vector2.zero && !isDashing)
        {
            bounceTimer += Time.deltaTime * bounceSpeed;
            float bounceOffset = Mathf.Abs(Mathf.Sin(bounceTimer)) * bounceHeight;
            visualChild.localPosition = initialChildPos + new Vector3(0f, bounceOffset, 0f);
        }
        else
        {
            bounceTimer = 0f;
            visualChild.localPosition = Vector3.Lerp(visualChild.localPosition, initialChildPos, Time.deltaTime * 15f);
        }
    }

    void FixedUpdate()
    {
        // Clamp position inside camera bounds
        Vector2 clampedPos = rb.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        rb.position = clampedPos;

        // Only apply regular movement velocity when not dashing
        if (!isDashing)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    void LateUpdate()
    {
        // Move the background slightly opposite to the player's position after physics/movement finishes
        if (background != null)
        {
            Vector3 playerOffset = new Vector3(transform.position.x - camCenter.x, transform.position.y - camCenter.y, 100f);
            Vector3 targetBgPos = initialBgPos - (playerOffset * parallaxStrength);
            background.position = Vector3.Lerp(background.position, targetBgPos, Time.deltaTime * 10f);
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;

        Vector2 dashDir = (moveInput != Vector2.zero) ? moveInput : lastMoveDir;
        rb.linearVelocity = dashDir * dashSpeed;

        // WaitForFixedUpdate ensures the dash ends cleanly on a physics tick
        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            rb.linearVelocity = dashDir * dashSpeed;
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isDashing = false;
    }
}