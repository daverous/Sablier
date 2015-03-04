using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {
	private Animator animator;
	private Character character;
	// Use this for initialization
	void Start () {
		character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
		if(character.moveDirection != new Vector3(0,0,0)){
			animator.SetBool("Running", true);
			
		}

	}
}
