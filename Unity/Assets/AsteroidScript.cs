using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
    public float asteroidDamage = 15f;
	// Use this for initialization
	void Start () {
	
	}
    void OnCollisionEnter(Collision other)
    {
        string tag = other.transform.root.tag;
        // If asteroid has hit player
        if (tag == "Player" || tag == "Player2")
        {
            Character player = GameObject.FindGameObjectWithTag(tag).GetComponent<Character>();
            player.beenHit(asteroidDamage);
        }
        if (tag == "Planet")
        {
            gameObject.GetComponent<Detonator>().Explode(); 
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
