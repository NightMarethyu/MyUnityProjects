using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;

    //[Header("Movement Settings")]
    private float maxVelocity = 80f;
    private float acceleration = 20f;  // New parameter to control force application rate.
    private float drag = 2f;            // Helps slow down the player when no input is given.

    public float playerHealth = 100;

    [Header("Bullet Settings")]
    public GameObject bullet;
    public GameObject bulletSpawner;
    public float weaponDamage = 10f;
    public float bulletForce = 75f;
    

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        // Setup Rigidbody properties for smooth physics behavior.
        playerRb.drag = drag;
        playerRb.angularDrag = 0.5f;  // Optional: Reduces rotational movement noise.

        // Camera setup logic.
        SetupCameraAndBulletSpawner();
    }

    private void Update()
    {
        HandleMovementInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    private void FixedUpdate()
    {
        LimitVelocity();  // Ensure player speed stays within bounds.
    }

    private void HandleMovementInput()
    {
        // Get normalized input relative to the camera's orientation.
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = (cameraForward * forwardInput) + (focalPoint.transform.right * sideInput);
        movementDirection.Normalize();

        // Gradually apply force for smoother acceleration.
        playerRb.AddForce(movementDirection * acceleration, ForceMode.Acceleration);
    }

    private void ShootBullet()
    {
        GameObject shotBullet = Instantiate(bullet, bulletSpawner.transform.position, bulletSpawner.transform.rotation);

        Rigidbody bulletRb = shotBullet.GetComponent<Rigidbody>();
        shotBullet.GetComponent<SpawnBullet>().SetDamage(weaponDamage);
        bulletRb.AddForce(bulletSpawner.transform.forward * bulletForce, ForceMode.Impulse);
    }

    private void LimitVelocity()
    {
        if (playerRb.velocity.sqrMagnitude > maxVelocity * maxVelocity)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxVelocity;
        }
    }

    private void SetupCameraAndBulletSpawner()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 cameraOffset = new Vector3(0, 4, -10);
        camera.transform.position = playerRb.transform.position + cameraOffset;
        camera.transform.LookAt(playerRb.transform.position + Vector3.up * 1);

        bulletSpawner.transform.position = playerRb.transform.position + new Vector3(0, 0, 1.2f);
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        } else if (playerHealth > 100)
        {
            playerHealth = 100;
        }
    }

    public void WeaponUpgrade(float damage)
    {
        weaponDamage = damage;
    }
}
