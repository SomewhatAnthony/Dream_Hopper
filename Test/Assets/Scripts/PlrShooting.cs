using UnityEngine;

public class PlrShooting : MonoBehaviour
{

    public GameObject projectilePrefab;

    public float fireRate = 1f / 8f; // cap fire rate
    private float nextFireTime = 0f; // time when the player can fire again

    void Start() {
        nextFireTime = Time.time; // Initialize nextFireTime to the current time
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            // Spawn projectile logic here
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotate the projectile to face upwards
            projectile.GetComponent<ProjectileScript>().lifeTime = 4f;
            
            nextFireTime = Time.time + fireRate;
        }
    }
}
