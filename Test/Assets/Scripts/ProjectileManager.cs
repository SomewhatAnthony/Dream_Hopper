using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour
{
    public GameObject projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(crossPattern());

        Debug.Log("asdfasdf");
    }

    public IEnumerator crossPattern()
    {
        float angle = 0f;
 
        for (int i = 0; i < 360; i++)
        {
            // Spawn projectile logic here

            for (int x = 0; x<4; x++)
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clone.transform.position = Vector3.zero;
                clone.transform.rotation = Quaternion.Euler(0f, 0f, angle+(x*90f));
                clone.GetComponent<ProjectileScript>().speed = 3f;
                clone.GetComponent<ProjectileScript>().lifeTime = 18f;
            }
           
            angle += 8f;

            yield return new WaitForSeconds(.02f);
        }
    }
    public IEnumerator pattern2()
    {
        float angle = 0f;
        for (int i = 0; i < 100; i++)
        {
            // Spawn projectile logic here
            GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone.transform.position = Vector3.zero;
            clone.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            clone.GetComponent<ProjectileScript>().speed = 3f;

            GameObject clone2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            clone2.transform.position = Vector3.zero;
            clone2.transform.rotation = Quaternion.Euler(0f, 0f, -angle-14);
            clone2.GetComponent<ProjectileScript>().speed = 2f;
            clone2.GetComponent<ProjectileScript>().lifeTime = 10f;

            angle += 360f / 14f;


            yield return new WaitForSeconds(.08f);


        }
    }


    // updates at a rate of 50 fps for fixed projectile spawning
    void FixedUpdate()
    {

    }



}
