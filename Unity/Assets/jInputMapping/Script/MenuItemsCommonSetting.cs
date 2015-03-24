using UnityEngine;
using System.Collections;

public class MenuItemsCommonSetting : MonoBehaviour
{
		[Space(7)]
		public GameObject
				MenuItemPrefab;
		[Space(1)]
		public GameObject
				AlertMarkPrefab;
		[Space(3)]
		public Vector2
				HeadingRelativePosi;
		#if (UNITY_EDITOR)
		[HideInInspector]
		public float
				AlignInterval;
		#endif

		void Start ()
		{

		}

		void Update ()
		{
	
		}
	
}
