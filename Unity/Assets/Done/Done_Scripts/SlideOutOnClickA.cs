using UnityEngine;
using System.Collections;

public class SlideOutOnClickA : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) { 
			animator.SetBool("aIsPressed", true);
				}
	//	anim.SetTrigger("StartOnClick");
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("pressaslideoutstay")){
			Application.LoadLevel ("TestScene");
			}
	}
}
