using UnityEngine;
using System.Collections;

public class PlrHealthManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public GameObject[] LivesObjects;
    public GameObject CurrentLifeUI;

    public float invincibilityDuration = 2f; // Duration of invincibility in seconds
    public float nextHitTime; // Time when the player can be hit again

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextHitTime = Time.time; // Initialize nextHitTime to the current time
        currentLives = maxLives;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hurt") && Time.time >= nextHitTime)
        {
            // Handle collision with projectile
            Debug.Log("Player hit by projectile!");

            Destroy(collision.gameObject); // delete projectile

            // lose life, activate invincibility frames

            currentLives--;
            nextHitTime = Time.time + invincibilityDuration;

            StartCoroutine(InvincibilityFrames());

            // switch currentLifeUI's gameobject image to the next one in the array



            if (currentLives <= 0) {
                Debug.Log("Player has no lives left!");

            } 
        }
    }

    IEnumerator InvincibilityFrames()
    {
        // make player blink for a few seconds to indicate invincibility
        for (int i = 0; i < 12; i++) {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(invincibilityDuration / (5 * 2));
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(invincibilityDuration / (5 * 2));
        }

    }
}
