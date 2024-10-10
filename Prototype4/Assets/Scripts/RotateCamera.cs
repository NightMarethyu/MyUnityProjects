using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Ammon Turner
    public float rotationSpeed;
    public float mouseSensitivity;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * mouseSensitivity * Time.deltaTime);
    }
}
