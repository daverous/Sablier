using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

public class TempStateToConfDesign : MonoBehaviour
{
	[HideInInspector]
	public int
		TempState;
	[HideInInspector]
	public bool
		SameKeyCheck;
	void Start ()
	{
	
	}

	void Update ()
	{
	
	}
}

	 #if (UNITY_EDITOR)
[CustomEditor(typeof(TempStateToConfDesign))]
public class TempStateToConfDesignInspector : Editor
{
	readonly string[] StateTopic = {"Normal", "Serected", "Wait Input"};
	readonly int[] StateTopicOption = {0, 1, 2};
	bool InspectorDisabled;

	public override void OnInspectorGUI ()
	{
		EditorGUILayout.Space ();
		TempStateToConfDesign TempStateScript = target as TempStateToConfDesign;
		if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused) {
			InspectorDisabled = false;
		} else {
			InspectorDisabled = true;
		}

		DrawDefaultInspector ();
		EditorGUILayout.Space ();
		EditorGUI.BeginDisabledGroup (InspectorDisabled);
		TempStateScript.TempState = EditorGUILayout.IntPopup ("TempState to Confirm Design", TempStateScript.TempState, StateTopic, StateTopicOption);
		TempStateScript.SameKeyCheck = EditorGUILayout.Toggle ("TempState SameKey Mapped", TempStateScript.SameKeyCheck);
		EditorGUI.EndDisabledGroup ();
		EditorGUILayout.Space ();
		EditorUtility.SetDirty (TempStateScript);
	}
}
	 #endif

	