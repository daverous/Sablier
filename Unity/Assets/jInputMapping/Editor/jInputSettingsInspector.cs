using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(jInputSettings))]
public class jInputSettingsInspector : Editor
{ 
/*
	public override void OnInspectorGUI() {
		serializedObject.Update();
		SerializedProperty DIA = serializedObject.FindProperty ("DefaultInputArray");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(DIA, true); //通常のInspectorの形と同じに描画
		if (EditorGUI.EndChangeCheck ()) {
				serializedObject.ApplyModifiedProperties ();
		}
		EditorGUI.EndChangeCheck();
		// ...
	}
*/
		
		//Foldoutを標準のInspectorと同じ様に描画、public static にすればどのスクリプトからでも呼んで使える
		bool Foldout (bool foldout, GUIContent content, bool toggleOnLabelClick, GUIStyle style)
		{
				Rect position = GUILayoutUtility.GetRect (40f, 40f, 16f, 16f, style);
				// EditorGUI.kNumberW == 40f but is internal
				return EditorGUI.Foldout (position, foldout, content, toggleOnLabelClick, style);
		}
		bool Foldout (bool foldout, string content, bool toggleOnLabelClick, GUIStyle style)
		{
				return Foldout (foldout, new GUIContent (content), toggleOnLabelClick, style);
		}


		GUIStyle jInputGUIStyle = new GUIStyle ();
		bool InspectorDisabled;
		bool DefaKeyButtonBoolCheck;
		GameObject PlayerNumWindowObject;
		GameObject PlayerNumTextObject;
		MenuItemsCommonSetting MenuItemsCommonScript;
		PlayerNumWindow PlayerNumWindowScript;
		string DefaKeyInconsistencyErrorText = "Default key name is inconsistency!\nThere is a need to Fix and Play again in order to apply.\n\n";
		readonly string[] DisplayedPlayerNum = {"1Player", "2Players", "3Players", "4Players"};
		readonly int[] OptionPlayerNum = {1, 2, 3, 4};
		readonly string[] DisplayedOpenCloseMethod = {"SetActive()", "LoadLevel()", "LoadLevelAdditive()"};
		readonly int[] OptionOpenCloseMethod = {0, 1, 2};

		public override void OnInspectorGUI ()
		{
				EditorGUILayout.Space ();
				jInputSettings SetScript = target as jInputSettings;
				GUI.changed = false;

				jInputGUIStyle.padding.top = 4;
				jInputGUIStyle.padding.bottom = 4;
				jInputGUIStyle.padding.left = 10;
				jInputGUIStyle.padding.right = 2;
				jInputGUIStyle.normal.textColor = EditorStyles.label.normal.textColor;//背景Dark/Lightでの文字色
				//jInputGUIStyle.fontSize = 12;//fontを大きくする場合


				/*
						SerializedProperty MIH = serializedObject.FindProperty ("MenuItemHeadings");
						SerializedProperty DIA = serializedObject.FindProperty ("DefaultInputArray");

						EditorGUI.BeginChangeCheck();
						EditorGUILayout.PropertyField(MIH, true);
						if (GUI.changed) {
								Debug.Log ("ch1");
						}
						EditorGUI.EndChangeCheck ();

						EditorGUI.BeginChangeCheck();
						EditorGUILayout.PropertyField(DIA, true);
						if (GUI.changed) {
							Debug.Log ("ch2");
						}
						EditorGUI.EndChangeCheck ();
						*/

				if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused) {
						InspectorDisabled = false;
				} else {
						InspectorDisabled = true;
				}

				EditorGUI.BeginDisabledGroup (InspectorDisabled);
				EditorGUI.BeginChangeCheck ();
				DrawDefaultInspector ();
				if (GUI.changed) {
						//SetScript.HeadingGiveNumber ();
				}
				EditorGUI.EndChangeCheck ();
				EditorGUI.EndDisabledGroup ();

				Undo.RecordObject (SetScript, "Inspector");
				if (Event.current.type == EventType.ValidateCommand) {
						if (Event.current.commandName == "UndoRedoPerformed") {
								SetScript.PlayerNumToSOData ();
								SetScript.ArrayCopyToSOData ();
								SetScript.ArrayCopyToSOData2 ();
								SetScript.ArrayCopyToSOData3 ();
								SetScript.ArrayCopyToSOData4 ();
								SetScript.DropdownToList ();
								SetScript.jInputSOData.AxesSetApply ();
								if (PlayerNumWindowObject == null)
										PlayerNumWindowObject = SetScript.transform.Find ("PlayerNumWindow").gameObject;
								if (PlayerNumTextObject == null)
										PlayerNumTextObject = SetScript.transform.Find ("PlayerSelectNumText").gameObject;
								if (PlayerNumWindowScript == null && PlayerNumWindowObject != null)
										PlayerNumWindowScript = PlayerNumWindowObject.GetComponent<PlayerNumWindow> ();
								if (PlayerNumWindowScript != null)
										PlayerNumWindowScript.PlayerMappingNumSet ();
								if (SetScript.PlayerNum != 1) {
										if (PlayerNumWindowObject != null) {
												PlayerNumWindowObject.hideFlags = HideFlags.None;
										}
										if (PlayerNumTextObject != null) {
												PlayerNumTextObject.hideFlags = HideFlags.None;
										}
								} else { //SetScript.PlayerNum==1
										SetScript.MaxPlayer1Check = true;
										if (PlayerNumWindowObject != null) {
												PlayerNumWindowObject.hideFlags = HideFlags.HideInHierarchy;
										}
										if (PlayerNumTextObject != null) {
												PlayerNumTextObject.hideFlags = HideFlags.HideInHierarchy;
										}
								}

								return;
						}
				}

				//EditorGUILayout.Separator ();
				EditorGUILayout.Space ();

				EditorGUI.BeginDisabledGroup (InspectorDisabled);
				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUILayout.LabelField ("Max Players in Same Place");
				EditorGUI.BeginChangeCheck ();
				SetScript.PlayerNum = EditorGUILayout.IntPopup (" ", SetScript.PlayerNum, DisplayedPlayerNum, OptionPlayerNum);
				if (GUI.changed) {
						SetScript.PlayerNumToSOData ();
						SetScript.ArrayCopyToSOData2 ();
						SetScript.ArrayCopyToSOData3 ();
						SetScript.ArrayCopyToSOData4 ();
						if (PlayerNumWindowObject == null)
								PlayerNumWindowObject = SetScript.transform.Find ("PlayerNumWindow").gameObject;
						if (PlayerNumTextObject == null)
								PlayerNumTextObject = SetScript.transform.Find ("PlayerSelectNumText").gameObject;
						if (PlayerNumWindowScript == null && PlayerNumWindowObject != null)
								PlayerNumWindowScript = PlayerNumWindowObject.GetComponent<PlayerNumWindow> ();
						if (PlayerNumWindowScript != null)
								PlayerNumWindowScript.PlayerMappingNumSet ();
						if (SetScript.PlayerNum != 1) {
								if (PlayerNumWindowObject != null) {
										PlayerNumWindowObject.hideFlags = HideFlags.None;
										if (SetScript.MaxPlayer1Check) {
												if (SetScript.NumWindowPreActive) {
														PlayerNumWindowObject.SetActive (true);
												} else {
														PlayerNumWindowObject.SetActive (false);	
												}
										}
								}
								if (PlayerNumTextObject != null) {
										PlayerNumTextObject.hideFlags = HideFlags.None;
										if (SetScript.MaxPlayer1Check) {
												if (SetScript.NumTextPreActive) {
														PlayerNumTextObject.SetActive (true);
												} else {
														PlayerNumTextObject.SetActive (false);
												}
										}
								}
								SetScript.MaxPlayer1Check = false;
						} else { //SetScript.PlayerNum==1
								SetScript.MaxPlayer1Check = true;
								if (PlayerNumWindowObject != null) {
										PlayerNumWindowObject.hideFlags = HideFlags.HideInHierarchy;
										if (PlayerNumWindowObject.activeSelf) {
												SetScript.NumWindowPreActive = true;
												PlayerNumWindowObject.SetActive (false);
										} else {
												SetScript.NumWindowPreActive = false;
										}
								}
								if (PlayerNumTextObject != null) {
										PlayerNumTextObject.hideFlags = HideFlags.HideInHierarchy;
										if (PlayerNumTextObject.activeSelf) {
												SetScript.NumTextPreActive = true;
												PlayerNumTextObject.SetActive (false);
										} else {
												SetScript.NumTextPreActive = false;
										}
								}
						}
				}
				EditorGUI.EndChangeCheck ();
				EditorGUILayout.EndVertical ();

				//EditorGUILayout.Separator ();

				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUILayout.LabelField ("Used Method for Open&Close Window");
				SetScript.OpenCloseMethodNum = EditorGUILayout.IntPopup (" ", SetScript.OpenCloseMethodNum, DisplayedOpenCloseMethod, OptionOpenCloseMethod);

				/*Toggleの場合
				EditorGUILayout.BeginVertical (GUI.skin.box);
				SetScript.AlwaysOpenToggle = EditorGUILayout.Toggle ("Always OpenWindow", SetScript.AlwaysOpenToggle);
				if (SetScript.AlwaysOpenToggle) {
						EditorGUI.indentLevel++;
						SetScript.MappingWindowDestroyToggle = EditorGUILayout.Toggle ("Destroy to Close", SetScript.MappingWindowDestroyToggle);
						EditorGUI.indentLevel--;
				}
				*/
				/* Toggleの場合で,最初のToggleがfalseの時は2つめのtoggleを薄く表示したままで機能しないようにするには
				EditorGUI.BeginDisabledGroup (SetScript.AlwaysOpenToggle);
				SetScript.MappingWindowDestroyToggle = EditorGUILayout.Toggle ("Destroy to Close", SetScript.MappingWindowDestroyToggle);
				EditorGUI.EndDisabledGroup ();
				 */
				EditorGUILayout.EndVertical ();
				EditorGUI.EndDisabledGroup ();

				//EditorGUILayout.Separator ();
				//EditorGUILayout.Space ();

				EditorGUILayout.BeginVertical (GUI.skin.box);
				//SetScript.FoldoutBool = GUILayout.Toggle( SetScript.FoldoutBool, "Click to "+(SetScript.FoldoutBool ? "collapse":"expand"), "Foldout", GUILayout.ExpandWidth(false));
				//SetScript.FoldoutBool = EditorGUILayout.Foldout(SetScript.FoldoutBool, "Default Input Mapping");
				/*
						SetScript.FoldoutBool = GUILayout.Toggle( SetScript.FoldoutBool, "Default Input Mapping", "Foldout", GUILayout.ExpandWidth(false));
						if (SetScript.FoldoutBool) {           
							for(int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
								SetScript.DefaultInputArray[i] = EditorGUILayout.TextField("    "+SetScript.MenuItemHeadings[i], SetScript.DefaultInputArray[i]);
							}
						}
						*/
				/*
						EditorGUILayout.GetControlRect (true, EditorGUIUtility.singleLineHeight, EditorStyles.foldout);
						Rect DefaKeyFoldRect = GUILayoutUtility.GetLastRect ();
						if (Event.current.type == EventType.MouseUp && DefaKeyFoldRect.Contains (Event.current.mousePosition)) {
								SetScript.DefaKeyFoldoutBool = !SetScript.DefaKeyFoldoutBool;
								//Event.current.Use(); //以降のクリック判定を抑制,重なっている下のものなどの判定をしない時に使う,これを入れると矢印部分にクリック判定が届かず色が変わらない
						}
						SetScript.DefaKeyFoldoutBool = EditorGUI.Foldout (DefaKeyFoldRect, SetScript.DefaKeyFoldoutBool, "Default Input Mapping");
						if (SetScript.DefaKeyFoldoutBool) {
								EditorGUI.indentLevel++;
								for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
										SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
								}
						*/


				EditorGUI.indentLevel++;
				EditorGUILayout.Space ();
				SetScript.jInputSOData.DefaKeyFoldoutBool = Foldout (SetScript.jInputSOData.DefaKeyFoldoutBool, "Default Input Mapping", true, EditorStyles.foldout);
				EditorGUILayout.Space ();
				if (SetScript.jInputSOData.DefaKeyFoldoutBool) {
						if (SetScript.PlayerNum == 1) {
								EditorGUI.indentLevel++;
								EditorGUI.BeginDisabledGroup (InspectorDisabled);
								EditorGUI.BeginChangeCheck ();
								try {
										//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
										//		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
										//}
										EditorGUI.indentLevel--;
										EditorGUI.indentLevel--;
										EditorGUILayout.BeginHorizontal ();
										EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
										GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
										for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
												GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
										}
										EditorGUILayout.EndVertical ();
										EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
										m_list.DoLayoutList ();
										EditorGUILayout.EndVertical ();
										EditorGUILayout.EndHorizontal ();
										serializedObject.ApplyModifiedProperties ();
										EditorGUI.indentLevel++;
										EditorGUI.indentLevel++;
								} catch {
										SetScript.DefferentLengthArrayRenew ();
										//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
										//		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
										//}
										EditorGUI.indentLevel--;
										EditorGUI.indentLevel--;
										EditorGUILayout.BeginHorizontal ();
										EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
										GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
										for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
												GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
										}
										EditorGUILayout.EndVertical ();
										EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
										m_list.DoLayoutList ();
										EditorGUILayout.EndVertical ();
										EditorGUILayout.EndHorizontal ();
										serializedObject.ApplyModifiedProperties ();
										EditorGUI.indentLevel++;
										EditorGUI.indentLevel++;
								}
								if (GUI.changed) {
										for (int i=0; i<SetScript.DefaultInputArray.Length; i++) {
												if (SetScript.DefaultInputArray [i].Length == 1) {
														if (System.Text.RegularExpressions.Regex.IsMatch (SetScript.DefaultInputArray [i], "[a-z]")) {
																SetScript.DefaultInputArray [i] = SetScript.DefaultInputArray [i].ToUpper ();
														}
												}
										}
										SetScript.ArrayCopyToSOData ();
								}
								EditorGUI.EndChangeCheck ();
								EditorGUI.EndDisabledGroup ();
								
								/*
								if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused) {
										if (DefaKeyButtonBoolCheck != true && SetScript.DefaKeyButtonBool != false) {
												//Play停止時にこのscript内のprivate boolであるDefaKeyButtonBoolCheckは自動でoffになる
												SetScript.DefaKeyButtonBool = false;
												Debug.Log ("Cancel Default Key Set Mode.");
										}
										if (SetScript.DefaKeyButtonBool != false) {
												if (GUILayout.Button ("..")) {
														SetScript.DefaKeyButtonBool = false;
														DefaKeyButtonBoolCheck = false;
												}
										} else {
												if (GUILayout.Button ("Default Key Set Mode")) {
														Debug.Log ("Default Key Set Mode ON");
														EditorApplication.isPlaying = true;
														DefaKeyButtonBoolCheck = true;
														SetScript.DefaKeyButtonBool = true;
												}
										}
								} else { //Play中
										if (SetScript.DefaKeyButtonBool != false) {
												if (GUILayout.Button ("Default Key Set Mode [ON]")) {
														EditorApplication.isPlaying = false;
												}
												//デフォキー変更を適用した場合の処理(boolをONして,OnGUI()でそのboolがONの時だけ描画)
												//Repaint ();
										} else {
												EditorGUI.BeginDisabledGroup (InspectorDisabled);
												GUILayout.Button ("..");
												EditorGUI.EndDisabledGroup ();
										}
								}
								*/

								EditorGUI.indentLevel--;
						} else { //SetScript.PlayerNum!=1
								EditorGUI.indentLevel++;
								if (SetScript.PlayerNum >= 2) {
										SetScript.jInputSOData.DefaKey1pFoldoutBool = Foldout (SetScript.jInputSOData.DefaKey1pFoldoutBool, "Player1 Default Input", true, EditorStyles.foldout);
										if (SetScript.jInputSOData.DefaKey1pFoldoutBool) {
												EditorGUI.BeginDisabledGroup (InspectorDisabled);
												EditorGUI.BeginChangeCheck ();
												try {
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												} catch {
														SetScript.DefferentLengthArrayRenew ();
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												}
												if (GUI.changed) {
														for (int i=0; i<SetScript.DefaultInputArray.Length; i++) {
																if (SetScript.DefaultInputArray [i].Length == 1) {
																		if (System.Text.RegularExpressions.Regex.IsMatch (SetScript.DefaultInputArray [i], "[a-z]")) {
																				SetScript.DefaultInputArray [i] = SetScript.DefaultInputArray [i].ToUpper ();
																		}
																}
														}
														SetScript.ArrayCopyToSOData ();
												}
												EditorGUI.EndChangeCheck ();
												EditorGUI.EndDisabledGroup ();
										}

										SetScript.jInputSOData.DefaKey2pFoldoutBool = Foldout (SetScript.jInputSOData.DefaKey2pFoldoutBool, "Player2 Default Input", true, EditorStyles.foldout);
										if (SetScript.jInputSOData.DefaKey2pFoldoutBool) {
												EditorGUI.BeginDisabledGroup (InspectorDisabled);
												EditorGUI.BeginChangeCheck ();
												try {
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray2p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray2p [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list2p.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												} catch {
														SetScript.DefferentLengthArrayRenew2 ();
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray2p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray2p [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list2p.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												}
												if (GUI.changed) {
														for (int i=0; i<SetScript.DefaultInputArray2p.Length; i++) {
																if (SetScript.DefaultInputArray2p [i].Length == 1) {
																		if (System.Text.RegularExpressions.Regex.IsMatch (SetScript.DefaultInputArray2p [i], "[a-z]")) {
																				SetScript.DefaultInputArray2p [i] = SetScript.DefaultInputArray2p [i].ToUpper ();
																		}
																}
														}
														SetScript.ArrayCopyToSOData2 ();
												}
												EditorGUI.EndChangeCheck ();
												EditorGUI.EndDisabledGroup ();
										}
								}

								if (SetScript.PlayerNum >= 3) {
										SetScript.jInputSOData.DefaKey3pFoldoutBool = Foldout (SetScript.jInputSOData.DefaKey3pFoldoutBool, "Player3 Default Input", true, EditorStyles.foldout);
										if (SetScript.jInputSOData.DefaKey3pFoldoutBool) {
												EditorGUI.BeginDisabledGroup (InspectorDisabled);
												EditorGUI.BeginChangeCheck ();
												try {
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray3p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray3p [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list3p.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												} catch {
														SetScript.DefferentLengthArrayRenew3 ();
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray3p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray3p [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list3p.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												}
												if (GUI.changed) {
														for (int i=0; i<SetScript.DefaultInputArray3p.Length; i++) {
																if (SetScript.DefaultInputArray3p [i].Length == 1) {
																		if (System.Text.RegularExpressions.Regex.IsMatch (SetScript.DefaultInputArray3p [i], "[a-z]")) {
																				SetScript.DefaultInputArray3p [i] = SetScript.DefaultInputArray3p [i].ToUpper ();
																		}
																}
														}
														SetScript.ArrayCopyToSOData3 ();
												}
												EditorGUI.EndChangeCheck ();
												EditorGUI.EndDisabledGroup ();
										}
								}

								if (SetScript.PlayerNum >= 4) {
										SetScript.jInputSOData.DefaKey4pFoldoutBool = Foldout (SetScript.jInputSOData.DefaKey4pFoldoutBool, "Player4 Default Input", true, EditorStyles.foldout);
										if (SetScript.jInputSOData.DefaKey4pFoldoutBool) {
												EditorGUI.BeginDisabledGroup (InspectorDisabled);
												EditorGUI.BeginChangeCheck ();
												try {
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray4p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray4p [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list4p.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												} catch {
														SetScript.DefferentLengthArrayRenew4 ();
														//for (int i=0; i<SetScript.MenuItemHeadings.Length; i++) {
														//		SetScript.DefaultInputArray4p [i] = EditorGUILayout.TextField (SetScript.MenuItemHeadings [i], SetScript.DefaultInputArray4p [i]);
														//}
														EditorGUI.indentLevel--;
														EditorGUI.indentLevel--;
														EditorGUILayout.BeginHorizontal ();
														EditorGUILayout.BeginVertical (GUI.skin.box, GUILayout.ExpandWidth (false));
														GUILayout.Box ("", GUIStyle.none, GUILayout.ExpandWidth (false), GUILayout.Height (12));
														for (var i=0; i<SetScript.MenuItemHeadings.Length; i++) {
																GUILayout.Label ("E" + i + ": " + SetScript.MenuItemHeadings [i], jInputGUIStyle, GUILayout.ExpandWidth (false));
														}
														EditorGUILayout.EndVertical ();
														EditorGUILayout.BeginVertical (GUILayout.ExpandWidth (true));
														m_list4p.DoLayoutList ();
														EditorGUILayout.EndVertical ();
														EditorGUILayout.EndHorizontal ();
														serializedObject.ApplyModifiedProperties ();
														EditorGUI.indentLevel++;
														EditorGUI.indentLevel++;
												}
												if (GUI.changed) {
														for (int i=0; i<SetScript.DefaultInputArray4p.Length; i++) {
																if (SetScript.DefaultInputArray4p [i].Length == 1) {
																		if (System.Text.RegularExpressions.Regex.IsMatch (SetScript.DefaultInputArray4p [i], "[a-z]")) {
																				SetScript.DefaultInputArray4p [i] = SetScript.DefaultInputArray4p [i].ToUpper ();
																		}
																}
														}
														SetScript.ArrayCopyToSOData4 ();
												}
												EditorGUI.EndChangeCheck ();
												EditorGUI.EndDisabledGroup ();
										}
								}
								EditorGUI.indentLevel--;
						}
				}
				EditorGUI.indentLevel--;
				EditorGUILayout.Space ();
				if (SetScript.jInputSOData.DefaultInputNameInconsistencyCheck) {
						EditorGUILayout.HelpBox (DefaKeyInconsistencyErrorText + SetScript.jInputSOData.DefaKeyInconsistencyListText, MessageType.Error, true);
				}
				EditorGUILayout.EndVertical ();

				//EditorGUILayout.Separator ();
				//EditorGUILayout.Space ();

				EditorGUI.BeginDisabledGroup (InspectorDisabled);
				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUI.BeginChangeCheck ();
				SetScript.flags = EditorGUILayout.MaskField ("ExcludeDecisionFnc", SetScript.flags, SetScript.MenuItemHeadingsCopy);
				if (GUI.changed) {
						SetScript.DropdownToList ();
				}
				EditorGUI.EndChangeCheck ();
				EditorGUILayout.EndVertical ();
				EditorGUI.EndDisabledGroup ();

				//EditorGUILayout.Separator ();

				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUI.indentLevel++;
				SetScript.jInputSOData.AxesFoldoutBool = Foldout (SetScript.jInputSOData.AxesFoldoutBool, "Axes Advance Settings", true, EditorStyles.foldout);
				if (SetScript.jInputSOData.AxesFoldoutBool) {
						EditorGUI.BeginChangeCheck ();
						SetScript.DeadZone = SetScript.jInputSOData.DeadZone = EditorGUILayout.Slider ("AxisDeadZone", SetScript.DeadZone, 0.01f, 1.0f);
						SetScript.Gravity = SetScript.jInputSOData.Gravity = EditorGUILayout.Slider ("AxisGravity", SetScript.Gravity, 0.1f, 50.0f);
						SetScript.Sensitivity = SetScript.jInputSOData.Sensitivity = EditorGUILayout.Slider ("AxisSensitivity", SetScript.Sensitivity, 0.1f, 50.0f);
						if (GUI.changed) {
								SetScript.jInputSOData.AxesSetApply ();
						}
						EditorGUI.EndChangeCheck ();
				}
				EditorGUI.indentLevel--;
				EditorGUILayout.EndVertical ();

				EditorGUILayout.Space ();

				if (GUI.changed) {
						EditorUtility.SetDirty (target);
						SetScript.jInputSOData.SOValueDetermine ();
				}
				//Repaint ();

				//元の標準のIndpectorを表示
				//DrawDefaultInspector ();

		}
	
		ReorderableList m_list;
		ReorderableList m_list2p;
		ReorderableList m_list3p;
		ReorderableList m_list4p;
		SerializedProperty ReoListElement;

		void OnEnable ()
		{//ドラッグ可能なInspectorでのListの設定はOnEnableで行わないとドラッグが効かない場合有り
		
				//jInputSettings SetScript = target as jInputSettings;

				//new UnityEditorInternal.ReorderableList(List<SomeType> someList, typeof(SomeType), dragable, displayHeader, displayAddButton, displayRemoveButton);
				m_list = new ReorderableList (serializedObject, serializedObject.FindProperty ("DefaultInputArray"), true, true, false, false);
		
				m_list.drawHeaderCallback = (rect) => {
						EditorGUI.LabelField (rect, "1P");
				};
		
				m_list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
						ReoListElement = m_list.serializedProperty.GetArrayElementAtIndex (index);
						rect.y += 2;

						//入力欄の背景に薄く文字を入れる
						//EditorGUI.LabelField (rect, string.Format ("{0}:{1}", index, "stringValue"));

						/*ラベルと入力欄が一緒にドラッグできるようにしたい場合
						EditorGUI.LabelField (rect, "E" + index + ": " + SetScript.MenuItemHeadings [index]);
						EditorGUI.PropertyField (new Rect (rect.x + (rect.width * 1 / 2), rect.y, (rect.width * 1 / 2), EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
						*/
						EditorGUI.PropertyField (new Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
				};


				m_list2p = new ReorderableList (serializedObject, serializedObject.FindProperty ("DefaultInputArray2p"), true, true, false, false);
			
				m_list2p.drawHeaderCallback = (rect) => {
						EditorGUI.LabelField (rect, "2P");
				};
			
				m_list2p.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
						ReoListElement = m_list2p.serializedProperty.GetArrayElementAtIndex (index);
						rect.y += 2;
						EditorGUI.PropertyField (new Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
				};
			

				m_list3p = new ReorderableList (serializedObject, serializedObject.FindProperty ("DefaultInputArray3p"), true, true, false, false);
			
				m_list3p.drawHeaderCallback = (rect) => {
						EditorGUI.LabelField (rect, "3P");
				};
				m_list3p.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
						ReoListElement = m_list3p.serializedProperty.GetArrayElementAtIndex (index);
						rect.y += 2;
						EditorGUI.PropertyField (new Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
				};


				m_list4p = new ReorderableList (serializedObject, serializedObject.FindProperty ("DefaultInputArray4p"), true, true, false, false);

				m_list4p.drawHeaderCallback = (rect) => {
						EditorGUI.LabelField (rect, "4P");
				};
				m_list4p.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
						ReoListElement = m_list4p.serializedProperty.GetArrayElementAtIndex (index);
						rect.y += 2;
						EditorGUI.PropertyField (new Rect (rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
				};

		
				/*
				m_list = new ReorderableList (serializedObject, serializedObject.FindProperty ("DefaultInputArray"), true, true, false, false);
				m_list.drawHeaderCallback = (rect) => {
						EditorGUI.LabelField (rect, DefaInputPlayerString);
				};
			
				m_list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
						ReoListElement = m_list.serializedProperty.GetArrayElementAtIndex (index);
						rect.y += 2;
				
						EditorGUI.LabelField (rect, "E" + index + ": " + SetScript.MenuItemHeadings [index]);
						EditorGUI.PropertyField (new Rect (rect.x + (rect.width * 1 / 2), rect.y, (rect.width * 1 / 2), EditorGUIUtility.singleLineHeight), ReoListElement, GUIContent.none);
				};
				*/

		}


}
