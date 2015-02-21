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
        character = GameObject.Find("Player").GetComponent<Character>();
    }

    void FixedUpdate()
    {
        if (planet) 
            planet.Attract(thisTransform);
    }
}
