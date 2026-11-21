using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlrHealthManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public SpriteRenderer PlayerSprite;
    public Sprite[] LivesImages;
    public GameObject currentHealthObject; 

    public float invincibilityDuration = 2f; // Duration of invincibility in seconds
    public float nextHitTime; // Time when the player can be hit again

    private Vector3 originalPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextHitTime = Time.time; // Initialize nextHitTime to the current time
        currentLives = maxLives;

        originalPos = currentHealthObject.transform.position;
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
            StartCoroutine(ShakeRoutine(0.4f, 0.1f));

            // switch currentLifeUI's gameobject image to the next one in the array

            currentHealthObject.GetComponent<Image>().sprite = LivesImages[currentLives];

            if (currentLives <= 0) {
                Debug.Log("Player has no lives left!");

            } 
        }
    }

    IEnumerator InvincibilityFrames()
    {
        float elapsed = 0f;
        float firstIFrames = invincibilityDuration / 4f; // the first quarter of I-frames is faster than the rest
        float normalDelay = invincibilityDuration / 10f; // how long a frame of delay is

        while (elapsed < invincibilityDuration)
        {
            // I frames 
            float currentDelay = (elapsed < firstIFrames) ? normalDelay / 2f : normalDelay;

            PlayerSprite.enabled = false;
            
            yield return new WaitForSeconds(currentDelay);
            
            elapsed += currentDelay;

            // Prevent overshooting the total duration if time runs out mid-blink
            if (elapsed >= invincibilityDuration) break;

            PlayerSprite.enabled = true;
            
            yield return new WaitForSeconds(currentDelay);
            
            elapsed += currentDelay;
        }

        PlayerSprite.enabled = true;
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            currentHealthObject.transform.position = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap back to the exact starting position
        currentHealthObject.transform.position = originalPos;
    }
}
