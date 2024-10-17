using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject powerUp;
    private float spawnRange = 45f;
    private int enemyCount;
    public int waveCount = 1;
    public int powerUpAvailable;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveCount);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveCount++;
            SpawnEnemyWave(waveCount);
            if (waveCount > powerUpAvailable)
            {
                Instantiate(powerUp, GenerateSpawnPosition(0.75f), powerUp.transform.rotation);
            }
        }

    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemy, GenerateSpawnPosition(0.5f), enemy.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition(float yPos)
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, yPos, spawnPosZ);
    }
}
