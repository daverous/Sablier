using UnityEngine;
using System.Collections;

public class SyncValuesData : ScriptableObject
{
		[HideInInspector]
		public float
				DeadZone = 0.15f;
		[HideInInspector]
		public float
				Gravity = 5.0f;
		[HideInInspector]
		public float
				Sensitivity = 3.0f;
}
