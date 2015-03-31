using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour
{

		
		public int MoveSpeed = 4;
		public int damage = 10;
		float distanceFromP1;
		float distanceFromP2;
		Transform actual;


		void Start ()
		{
				GameObject p1 = GameObject.FindGameObjectWithTag ("Player");
				GameObject p2 = GameObject.FindGameObjectWithTag ("Player2");
				distanceFromP1 = Vector3.Distance (transform.position, p1.transform.position);
				distanceFromP2 = Vector3.Distance (transform.position, p2.transform.position);
				if (distanceFromP1 > distanceFromP2) {
						actual = p1.transform;
				} else {
						actual = p2.transform;
				}
		}
		void Update ()
		{
				
				transform.LookAt (actual);
				Debug.Log ("forward");
				transform.position += transform.forward * MoveSpeed * Time.deltaTime;				
				
		}

		void OnCollisionEnter (Collision other)
		{
				string tag = other.collider.transform.root.tag;
				Debug.Log (tag);
				if ((tag == "Player" || tag == "Player2") && other.collider.tag != "Weapon"){
						GameObject.FindGameObjectWithTag (tag).GetComponent<Character> ().beenHit (damage);
						Destroy (gameObject);
				}

		}
				
		
		
}
