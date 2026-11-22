using UnityEngine;
using UnityEngine.InputSystem;

public class PlrMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float minX, maxX, minY, maxY;
    private float halfWidth, halfHeight;
    private Rigidbody2D rb;
    private Vector2 moveInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x;
            halfHeight = sr.bounds.extents.y;
        }

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

    // Update is called once per frame
    void Update()
    {



        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //rb.linearVelocity = moveInput * moveSpeed;

    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        // Clamp the Rigidbody2D position directly to prevent physics jitter
        Vector2 clampedPos = rb.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.y = Mathf.Clamp(clampedPos.y, minY, maxY);
        rb.position = clampedPos;
    }



}
