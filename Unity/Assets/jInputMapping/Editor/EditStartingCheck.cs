using UnityEngine;
using System.Collections;
using UnityEditor;

[InitializeOnLoad]
public class EditStartingChecker : AssetPostprocessor
{
	static public bool StartingOnceCheck;
	static jInputSettings SetScript;
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
	{
		if (importedAssets.Length > 0) {
			for (int i = 0; i < importedAssets.Length; i++) {
				if (importedAssets [i] == "ProjectSettings/InputManager.asset"
					|| importedAssets [i] == "ProjectSettings/InputManager(original).asset"
					|| importedAssets [i] == "ProjectSettings/InputManager(original)Copy.asset") {
					SyncValuesSOData ();
				}
			}
		}
	}
	
	static EditStartingChecker ()
	{
		EditorApplication.hierarchyWindowChanged += MyhierarchyChanged;
		EditorApplication.playmodeStateChanged += MyPlaymodeChanged;
	}

	static void MyPlaymodeChanged ()//Play開始停止時にMyhierarchyChanged()が反応しないようにしている
	{
		//以下を付ければPlayからエディット状態に戻る時のみMyhierarchyChanged()からSyncValuesSOData()が行われる
		//if (EditorApplication.isPlaying && EditorApplication.isPaused && EditorApplication.isPlayingOrWillChangePlaymode)
		StartingOnceCheck = true;
	}

	static void MyhierarchyChanged ()
	{
		//isPlayingOrWillChangePlaymodeによりPlayからエディット状態に戻る時のみ
		if (!EditorApplication.isPlaying && !EditorApplication.isPaused && !EditorApplication.isPlayingOrWillChangePlaymode) {
			if (GameObject.Find ("jInputMappingSet") != true) {
				StartingOnceCheck = false;
				return;
			} else {
				if (StartingOnceCheck != true) {
					if (GameObject.Find ("jInputMappingSet").GetComponent<jInputSettings> ()) {
						SyncValuesSOData ();
					}
				}
			}
		}
	}
	
	static void SyncValuesSOData ()
	{
		if (GameObject.Find ("jInputMappingSet")) {
			if (SetScript = GameObject.Find ("jInputMappingSet").GetComponent<jInputSettings> ()) {
				SetScript.DefaultArrayCopyReset ();
				SetScript.jInputSOData.DefaKeyInconsistencyListText = "";
				SetScript.PlayerNumToSOData ();
				SetScript.ArrayCopyToSOData ();
				SetScript.ArrayCopyToSOData2 ();
				SetScript.ArrayCopyToSOData3 ();
				SetScript.ArrayCopyToSOData4 ();
				SetScript.DropdownToList ();
				SetScript.AxesAdvanceToSOData ();
				SetScript.jInputSOData.AxesSetApply ();
				StartingOnceCheck = true;
			}
		}
	}
	
}
