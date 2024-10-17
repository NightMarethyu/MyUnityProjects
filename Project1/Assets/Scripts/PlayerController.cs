using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float maxVelocity = 50;
    private float sqrMaxVelocity = 2500;

    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 cameraOffset = new Vector3(0, 5, -17);
        camera.transform.position = playerRb.transform.position + cameraOffset;

        camera.transform.LookAt(playerRb.transform.position + Vector3.up * 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        float forwardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = (cameraForward * forwardInput) + (focalPoint.transform.right * sideInput);
        //if (playerRb.velocity > )
        playerRb.AddForce(movementDirection * speed);
        
    }

    private void FixedUpdate()
    {
        var velocity = playerRb.velocity;
        if (velocity.sqrMagnitude > sqrMaxVelocity)
        {
            playerRb.velocity = velocity.normalized * maxVelocity;
        }
    }
}
