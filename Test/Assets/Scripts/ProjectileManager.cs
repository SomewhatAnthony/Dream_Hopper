using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ProjectileManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject nightmare;
    bool patternActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(crossPattern(nightmare.transform));

        Debug.Log("asdfasdf");
    }

    public IEnumerator crazyPattern(Transform origin)
    {
        patternActive = true;
        float angle = 0f;

        for (int i = 0; i < 150; i++)
        {
            // Spawn projectile logic here

            for (int x = 0; x<5; x++)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clone.transform.position = origin.position;
                clone.transform.rotation = Quaternion.Euler(0f, 0f, angle+(x*73f));
            }
           
            angle += 20f;

            yield return new WaitForSeconds(.1f);
        }
        patternActive = false;
    }

    public IEnumerator crossPattern(Transform origin)
    {
        patternActive = true;
        float angle = 0f;

        for (int i = 0; i < 150; i++)
        {
            // Spawn projectile logic here

            for (int x = 0; x < 5; x++)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clone.transform.position = origin.position;
                clone.transform.rotation = Quaternion.Euler(0f, 0f, angle + (x * 73f));
            }

            angle += 2f;

            yield return new WaitForSeconds(.05f);
        }
    }

    public IEnumerator burstPattern(Transform origin)
    {
        float angle = 0f;

        for (int i = 0; i < 10; i++)
        {
            // Spawn projectile logic here

            for (int x = 0; x < 36; x++)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clone.transform.position = origin.position;
                clone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            angle += 2f;

            yield return new WaitForSeconds(.2f);
        }
    }

    public IEnumerator circlePattern(Transform origin)
    {
        float angle = 0f;
        for (int i = 0; i < 100; i++)
        {
            // Spawn projectile logic here
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone.transform.position = origin.position;
            clone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            clone.GetComponent<ProjectileScript>().speed = 3f;

            GameObject clone2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone2.transform.position = origin.position;
            clone2.transform.rotation = Quaternion.Euler(0f, 0f, -angle-14);
            clone2.GetComponent<ProjectileScript>().speed = 2f;
            clone2.GetComponent<ProjectileScript>().lifeTime = 10f;

            angle += 360f / 14f;


            yield return new WaitForSeconds(.08f);


        }
        patternActive = false;
    }


    // updates at a rate of 50 fps for fixed projectile spawning
    void FixedUpdate()
    {

    }



}
