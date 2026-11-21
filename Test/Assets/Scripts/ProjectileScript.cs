using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public bool isWavy = false;
    public bool isHoming = false;
    
    public float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Fixed Update fires at a rate of 50 fps for consistent movement
    void FixedUpdate()
    {
        // Move the projectile forward
        transform.Translate(Vector3.up * Time.fixedDeltaTime * speed);
    }
}
