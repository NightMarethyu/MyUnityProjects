using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI score = gameObject.GetComponent<TextMeshProUGUI>();
        score.text = EnvManager.Instance.ScoreMessage();
    }
}
