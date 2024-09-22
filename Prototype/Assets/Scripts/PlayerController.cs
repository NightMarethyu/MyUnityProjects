using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Ammon Turner - Player Controller

    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 18.0f;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Shoot a projectile
            Instantiate(projectilePrefab, transform.position + new Vector3(0, 3.5f, 0), projectilePrefab.transform.rotation);
        }

        // Keep the player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Move the player based on user input
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }
}
