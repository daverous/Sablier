using UnityEngine;
using System.Collections;

public class AsteroidSpawnScript : MonoBehaviour {

   public GameObject asteroid;
   public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    float SpawnStartDelay  = 0; 
   float SpawnRate = 5.0f;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", SpawnStartDelay, SpawnRate);
	}

    // Spawn the SpawnObject 
    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(asteroid, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
	// Update is called once per frame

}
