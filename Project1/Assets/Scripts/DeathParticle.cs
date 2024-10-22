using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip deathSound;
    public ParticleSystem deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(deathSound, 1.0f);
        deathParticles.Play();
        Destroy(gameObject, 3f);
    }
}
