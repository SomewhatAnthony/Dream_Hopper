using UnityEngine;
using System.Collections;

public class NightmareHealth : MonoBehaviour
{

    public int Health = 100;

    public ParticleSystem damageParticles;

    public ParticleSystem clouds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // have health scale by points
    }




    IEnumerator disappear()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        Color c = render.color;

        for (int i = 0; i < 100; i++)
        {
            c.a -= 0.01f;
            render.color = c;
            yield return new WaitForSeconds(0.01f); // Fades over ~1 second
        }

        // Ensure it reaches exact 0 at the end
        c.a = 0f;
        render.color = c;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerProjectile") && GameManager.Instance.GameInPlay)
        {
            Health -= 1;

            damageParticles.Play();

            Debug.Log("Nightmare hit! Current Health: " + Health);
            
            if (Health <= 0)
            {
                //Destroy(gameObject); // Destroy the Nightmare object when health reaches 0

                Debug.Log("Nightmare defeated!");

                clouds.Stop();
                StartCoroutine(disappear());

                GameManager.Instance.winGame();

                Destroy(gameObject, 2f);
                
            }

            Destroy(collider.gameObject);
        }
    }
}
