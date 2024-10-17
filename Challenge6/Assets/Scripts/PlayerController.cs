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
    
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Power Up")
        {
            Destroy(collision.gameObject);
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach(Enemy enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

}


