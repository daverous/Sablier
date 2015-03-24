using UnityEngine;
using System.Collections;

public class ActiveFalseSelf : MonoBehaviour
{
		void OnEnable ()
		{
				Invoke ("AutoFalseSelf", 1.1f);
		}

		void AutoFalseSelf ()
		{
				this.gameObject.SetActive (false);
		}
}
