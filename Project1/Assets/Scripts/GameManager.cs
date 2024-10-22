using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemyCount;
    private int score;

    public PlayerController playerController;
    public SpawnerController spawner;
    public int difficulty;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI enemyCountText;

    void Start()
    {
        if (playerController != null)
        {
            playerController.onHealthChange += UpdateHealthText;
            UpdateHealthText(playerController.playerHealth);
        }
        else
        {
            Debug.LogError("PlayerController is not assigned!");
        }
    }

    void Update()
    {
        int oldEnemyCount = enemyCount;
        enemyCount = FindObjectsOfType<EnemyMovement>().Length;
        if (enemyCount == 0)
        {
            GameOver();
        } else if (enemyCount != oldEnemyCount)
        {
            UpdateEnemyCount(enemyCount);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Start is called before the first frame update
    public void StartGame(int diff)
    {
        score = 0;
        difficulty = diff;
        FindAnyObjectByType<PlayerController>().gameObject.GetComponent<PlayerController>().ResetPlayer();
        UpdateScore(0);
        spawner.BeginSpawning(diff);
        titleScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
        enemyCountText.gameObject.SetActive(true);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        scoreText.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        enemyCountText.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
        gameOverScoreText.text = "Your Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateEnemyCount(int count)
    {
        enemyCountText.text = "Enemies Left: " + count;
    }

    void UpdateHealthText(float newHealth)
    {
        if (newHealth <= 0)
        {
            GameOver();
        }
        healthText.text = "Health: " + newHealth;
    }
}
