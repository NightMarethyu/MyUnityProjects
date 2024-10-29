using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI userScore;

    // Update is called once per frame
    void Update()
    {
        userScore.text = "Score: " + EnvManager.Instance.score;
    }
}
