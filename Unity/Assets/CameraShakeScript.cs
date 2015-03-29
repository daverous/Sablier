using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CameraShakeScript : MonoBehaviour
{
		// Transform of the camera to shake. Grabs the gameObject's transform
		// if null.
		Transform camTransform;
	
		PlayerIndex playerIndex = (PlayerIndex)0;
		PlayerIndex player2Index = (PlayerIndex)1;
		// How long the object should shake for.
		public float shake = 2f;

		// Amplitude of the shake. A larger value shakes the camera harder.
		public float shakeAmount = 0.7f;
		public float decreaseFactor = 1.0f;
		public float vibrateFeelL = 0;
		public float vibrateFeelR = 0;

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
                    vibrateFeelR += 0.1f;
                    vibrateFeelL += 0.1f;
						if (transform.root.tag == "Player") {

								GamePad.SetVibration (playerIndex, vibrateFeelL, vibrateFeelR);
						}
						if (transform.root.tag == "Player2") {
								GamePad.SetVibration (player2Index, vibrateFeelL, vibrateFeelR);
						}
						this.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

						init -= Time.deltaTime * decreaseFactor;
				} else if (canShake) {
                    if (transform.root.tag == "Player")
                    {
                        GamePad.SetVibration(playerIndex, 0, 0);
                    }
                    if (transform.root.tag == "Player2")
                    {
                        GamePad.SetVibration(player2Index, 0, 0);
                    }
						init = shake;
                        vibrateFeelL = 0f;
                        vibrateFeelR = 0f;
						camTransform.localPosition = originalPos;
						canShake = false;
				} else {
						init = shake;
				}
		}
}