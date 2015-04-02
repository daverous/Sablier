using UnityEngine;
using System.Collections;

public class SkyBlade : MonoBehaviour {

	private Animator animator;
	private GameObject tail;

	// Use this for initialization
	void Start () {
		animator = this.transform.root.GetComponent<Animator> ();
		tail = GameObject.FindGameObjectWithTag("Trail");
	}
	
	// Update is called once per frame
	void Update () {
		if(animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|IdleAtSide")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|IdleOverShoulder")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|RunAtSide")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|JogAtSide")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|RunOverShoulder")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|JogOverShoulder")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|JumpAtSide")||
		   animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|JumpOverShoulder"))
		{
			tail.GetComponent<TrailRenderer>().enabled=false;
		} else {
			tail.GetComponent<TrailRenderer>().enabled=true;
		}
	
	}
}
