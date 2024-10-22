using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public float damage = 10f; // Damage dealt by the bullet
    public AudioClip fire;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(fire, 1.0f);
        // Bullet will self-destruct after 5 seconds if it doesn't collide.
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Reduce player's health (assuming PlayerController has a TakeDamage function).
            collision.gameObject.GetComponent<PlayerController>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float val)
    {
        damage = val;
    }
}
