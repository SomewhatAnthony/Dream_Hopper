using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour
{
    public GameObject projectilePrefab;
    bool patternActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(pattern2());

        Debug.Log("asdfasdf");
    }

    public IEnumerator pattern1()
    {
        patternActive = true;
        float angle = 0f;
        for (int i = 0; i < 50; i++)
        {
            // Spawn projectile logic here
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone.transform.position = Vector3.zero;
            clone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            angle += 360f / 14f;

            yield return new WaitForSeconds(.12f);
        }
        patternActive = false;
    }
    public IEnumerator pattern2()
    {
        patternActive = true;
        float angle = 0f;
        for (int i = 0; i < 50; i++)
        {
            // Spawn projectile logic here
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone.transform.position = Vector3.zero;
            clone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            angle += 360f / 14f;

            GameObject clone2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone2.transform.position = Vector3.zero;
            clone2.transform.rotation = Quaternion.Euler(0f, 0f, -angle);

            yield return new WaitForSeconds(.12f);
        }
        patternActive = false;
    }


    // updates at a rate of 50 fps for fixed projectile spawning
    void FixedUpdate()
    {

    }



}
