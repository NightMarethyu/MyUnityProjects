using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl1Enemy : MonoBehaviour
{
    private int score = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnvManager.Instance.setScore(score);
            Destroy(collision.gameObject);
            if (EnvManager.Instance.score >= 100)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
