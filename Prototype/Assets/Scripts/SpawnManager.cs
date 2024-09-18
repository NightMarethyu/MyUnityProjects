using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animals = new GameObject[3];
    private float xSpawnRange = 20;
    private float zSpawn = 20;
    //public int animalIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 spawn = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 0, zSpawn);
            int animalIndex = Random.Range(0, animals.Length);
            Instantiate(animals[animalIndex], spawn, animals[animalIndex].transform.rotation);
        }
    }
}
