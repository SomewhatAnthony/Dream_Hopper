using UnityEngine;
using UnityEngine.UI;


public class PlrShooting : MonoBehaviour
{

    public GameObject projectilePrefab;

    public float fireRate = 1f / 6f; // cap fire rate
    private float nextFireTime = 0f; // time when the player can fire again

    public Sprite shootSprite;
    public Sprite defaultSprite;

    public float shootDuration = 0.3f; // Duration of the shooting animation
    private float endDuration; 

    void Start() {
        nextFireTime = Time.time; // Initialize nextFireTime to the current time
        endDuration = Time.time; // Initialize endDuration to the current time plus the shooting duration
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

            endDuration = Time.time + shootDuration;
            GetComponent<SpriteRenderer>().sprite = shootSprite; // Change to shooting sprite
        }

        if (Time.time >= endDuration)
        {
            GetComponent<SpriteRenderer>().sprite = defaultSprite; // Change back to default sprite
        }
    }
}
