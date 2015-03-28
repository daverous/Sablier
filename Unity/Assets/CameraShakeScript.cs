using UnityEngine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour
{
		// Transform of the camera to shake. Grabs the gameObject's transform
		// if null.
		Transform camTransform;

		// How long the object should shake for.
		public float shake = 2f;

		// Amplitude of the shake. A larger value shakes the camera harder.
		public float shakeAmount = 0.7f;
		public float decreaseFactor = 1.0f;

		Vector3 originalPos;
		bool canShake;
		float init;
		void Start ()
		{
				camTransform = this.transform;
				canShake = false;
				init = shake;
				originalPos = camTransform.localPosition;
		}

		public void startShake ()
		{
				canShake = true;
		}
		public void stopShake ()
		{
				canShake = false;
		}

		void Update ()
		{
				if (init > 0 && canShake) {
    
						this.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

						init -= Time.deltaTime * decreaseFactor;
				} else if (canShake) {
						init = shake;
						camTransform.localPosition = originalPos;
						canShake = false;
				} else {
						init = shake;
				}
		}
}