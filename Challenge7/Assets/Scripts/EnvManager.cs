using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvManager : MonoBehaviour
{
    public static EnvManager Instance;
    private int maxHealth = 100;
    private int health;
    public int score;
    public int oldHighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void OnApplicationQuit()
    {
        SaveHighScore();
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void setHealth(int damage)
    {
        health += damage;
        if (health <= 0) { SceneManager.LoadScene(3); }
        else if (health > 100) { health = 100; }
    }

    public int getHealth()
    {
        return health;
    }

    public void setScore(int points)
    {
        score += points;
    }

    [System.Serializable]
    class HighScore
    {
        public int Score;
    }

    public void SaveHighScore()
    {
        HighScore data = new HighScore();
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore oldData = JsonUtility.FromJson<HighScore>(json);

            if (oldData.Score > score)
            {
                data.Score = oldData.Score;
            } else
            {
                data.Score = score;
            }
        } else
        {
            data.Score = score;
        }

        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);

            oldHighScore = data.Score;
        } else
        {
            oldHighScore = score;
        }
    }

    public string ScoreMessage()
    {
        LoadHighScore();
        if (oldHighScore <= score)
        {
            return $"Congratulations! New High Score! {score}";
        } else
        {
            return $"Score: {score}\nHigh Score: {oldHighScore}"; 
        }
    }

}
