using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlrHealthManager : MonoBehaviour
{
    public int maxLives = 3;
    public int currentLives;

    public SpriteRenderer PlayerSprite;
    public Sprite[] LivesImages;
    public RectTransform currentHealthObject; // Changed from GameObject to RectTransform
    public ParticleSystem damageParticlesUI;

    public float invincibilityDuration = 2f; // Duration of invincibility in seconds
    public float nextHitTime; // Time when the player can be hit again

    private Vector2 originalPos; // position of the UI health object

    void Start()
    {
        nextHitTime = Time.time; 
        currentLives = maxLives;

        originalPos = currentHealthObject.anchoredPosition;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hurt") && Time.time >= nextHitTime && GameManager.Instance.GameInPlay)
        {
            Debug.Log("Player hit by projectile!");

            Destroy(collision.gameObject); // delete projectile

            // lose life, activate invincibility frames
            currentLives--;
            nextHitTime = Time.time + invincibilityDuration;

            StartCoroutine(InvincibilityFrames());
            StartCoroutine(ShakeRoutine(0.4f, 10f)); // Note: UI pixels require a larger magnitude than world units!

            damageParticlesUI.Play();

            // Prevent an IndexOutOfRangeException if currentLives drops below 0
            if (currentLives >= 0 && currentLives < LivesImages.Length)
            {
                currentHealthObject.GetComponent<Image>().sprite = LivesImages[currentLives];
            }

            if (currentLives <= 0)
            {
                Debug.Log("Player has no lives left!");
                GameManager.Instance.loseGame();
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
            float currentDelay = (elapsed < firstIFrames) ? normalDelay / 2f : normalDelay;

            PlayerSprite.enabled = false;
            yield return new WaitForSeconds(currentDelay);
            elapsed += currentDelay;

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
        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Shake UI using anchoredPosition (in pixels)
            currentHealthObject.anchoredPosition = originalPos + new Vector2(x, y);

            // Shake Camera using a smaller scale so the screen doesn't fly off
            float camX = x * 0.01f;
            float camY = y * 0.01f;
            Camera.main.transform.position = originalCamPos + new Vector3(camX, camY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap both back to their exact starting positions
        currentHealthObject.anchoredPosition = originalPos;
        Camera.main.transform.position = originalCamPos;
    }
}