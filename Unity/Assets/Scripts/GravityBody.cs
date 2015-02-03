using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {


    GravityAttractor planet;
    private Transform thisTransform;
	// Use this for initialization
    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        thisTransform = transform;
    }

    void FixedUpdate()
    {
        if (planet) 
            planet.Attract(thisTransform);
    }
}
