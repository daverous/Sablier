using UnityEngine;
using System.Collections;
using UnityEditor;
using  System.Collections.Generic;

[CustomEditor(typeof(MenuItemsCommonSetting))]
public class MenuItemsCommonInspector : Editor
{

		Transform InMenuItems;
		Transform BaseItemTrns;
		string ComparisonName;
		bool AlignInvalidBool;

		public override void OnInspectorGUI ()
		{
				EditorGUILayout.Space ();
				MenuItemsCommonSetting ItemsCommonScript = target as MenuItemsCommonSetting;
				GUI.changed = false;

				DrawDefaultInspector ();
				EditorGUILayout.Space ();

				Undo.RecordObject (ItemsCommonScript, "Inspector");
				EditorGUILayout.BeginVertical (GUI.skin.box);

				if (!GameObject.Find ("jInputMappingSet/InMapperMenuItems")) {
						AlignInvalidBool = true;
				} else {
						AlignInvalidBool = false;
						InMenuItems = GameObject.Find ("jInputMappingSet/InMapperMenuItems").transform;
						if (InMenuItems.childCount <= 1) {
								AlignInvalidBool = true;
						} else {
								if (BaseItemTrns = InMenuItems.FindChild ("MapperMenuItem00")) {
				
								} else {
										AlignInvalidBool = true;
								}
						}
				}

				EditorGUILayout.LabelField ("Vertical Align MenuItems");
				EditorGUI.indentLevel++;
				EditorGUI.BeginDisabledGroup (AlignInvalidBool);
				ItemsCommonScript.AlignInterval = EditorGUILayout.Slider ("Interval (0.1-5.0)", ItemsCommonScript.AlignInterval, 0.1f, 5.0f);
				if (ItemsCommonScript.AlignInterval < 0.1f)
						ItemsCommonScript.AlignInterval = 0.1f;
				else if (ItemsCommonScript.AlignInterval > 5.0f)
						ItemsCommonScript.AlignInterval = 5.0f;
				EditorGUI.EndDisabledGroup ();
				EditorGUI.indentLevel--;
				EditorGUILayout.BeginHorizontal ();
				GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (true));
				EditorGUI.BeginDisabledGroup (AlignInvalidBool);
				if (GUILayout.Button ("Align", GUILayout.Width (80))) {
						if (ItemsCommonScript.AlignInterval < 0.1f)
								ItemsCommonScript.AlignInterval = 0.1f;
						else if (ItemsCommonScript.AlignInterval > 5.0f)
								ItemsCommonScript.AlignInterval = 5.0f;
						GUI.FocusControl (""); //Inspectorのフォーカスを解除して入力欄を更新
						//itemを整列させる
						if (InMenuItems != null) {
								int ItemListIndex = -1;
								List<string> MenuItemsList = new List<string> ();
								for (int i = 0; i < InMenuItems.childCount; i++) {
										MenuItemsList.Add (InMenuItems.transform.GetChild (i).name);
								}
								for (int i = 0; i <= 30; i++) {
										if (0 <= i && i <= 9) {
												ComparisonName = "MapperMenuItem0" + i;
										} else if (10 <= i && i <= 30) {
												ComparisonName = "MapperMenuItem" + i;
										}
										if (ComparisonName != null) {
												ItemListIndex = MenuItemsList.IndexOf (ComparisonName);
										}
										if (ItemListIndex == -1) {

										} else {
												Transform TemporaryItemTrns = InMenuItems.FindChild (ComparisonName);
												Undo.RecordObject (TemporaryItemTrns, "Inspectoree");
												TemporaryItemTrns.position = new Vector3 (TemporaryItemTrns.position.x, BaseItemTrns.position.y - (ItemsCommonScript.AlignInterval * i), TemporaryItemTrns.position.z);
										}
								}
						}
				}
				EditorGUI.EndDisabledGroup ();
				GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Width (10));
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
				EditorGUILayout.EndVertical ();
				EditorGUILayout.Space ();
				if (GUI.changed)
						EditorUtility.SetDirty (target);
		}
}
