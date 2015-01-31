using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {

    public float gravity = -10f;
	// Use this for initialization


    public void Attract(Transform body)
    {
        Vector3 target = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, target) * body.rotation;
        body.rigidbody.AddForce(target * gravity);
    }

}
