using UnityEngine;
using System.Collections;
using System;
//using System.Collections.Generic;

public class Mapper : MonoBehaviour
{
		public static string[] InputArray;
		public static string[] InputArray2p;
		public static string[] InputArray3p;
		public static string[] InputArray4p;
		[SerializeField]
		jInput
				jInputSOData;
		[HideInInspector]
		public string[]
				AllJoinInputArray;
		[HideInInspector]
		public string
				MapperSaveData;
		[HideInInspector]
		public bool
				IstCheck;
		bool LevelLoadedCheck;
		static int PlayerNum;
		static int InputLength;
		SaveLoadScript SaveLoad;
		public static jInputSettings SetScript;


		#if (UNITY_EDITOR)
		public static bool
				EditorPlaymodeStop;
		#endif
	

		public void LoadfailureDeal ()
		{
		
				//write to deal with when have failed to operate load data of input mapping
		
		
				SaveFileDelete ();
		}
	
		public void SaveFileDelete ()
		{
				#if (UNITY_STANDALONE || UNITY_EDITOR)
				if (SaveLoad != null) {
						SaveLoad = gameObject.GetComponent<SaveLoadScript> ();
				}
				SaveLoad.DeleteInputSetting ();
				#else
				PlayerPrefs.DeleteKey("PrefsMappingData");
				#endif
				Debug.LogWarning ("[jInput] jInput have deleted the save data file. It is wrong Dafault Input Mapping name or there is a suspicion that the save data file was broken.");
				Debug.Break ();
		
				#if (UNITY_EDITOR)
				EditorPlaymodeStop = true;
				return;
				#else
				SetScriptSearch ();
				if (SetScript != null){
					SetScript.ResetSet ();
				}
				Start ();
				#endif
		}

		public void SaveSucsessIndicate ()
		{
				if (SetScript != null)
						SetScript.SaveSucsessIndicate ();
		}
		public void SavefailureIndicate ()
		{
				if (SetScript != null)
						SetScript.SavefailureIndicate ();
		}
		public void LoadData ()
		{
				#if (UNITY_STANDALONE || UNITY_EDITOR)
				SaveLoad.LoadInputSetting ();
				#else
				MapperSaveData = PlayerPrefs.GetString("PrefsMappingData");
				#endif
				AllJoinInputArray = MapperSaveData.Split (',');
				Array.Copy (AllJoinInputArray, 0, Mapper.InputArray, 0, InputLength);
				if (PlayerNum >= 2) {
						Array.Copy (AllJoinInputArray, InputLength, Mapper.InputArray2p, 0, InputLength);
				}
				if (PlayerNum >= 3) {
						Array.Copy (AllJoinInputArray, InputLength * 2, Mapper.InputArray3p, 0, InputLength);
				}
				if (PlayerNum >= 4) {
						Array.Copy (AllJoinInputArray, InputLength * 3, Mapper.InputArray4p, 0, InputLength);
				}
		}
		public void SaveData ()
		{
				MapperSaveData = string.Join (",", AllJoinInputArray);
				#if (UNITY_STANDALONE || UNITY_EDITOR)
				SaveLoad.SaveInputSetting ();
				#else
				PlayerPrefs.SetString("PrefsMappingData", MapperSaveData);
				PlayerPrefs.Save();
				#endif
		}

		void Start ()
		{
				#if (UNITY_EDITOR)
				EditorPlaymodeStop = false;
				#endif
				if (SetScript == null) {
						try {
								SetScript = GameObject.Find ("jInputMappingSet").GetComponent<jInputSettings> ();
						} catch {

						}
				}
				if (PlayerNum <= 0 || InputLength <= 0) {
						if (SetScript != null) {
								PlayerNum = SetScript.PlayerNum;
								InputLength = SetScript.DefaultInputArray.Length;
						} else {
								PlayerNum = jInputSOData.PlayerNum;
								InputLength = jInputSOData.DefaultInputArray.Count;
						}
				}
				if (PlayerNum <= 0 || InputLength <= 0) {
						ErrorStopEditor ();
				}
				#if (UNITY_STANDALONE || UNITY_EDITOR)
				if (SaveLoad == null)
						SaveLoad = gameObject.GetComponent<SaveLoadScript> ();
				#endif
				if (AllJoinInputArray.Length <= 0) {
						AllJoinInputArray = new string[InputLength * PlayerNum + 1];
				}
				if (InputArray == null) {
						InputArray = new string[InputLength];
						if (SetScript != null) {
								SetScript.DefaultInputArray.CopyTo (AllJoinInputArray, 0);
						} else {
								if (jInputSOData.DefaultInputArray.Count == 0 || jInputSOData.DefaultInputArray [0] == null) {
										ErrorStopEditor ();
								} else {
										jInputSOData.DefaultInputArray.CopyTo (AllJoinInputArray, 0);
								}
						}
						LevelLoadedCheck = true;
				}
				if (PlayerNum >= 2) {
						if (InputArray2p == null) {
								InputArray2p = new string[InputLength];
								if (SetScript != null) {
										SetScript.DefaultInputArray2p.CopyTo (AllJoinInputArray, InputLength);
								} else {
										if (jInputSOData.DefaultInputArray2p.Count == 0 || jInputSOData.DefaultInputArray2p [0] == null || jInputSOData.DefaultInputArray2p.Count != InputLength) {
												ErrorStopEditor ();
										} else {
												jInputSOData.DefaultInputArray2p.CopyTo (AllJoinInputArray, InputLength);
										}
								}
								LevelLoadedCheck = true;
						}
				}
				if (PlayerNum >= 3) {
						if (InputArray3p == null) {
								InputArray3p = new string[InputLength];
								if (SetScript != null) {
										SetScript.DefaultInputArray3p.CopyTo (AllJoinInputArray, InputLength * 2);
								} else {
										if (jInputSOData.DefaultInputArray3p.Count == 0 || jInputSOData.DefaultInputArray3p [0] == null || jInputSOData.DefaultInputArray3p.Count != InputLength) {
												ErrorStopEditor ();
										} else {
												jInputSOData.DefaultInputArray3p.CopyTo (AllJoinInputArray, InputLength * 2);
										}
								}
								LevelLoadedCheck = true;
						}
				}
				if (PlayerNum >= 4) {
						if (InputArray4p == null) {
								InputArray4p = new string[InputLength];
								if (SetScript != null) {
										SetScript.DefaultInputArray4p.CopyTo (AllJoinInputArray, InputLength * 3);
								} else {
										if (jInputSOData.DefaultInputArray4p.Count == 0 || jInputSOData.DefaultInputArray4p [0] == null || jInputSOData.DefaultInputArray4p.Count != InputLength) {
												ErrorStopEditor ();
										} else {
												jInputSOData.DefaultInputArray4p.CopyTo (AllJoinInputArray, InputLength * 3);
										}
								}
								LevelLoadedCheck = true;
						}
				}

				if (LevelLoadedCheck != false) {
						MapperSaveData = string.Join (",", AllJoinInputArray);
		
						#if (UNITY_STANDALONE || UNITY_EDITOR)
						SaveLoad.LoadInputSetting ();
						#else
						if(PlayerPrefs.HasKey("PrefsMappingData")) {
							MapperSaveData = PlayerPrefs.GetString("PrefsMappingData");
						}else {
							PlayerPrefs.SetString("PrefsMappingData", MapperSaveData);
						}
						#endif
		
						string[] LoadArray = MapperSaveData.Split (',');
						if (AllJoinInputArray.Length != LoadArray.Length) {
								Debug.LogWarning ("[jInput] SaveData items are different number of current input items.");
								MapperSaveData = string.Join (",", AllJoinInputArray);
			
								#if (UNITY_STANDALONE || UNITY_EDITOR)
								SaveLoad.DeleteInputSetting ();
								SaveLoad.SaveInputSetting ();
								#else
								PlayerPrefs.DeleteKey("PrefsMappingData");
								PlayerPrefs.SetString("PrefsMappingData", MapperSaveData);
								#endif
								Debug.LogWarning ("[jInput] jInputMapping was created new SaveData with Default Input Mapping.");
						} else {
								AllJoinInputArray = MapperSaveData.Split (',');
						}
						Array.Copy (AllJoinInputArray, 0, InputArray, 0, InputLength);
						if (PlayerNum >= 2) {
								Array.Copy (AllJoinInputArray, InputLength, InputArray2p, 0, InputLength);
						}
						if (PlayerNum >= 3) {
								Array.Copy (AllJoinInputArray, InputLength * 2, InputArray3p, 0, InputLength);
						}
						if (PlayerNum >= 4) {
								Array.Copy (AllJoinInputArray, InputLength * 3, InputArray4p, 0, InputLength);
						}
				}
				LevelLoadedCheck = false;
				DontDestroyOnLoad (gameObject);
		}
		void OnLevelWasLoaded ()
		{
				Start ();
		}

		void ErrorStopEditor ()
		{
				Debug.LogError ("[jInput] Error!! To verify jInput Settings and re-create the input mapping data!!");
				Debug.Break ();
				#if (UNITY_EDITOR)
				EditorPlaymodeStop = true;
				return;
				#else
				return;
				#endif
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						SetScriptSearch ();
						if (SetScript != null) {
								SetScript.EscBehavior ();
						} else {
								
						}
				}
		}

		public static void BackPlayerSelect ()
		{
				SetScriptSearch ();
				if (SetScript != null) {
						SetScript.BackPlayerSelect ();
				}
		}

		public static void MappingWindowOpen ()
		{
				SetScriptSearch ();
				if (SetScript != null) {
						SetScript.MappingWindowOpen ();
				}
		}

		public static void MappingWindowClose ()
		{
				SetScriptSearch ();
				if (SetScript != null) {
						SetScript.MappingWindowClose ();
				}
		}

		public static void MappingWindowClose (jInputSettings.MultiDelegate TemporaryDelegate)
		{
				SetScriptSearch ();
				if (SetScript != null) {
						SetScript.MappingWindowClose (TemporaryDelegate);
				}
		}

		public static void MappingWindowDestroy ()
		{
				SetScriptSearch ();
				if (SetScript != null) {
						SetScript.MappingWindowDestroy ();
				}
		}

		static void SetScriptSearch ()
		{
				if (SetScript == null) {
						try {
								SetScript = GameObject.Find ("jInputMappingSet").GetComponent<jInputSettings> ();
						} catch {
								//Debug.LogWarning ("[jInput] jInputMappingSet Not Found.");
						}
				}
		}

}
