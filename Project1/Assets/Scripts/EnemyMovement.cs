using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Transform orbitPosition;  // Center of orbit
    public float moveSpeed = 5f;
    public float attackRange = 10f;
    public float orbitSpeed = 2f;

    private Rigidbody enemyRb;
    private bool playerDetected = false;
    private float currentAngle = 0f;  // Store angle for orbit

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.drag = 1f;  // Adjust drag to prevent excessive acceleration
    }

    void FixedUpdate()
    {
        if (transform.position.y < -5)  // Destroy if it falls out of bounds
        {
            Destroy(gameObject);
        }

        if (!playerDetected && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            playerDetected = true;  // Lock on to the player
        }

        Vector3 forceDirection;

        if (playerDetected)
        {
            // Track and move towards the player
            forceDirection = (player.position - transform.position).normalized;
        }
        else
        {
            // Smooth orbit using a tangential force
            currentAngle += orbitSpeed * Time.fixedDeltaTime;  // Increment angle over time

            // Calculate the offset for circular movement
            Vector3 offset = new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle));

            // Target position along the orbit path
            Vector3 targetPosition = orbitPosition.position + offset;

            // Compute the force direction towards the target position
            Vector3 tangentDirection = new Vector3(-Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle));

            // Ensure the force points tangentially, simulating circular orbit
            forceDirection = tangentDirection.normalized;
        }

        // Apply force to the Rigidbody
        enemyRb.AddForce(forceDirection * moveSpeed, ForceMode.Acceleration);
    }
}
