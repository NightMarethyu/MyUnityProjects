using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    public List<Vector3> enemySpawnLocations;
    public GameObject enemyPrefab;
    public int numberOfEnemies;

    // TODO: add health packs maybe weapon upgrades/powerups

    public void BeginSpawning(int difficulty)
    {
        numberOfEnemies = difficulty;
        List<Vector3> usedEnemyLocations = new();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 location = GetRandomEnemySpawnLocation(usedEnemyLocations);
            usedEnemyLocations.Add(location);
            Instantiate(enemyPrefab, location, Quaternion.identity);
        }
    }

    private Vector3 GetRandomEnemySpawnLocation(List<Vector3> used)
    {
        if (used.Count >= enemySpawnLocations.Count)
        {
            Debug.LogWarning("All spawn locations are used.");
            return enemySpawnLocations[0]; // Fallback to a default location.
        }

        Vector3 location = Vector3.zero;
        int attempts = 0;
        do
        {
            int randIndex = Random.Range(0, enemySpawnLocations.Count);
            location = enemySpawnLocations[randIndex];
            attempts++;
        } while (used.Contains(location) && attempts < 10);

        if (used.Contains(location))
        {
            Debug.LogWarning("Could not find an unused location after 10 attempts.");
        }

        return location;
    }

}
