using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour
{
    private AudioSource source;
    public AudioClip ExplodeSound;
		public float asteroidDamage = 15f;
		// Use this for initialization
		void Start ()
		{
	
		}

        void Awake()
        {
            source = GetComponent<AudioSource>();

        }
		void OnCollisionEnter (Collision other)
		{
				string tag = other.transform.root.tag;
				// If asteroid has hit player
				if (tag == "Player" || tag == "Player2") {
						Character player = GameObject.FindGameObjectWithTag (tag).GetComponent<Character> ();
						player.beenHit (asteroidDamage);
				}
				if (tag == "Planet" || tag == "PowerUp" || tag == "HealthUp") {
                    source.PlayOneShot(ExplodeSound);
						gameObject.GetComponent<Detonator> ().Explode (); 
				}
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
}
