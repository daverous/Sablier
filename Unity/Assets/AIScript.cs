using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour
{

		public Transform[] Players;
		
		public int MoveSpeed = 4;
		public int MaxDist = 10;
		public int MinDist = 0;
	
		float distanceFromP1;
		float distanceFromP2;
		Transform actual;
	
		void Start ()
		{
				distanceFromP1 = Vector3.Distance (transform.position, Players [0].position);
				distanceFromP2 = Vector3.Distance (transform.position, Players [1].position);
				if (distanceFromP1 > distanceFromP2) {
						actual = Players [1];
				} else {
						actual = Players [2];
				}
		}
		void Update ()
		{
				
				transform.LookAt (actual);
				if (Vector3.Distance (transform.position, actual.position) >= MinDist) {
				
						transform.position += transform.forward * MoveSpeed * Time.deltaTime;
				}
				
				
				if (Vector3.Distance (transform.position, actual.position) <= 1) {
						Character ch = actual.gameObject.GetComponent<Character> ();
						//TODO add adim here
						ch.beenHit (10);
						//TODO detonate
				} 
				

						
				
		}
				
		
		
}
