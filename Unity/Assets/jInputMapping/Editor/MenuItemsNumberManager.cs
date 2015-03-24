using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using  System.Collections.Generic;

[InitializeOnLoad]
public class MenuItemsNumberManager : MonoBehaviour
{
		static double NextItemCheckTime = 5.0;
		static GameObject MappingSet;
		static GameObject InMenuItems;
		static jInputSettings SetScript;
		static GameObject MenuItemPrefab;
		static int ItemsNum;
		static int HeadAryLngth;
		static string ComparisonName;
		static GameObject TemporaryItem;
		static string PreviousComparisonName;
		static int ItemListIndex;
		static GameObject MappingManagerPrefab;
		static bool ExistMappingManagerCheck = false;

		static MenuItemsNumberManager ()
		{

				EditorApplication.update += MyUpdate; //EditorApplication.update デリゲートに処理を入れると毎フレーム呼び出される
				//EditorApplication.hierarchyWindowChanged += MyUpdate;
				//EditorApplication.projectWindowChanged += MyUpdate;
				//EditorApplication.playmodeStateChanged += MyUpdate;

		}

		static void MyUpdate ()
		{
				if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying || EditorApplication.isPaused) {
						
						if (ExistMappingManagerCheck != true) {
								if (GameObject.Find ("jInputMappingManager") == null) {
										if (MappingManagerPrefab == null) {
												//Resources.Loadを使っても良い
												string[] Pathes = AssetDatabase.FindAssets ("jInputMappingManager t:GameObject");
												if (Pathes.Length >= 1) {
														string MappingManagerPath = AssetDatabase.GUIDToAssetPath (Pathes [0]);
														MappingManagerPrefab = Resources.LoadAssetAtPath<GameObject> (MappingManagerPath);
												}
												if (Pathes.Length > 1) {
														Debug.LogError ("[jInput] There are some GameObjects included in the name 'jInputMappingManager' in Project window!! There is a possibility that it may not operate normally.");
												}
										}
										if (MappingManagerPrefab != null) {
												GameObject Ist = Instantiate (MappingManagerPrefab) as GameObject;
												Ist.name = "jInputMappingManager";
												Ist.hideFlags = HideFlags.HideInHierarchy;
												Ist.GetComponent<Mapper> ().IstCheck = true;
												ExistMappingManagerCheck = true;
										} else {
												Debug.LogError ("[jInput] Error! jInputMappingManager Prefab Not Found in Project window!!");
												EditorPlaymodeStop ();
												return;
										}
								}
						}
				} else {
						ExistMappingManagerCheck = false;
						GameObject FindMappingManager;
						if (FindMappingManager = GameObject.Find ("jInputMappingManager")) {
								if (FindMappingManager.GetComponent<Mapper> ().IstCheck == true)
										DestroyImmediate (GameObject.Find ("jInputMappingManager"));
						}
				}


				if (Mapper.EditorPlaymodeStop != false) {
						EditorPlaymodeStop ();
				}

				//PlayするとjInputMappingSetが非表示だと取れずにエラーになる部分があるのでエディット中だけ動作
				if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying && !EditorApplication.isPaused) {
								
						if (MappingSet == null) {
								MappingSet = GameObject.Find ("jInputMappingSet");
						} else {
								if (SetScript == null) {
										SetScript = MappingSet.GetComponent<jInputSettings> ();
										if (SetScript.jInputSOData.DefaultInputNameInconsistencyCheck) {
												Selection.activeGameObject = MappingSet;
										}
								}
								if (InMenuItems == null) {
										InMenuItems = GameObject.Find ("jInputMappingSet/InMapperMenuItems");
								}
								if (MenuItemPrefab == null && InMenuItems != null) {
										MenuItemPrefab = InMenuItems.GetComponent<MenuItemsCommonSetting> ().MenuItemPrefab;
								}

								if (EditorApplication.timeSinceStartup > NextItemCheckTime) {
										ItemsNum = InMenuItems.transform.childCount;
										HeadAryLngth = SetScript.MenuItemHeadings.Length;
						
										if (ItemsNum != HeadAryLngth) {
												ItemsNumMatch ();
										} else {
												ItemsSort ();
										}
										NextItemCheckTime = EditorApplication.timeSinceStartup + 0.5;
								}
						}
				}
		}

		static void ItemsNumMatch ()
		{
				ItemListIndex = -1;

				List<string> MenuItemsList = new List<string> ();
				for (int i = 0; i < ItemsNum; i++) {
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

						if (i < HeadAryLngth && i >= 0) {
								if (ItemListIndex == -1 && MenuItemPrefab != null) {
										TemporaryItem = PrefabUtility.InstantiatePrefab (MenuItemPrefab) as GameObject;
										Undo.RegisterCreatedObjectUndo (TemporaryItem, "Inspector");
										if (i == 0) {//カメラの位置と角度を基準にtransformを変える
												TemporaryItem.transform.parent = InMenuItems.transform;
												TemporaryItem.transform.position = InMenuItems.transform.position;
												TemporaryItem.transform.rotation = InMenuItems.transform.rotation;
										} else if (i > 0 && i <= 30) {//ひとつ前の番号の少し下にtransformを変える
												GameObject OneBeforeItem = InMenuItems.transform.FindChild (PreviousComparisonName).gameObject;
												if (OneBeforeItem != null) {
														TemporaryItem.transform.parent = InMenuItems.transform;
														TemporaryItem.transform.position = OneBeforeItem.transform.position + new Vector3 (0, -0.6f, 0);
														TemporaryItem.transform.rotation = OneBeforeItem.transform.rotation;
														TemporaryItem.transform.localScale = OneBeforeItem.transform.localScale;
												}
										}
										TemporaryItem.name = ComparisonName;
										TemporaryItem.transform.SetSiblingIndex (i);

								} else if (ItemListIndex != i) {
										TemporaryItem = InMenuItems.transform.FindChild (ComparisonName).gameObject;
										TemporaryItem.transform.SetSiblingIndex (i);
										MenuItemsList.RemoveAt (ItemListIndex);
								} else if (ItemListIndex == i) {
										MenuItemsList.RemoveAt (ItemListIndex);
								}
						} else {
								if (ItemListIndex != -1) {
										TemporaryItem = InMenuItems.transform.FindChild (ComparisonName).gameObject;
										Undo.DestroyObjectImmediate (TemporaryItem);
										DestroyImmediate (TemporaryItem);
										MenuItemsList.RemoveAt (ItemListIndex);
								}
						}
						PreviousComparisonName = ComparisonName;
				}
				if (MenuItemsList.Count > 0) {
						Debug.LogError ("[jInput] There is otiose other item or same name item in jInputMappingSet/InMapperMenuItems.");
				}
		}

		static void ItemsSort ()
		{
				ItemListIndex = -1;
			
				if (InMenuItems != null) {
						List<string> MenuItemsList = new List<string> ();
						for (int i = 0; i < ItemsNum; i++) {
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
					
								if (i < HeadAryLngth && i >= 0) {
										if (ItemListIndex != i && ItemListIndex != -1) {
												TemporaryItem = InMenuItems.transform.FindChild (ComparisonName).gameObject;
												TemporaryItem.transform.SetSiblingIndex (i);
										} else if (ItemListIndex == -1 && MenuItemPrefab != null) {
												TemporaryItem = PrefabUtility.InstantiatePrefab (MenuItemPrefab) as GameObject;
												Undo.RegisterCreatedObjectUndo (TemporaryItem, "Inspector");
												if (i == 0) {//カメラの位置と角度を基準にtransformを変える
														TemporaryItem.transform.parent = InMenuItems.transform;
														TemporaryItem.transform.position = InMenuItems.transform.position;
														TemporaryItem.transform.rotation = InMenuItems.transform.rotation;
												} else if (i > 0 && i <= 30) {//ひとつ前の番号の少し下にtransformを変える
														GameObject OneBeforeItem = InMenuItems.transform.FindChild (PreviousComparisonName).gameObject;
														if (OneBeforeItem != null) {
																TemporaryItem.transform.parent = InMenuItems.transform;
																TemporaryItem.transform.position = OneBeforeItem.transform.position + new Vector3 (0, -0.6f, 0);
																TemporaryItem.transform.rotation = OneBeforeItem.transform.rotation;
																TemporaryItem.transform.localScale = OneBeforeItem.transform.localScale;
														}
												}
												TemporaryItem.name = ComparisonName;
												TemporaryItem.transform.SetSiblingIndex (i);
										}
								}
								PreviousComparisonName = ComparisonName;
						}
				}
		}

		static void EditorPlaymodeStop ()
		{
				Mapper.EditorPlaymodeStop = false;
				EditorApplication.isPaused = false;
				EditorApplication.isPlaying = false;
		}

}
