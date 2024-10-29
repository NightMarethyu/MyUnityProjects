using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public GameObject body;
    public GameObject bullet;
    public GameObject spawnBullet;
    public float speed = 5.0f;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    
    
    // Start is called before the first frame update
    void Start()
    {

        healthBar.SetHealth(EnvManager.Instance.getHealth());

        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
        body.transform.position = transform.position + new Vector3(0, .75f, 0);
        body.transform.rotation = focalPoint.transform.rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, spawnBullet.transform.position, spawnBullet.transform.rotation);
        }
    }

    void TakeDamage(int damage)
    {
        EnvManager.Instance.setHealth(damage);
        healthBar.SetHealth(EnvManager.Instance.getHealth());
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(-20);
        }
        if (collision.gameObject.CompareTag("Good"))
        {
            TakeDamage(20);
        }

    }


}


