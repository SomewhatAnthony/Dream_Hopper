using UnityEngine;

public class PlrShooting : MonoBehaviour
{

    public GameObject projectilePrefab;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Spawn projectile logic here
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotate the projectile to face upwards
            projectile.GetComponent<ProjectileScript>().lifeTime = 4f;
        }
    }
}
