using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private float boost = 5;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public ParticleSystem boostParticles;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        var emission = boostParticles.emission;
        emission.enabled = true;  // Enable emission

    }

    void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Add force to player in direction of the focal point (and camera)
        Vector3 playerBottom = new Vector3(0, -0.6f, 0);
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = (cameraForward * verticalInput).normalized;

        if (Input.GetKey(KeyCode.Space))
        {
            boostParticles.Play();
            playerRb.AddForce(movementDirection * speed * Time.deltaTime * boost);
        } else
        {
            boostParticles.Stop();
            playerRb.AddForce(movementDirection * speed * Time.deltaTime);
        }
        

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + playerBottom;
        boostParticles.transform.position = transform.position;

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        } else if (other.gameObject.CompareTag("Ground"))
        {
            boostParticles.Play();
        }
    }



}
