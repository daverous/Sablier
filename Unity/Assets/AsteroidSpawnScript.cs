using UnityEngine;
using System.Collections;

public class AsteroidSpawnScript : MonoBehaviour {

   public GameObject[] asteroids;
   public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
   public float SpawnStartDelay  = 0; 
   public float SpawnRate = 5.0f;
    
	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", SpawnStartDelay, SpawnRate);
	}

    // Spawn the SpawnObject 
    void Spawn()
    {
        if (asteroids != null && spawnPoints != null)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int AsteroidType = Random.Range(0, asteroids.Length);

            Instantiate(asteroids[AsteroidType], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
	// Update is called once per frame

}
