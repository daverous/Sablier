using UnityEngine;
using System.Collections;


public class PowerUpScript : MonoBehaviour
{

		private float powerUpAmount = 0.3f;
		public float rotateSpeed = 10f;
		// Use this for initialization
		void Start ()
		{
	
		}

		public float getPowerUpAmount ()
		{
				return powerUpAmount;
		}
	
		// Update is called once per frame
		void Update ()
		{
				transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime);
		}
}
