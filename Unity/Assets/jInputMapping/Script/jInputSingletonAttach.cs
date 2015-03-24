using UnityEngine;
using System.Collections;

public class jInputSingletonAttach : jInputSingleton<jInputSingletonAttach>
{
		public void Awake ()
		{
				if (this != Instance) {
						Destroy (this.gameObject);
						return;
				}
			
				DontDestroyOnLoad (this.gameObject);
		}
		void OnLevelWasLoaded ()
		{
				if (transform.parent != null) {
						transform.parent = null;
				}
		}
}
