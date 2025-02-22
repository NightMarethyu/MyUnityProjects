using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Ammon Turner - Spawn Manager

    public GameObject[] animals = new GameObject[3];
    private float xSpawnRange = 20;
    private float zSpawn = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    //public int animalIndex;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void SpawnRandomAnimal()
    {
        Vector3 spawn = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawn);
        int animalIndex = Random.Range(0, animals.Length);
        Instantiate(animals[animalIndex], spawn, animals[animalIndex].transform.rotation);
    }
}
