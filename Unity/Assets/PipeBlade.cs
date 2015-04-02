using UnityEngine;
using System.Collections;

public class PipeBlade : MonoBehaviour {
	private Animator animator;
	private Animator anim;
	private GameObject[] fires;
	private GameObject scorch;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		anim = this.transform.root.GetComponent<Animator>();
		scorch = GameObject.FindGameObjectWithTag("Scorch");
		scorch.SetActive(false);
		fires = GameObject.FindGameObjectsWithTag("FirePart");
		foreach (GameObject fire in fires) {
			fire.SetActive(false);
		}


	}
	
	// Update is called once per frame
	void Update () {


		if(anim.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|QuickForward") ||
		   anim.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|QuickBack") ||
		   anim.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|DashForward") ||
		   anim.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|DashBack") ||
		   anim.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|Heavy")){
			foreach (GameObject fire in fires) {
				fire.SetActive(true);
			}

		}else{
			animator.SetBool("Torch", false);
			foreach (GameObject fire in fires) {
				fire.SetActive(false);
			}
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|AerialHeavyEndF"))
		{

			scorch.SetActive(true);
			
		}else{
			scorch.SetActive(false);
			}
	}
	void OnCollisionEnter (Collision other){
		Debug.Log ("GAS IT!");
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy")){
			Debug.Log ("GAS IT");
			anim.SetBool("Aerial", true);
		}
	}

}
