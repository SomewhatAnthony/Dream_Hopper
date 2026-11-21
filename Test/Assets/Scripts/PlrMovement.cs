using UnityEngine;
using UnityEngine.InputSystem;

public class PlrMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        rb.linearVelocity = moveInput * moveSpeed;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hurt"))
        {
            // Handle collision with projectile
            Debug.Log("Player hit by projectile!");

            Destroy(collision.gameObject); // delete projectile

            // lose life, activate invincibility frames
        }
    }

}
