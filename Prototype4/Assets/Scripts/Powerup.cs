using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
// Ammon Turner

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
    }
}
