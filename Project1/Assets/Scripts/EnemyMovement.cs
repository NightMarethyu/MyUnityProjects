using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Game Objects")]
    public Transform player;
    public GameObject bulletPrefab; // Reference to bullet prefab
    public Transform bulletSpawnPoint; // Location where bullets spawn

    [Header("Enemy Settings")]
    public float moveSpeed = 5f;
    public float attackRange = 10f;
    public float fireRate = 2f; // Time between each shot
    public float weaponPower = 10f;
    public GameObject deathParticle;
    public AudioClip deathSound;

    private Rigidbody enemyRb;
    private bool playerDetected = false;
    private float nextFireTime; // Timer for next bullet fire
    private float health;
    private int pointValue;
    private GameManager gameManager;
    private AudioSource enemyAudio;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player object not found in the scene!");
            }
        }

        enemyRb = GetComponent<Rigidbody>();
        enemyAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SetDifficulty(gameManager.difficulty);
    }


    void FixedUpdate()
    {
        DetectAndChasePlayer();

        if (playerDetected && Time.time >= nextFireTime) { 
            AlignBulletSpawnPoint();

            ShootAtPlayer();
            nextFireTime = Time.time + fireRate; // Schedule the next shot
        }
    }

    private void DetectAndChasePlayer()
    {
        if (!playerDetected && Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            playerDetected = true;
        }

        if (playerDetected)
        {
            Vector3 forceDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(forceDirection * moveSpeed);
        }
    }

    private void AlignBulletSpawnPoint()
    {
        Vector3 directionToPlayer = player.transform.position - bulletSpawnPoint.position;
        directionToPlayer.y = 0;

        if (directionToPlayer != Vector3.zero)
        {
            bulletSpawnPoint.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }

    private void ShootAtPlayer()
    {
        Vector3 direction = (player.transform.position - bulletSpawnPoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Assign the direction to the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bullet.GetComponent<SpawnBullet>().SetDamage(weaponPower);
        bulletRb.AddForce(direction * 50f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(collision.gameObject.GetComponent<SpawnBullet>().damage);
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);
            enemyAudio.PlayOneShot(deathSound, 1.0f);
            gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
        }
    }

    private void SetDifficulty(int difficulty)
    {
        if (difficulty == 5)
        {
            health = 20;
            pointValue = 20;
            attackRange = 10;
            fireRate = 2;
            weaponPower = 10;
        }
        else if (difficulty == 8)
        {
            health = 30;
            pointValue = 30;
            attackRange = 25;
            fireRate = 1.5f;
            weaponPower = 15;
        }
        else if (difficulty == 10)
        {
            health = 40;
            pointValue = 50;
            playerDetected = true;
            fireRate = 1;
            weaponPower = 20;
        }
    }
}
