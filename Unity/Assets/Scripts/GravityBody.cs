using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {


    GravityAttractor planet;
    private Transform thisTransform;
    Character character;
	// Use this for initialization
    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        thisTransform = transform;
        if (gameObject.tag == "Player")
            character = GameObject.Find("Player").GetComponent<Character>();
        else if (gameObject.tag == "Player2")
            character = GameObject.Find("Player2").GetComponent<Character>();
        
    }

    void FixedUpdate()
    {
        if (planet) 
            planet.Attract(thisTransform);
    }
}
