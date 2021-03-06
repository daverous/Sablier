﻿using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;
	// Use this for initialization


    public void Attract(Transform body)
    {
        Vector3 target = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, target) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(target * gravity);
    }

    public void AttractObject(Transform body) {
        Vector3 target = (body.position - transform.position).normalized;
        body.GetComponent<Rigidbody>().AddForce(target * gravity);
    
    }

    //TODO make new gravity script
}
