using UnityEngine;

public class NightmareHealth : MonoBehaviour
{

    public int Health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // have health scale by points
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerProjectile"))
        {
            Health -= 1;
            
            Debug.Log("Nightmare hit! Current Health: " + Health);
            
            if (Health <= 0)
            {
                //Destroy(gameObject); // Destroy the Nightmare object when health reaches 0

                Debug.Log("Nightmare defeated!");
            }

            Destroy(collider.gameObject);
        }
    }
}
