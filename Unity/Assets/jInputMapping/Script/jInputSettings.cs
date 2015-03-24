using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class jInputSettings : MonoBehaviour
{
	string[] ImpossibleMappingKeyArray = new string[] {//"Escape"key is impossible Mapping by default.
	//"W", "LeftShift", "Joystick1Button0", "MouseWheel+", "Joystick1Axis1-" //Example
				};

	public jInput jInputSOData;
	[Space(15)]
	public string[]
		MenuItemHeadings = new string[] {
				"UpMove",
				"DownMove",
				"RightMove",
				"LeftMove",
				"Rotate",
				"ChangeColor",
				"Particle"
				};
	[HideInInspector]
	public string[]
		DefaultInputArray = new string[] {
				"UpArrow",
				"DownArrow",
				"RightArrow",
				"LeftArrow",
				"Z",
				"Mouse0",
				"MouseWheel-"
				};
	[HideInInspector]
	public string[]
		DefaultInputArray2p;
	[HideInInspector]
	public string[]
		DefaultInputArray3p;
	[HideInInspector]
	public string[]
		DefaultInputArray4p;
		#if (UNITY_EDITOR)
	[HideInInspector]
	public List<string>
		DefaultInputArrayCopy;
	[HideInInspector]
	public List<string>
		DefaultInputArray2pCopy;
	[HideInInspector]
	public List<string>
		DefaultInputArray3pCopy;
	[HideInInspector]
	public List<string>
		DefaultInputArray4pCopy;
	[HideInInspector]
	public string[]
		MenuItemHeadingsCopy;
	[HideInInspector]
	public bool
		DefaKeyButtonBool;
	[HideInInspector]
	public bool
		MaxPlayer1Check;
	[HideInInspector]
	public bool
		NumWindowPreActive;
	[HideInInspector]
	public bool
		NumTextPreActive;
		#endif
	[HideInInspector]
	public List<int>
		ExcludeNum = new List<int> (); //publicにしておかないとPlay時に初期化されてしまい除外が効かなくなる
	[HideInInspector]
	public int
		flags = 0; //複数選択ドロップダウンのbit値の合計が入る
	[HideInInspector]
	public int
		PlayerNum = 1;
	string[] TemporaryInputArray;
	string[] TemporaryInputArray2p;
	string[] TemporaryInputArray3p;
	string[] TemporaryInputArray4p;
	string[] ProvisionalArray;
	[HideInInspector]
	public float
		DeadZone = 0.15f;
	[HideInInspector]
	public float
		Gravity = 5.0f;
	[HideInInspector]
	public float
		Sensitivity = 3.0f;
	[HideInInspector]
	public int
		SelectPosition;
	[HideInInspector]
	public int
		OperateLineSelectPosition;
	[HideInInspector]
	public bool
		MappingMode;
	[HideInInspector]
	public bool
		FirstSet;
	[HideInInspector]
	public bool
		CloseButtonSelect;
	[HideInInspector]
	public bool
		SaveNoSelectPosition;
	[HideInInspector]
	public int
		ExitSelectPosition;
	[HideInInspector]
	public TextMesh
		TextComponent;
	[HideInInspector]
	public string
		PreviousText;
	[HideInInspector]
	public bool
		OperateItemLine;
	[HideInInspector]
	public bool
		CurrentryRestore;
	[HideInInspector]
	public int
		OpenCloseMethodNum;
	List<string> InputCompareList;
	public delegate void MultiDelegate ();
	MultiDelegate CloseMappingDelegate;
	Mapper MapperScript;
	float v;
	float h;
	float verticalPositive;
	float verticalNegative;
	float horizontalPositive;
	float horizontalNegative;
	int MaxMenuSize;
	int MaxOperateMenuSize;
	int InputLength;
	int HeadDiff;
	Array KeyValues;
	string CurrentInput;
	string AxisName;
	string ComparisonInputName;
	string MapNameExclude;
	string MinHeadingKeep;
	GameObject PlayerNumTextObject;
	TextMesh PlayerNumTextMesh;
	float AxisDelayTimer;
	float KeyDelayTimer;
	bool AxisDelay;
	bool KeyDelay;
	bool PlayerNumSituation;
	bool SavingSituation;
	bool ExitSituation;
	bool NothingCloseButtonBool;
	RaycastHit hit;
	GameObject MenuItemAll;
	GameObject OperateItemAll;
	GameObject CurrentlyItem;
	GameObject OtherItem;
	SelectOrnament CurrentlyItemScript;
	SelectOrnament OtherItemScript;
	GameObject SaveConfirm;
	GameObject ExitConfirm;
	GameObject Selection;
	GameObject SaveSucsess;
	GameObject Savefailure;
	GameObject PlayerNumWindow;
	Camera jCamera;
	bool DefaultInputNameUnsuitableCheck;
	bool MappingWindowDestroyOn;
	[HideInInspector]
	public int
		PlayerSelectNum = 1;
	string JoystickNumString;
	int JoystickNum;
	int OtherMingleJoystickNum;
	string OtherMingleCheckText;
	int PlayerMappingNumber;
	PlayerMappingNumHold PlayerMappingNumScript;
	MenuItemsCommonSetting MenuItemsCommonScript;

	void vhRenew ()
	{
		if (PlayerNum == 2) { //when 2Players in Same Place
			verticalPositive = jInput.GetAxis (Mapper.InputArray [0]) + jInput.GetAxis (Mapper.InputArray2p [0]);
			verticalNegative = jInput.GetAxis (Mapper.InputArray [1]) + jInput.GetAxis (Mapper.InputArray2p [1]);
			horizontalPositive = jInput.GetAxis (Mapper.InputArray [2]) + jInput.GetAxis (Mapper.InputArray2p [2]);
			horizontalNegative = jInput.GetAxis (Mapper.InputArray [3]) + jInput.GetAxis (Mapper.InputArray2p [3]);
		} else if (PlayerNum == 3) { //when 3Players in Same Place
			verticalPositive = jInput.GetAxis (Mapper.InputArray [0]) + jInput.GetAxis (Mapper.InputArray2p [0]) + jInput.GetAxis (Mapper.InputArray3p [0]);
			verticalNegative = jInput.GetAxis (Mapper.InputArray [1]) + jInput.GetAxis (Mapper.InputArray2p [1]) + jInput.GetAxis (Mapper.InputArray3p [1]);
			horizontalPositive = jInput.GetAxis (Mapper.InputArray [2]) + jInput.GetAxis (Mapper.InputArray2p [2]) + jInput.GetAxis (Mapper.InputArray3p [2]);
			horizontalNegative = jInput.GetAxis (Mapper.InputArray [3]) + jInput.GetAxis (Mapper.InputArray2p [3]) + jInput.GetAxis (Mapper.InputArray3p [3]);
		} else if (PlayerNum == 4) { //when 4Players in Same Place
			verticalPositive = jInput.GetAxis (Mapper.InputArray [0]) + jInput.GetAxis (Mapper.InputArray2p [0]) + jInput.GetAxis (Mapper.InputArray3p [0]) + jInput.GetAxis (Mapper.InputArray4p [0]);
			verticalNegative = jInput.GetAxis (Mapper.InputArray [1]) + jInput.GetAxis (Mapper.InputArray2p [1]) + jInput.GetAxis (Mapper.InputArray3p [1]) + jInput.GetAxis (Mapper.InputArray4p [1]);
			horizontalPositive = jInput.GetAxis (Mapper.InputArray [2]) + jInput.GetAxis (Mapper.InputArray2p [2]) + jInput.GetAxis (Mapper.InputArray3p [2]) + jInput.GetAxis (Mapper.InputArray4p [2]);
			horizontalNegative = jInput.GetAxis (Mapper.InputArray [3]) + jInput.GetAxis (Mapper.InputArray2p [3]) + jInput.GetAxis (Mapper.InputArray3p [3]) + jInput.GetAxis (Mapper.InputArray4p [3]);
		} else { //when 1Player in Same Place
			verticalPositive = jInput.GetAxis (Mapper.InputArray [0]);
			verticalNegative = jInput.GetAxis (Mapper.InputArray [1]);
			horizontalPositive = jInput.GetAxis (Mapper.InputArray [2]);
			horizontalNegative = jInput.GetAxis (Mapper.InputArray [3]);
		}

		/*
				verticalPositive is Up move in jInputMappingSet
				verticalNegative is Down move in jInputMappingSet
				horizontalPositive is Right move in jInputMappingSet
				horizontalNegative is Left move in jInputMappingSet
				*/
	}

	#if (UNITY_EDITOR)
	void OnValidate ()
	{
		if (MenuItemAll == null)
			MenuItemAll = transform.Find ("InMapperMenuItems").gameObject;
		if (MenuItemAll != null)
			MenuItemAll.BroadcastMessage ("HeadingTextPour", SendMessageOptions.DontRequireReceiver);

		if (MenuItemHeadings.Length <= 0) {
			MenuItemHeadings = new string[1];
			MenuItemHeadings [0] = MinHeadingKeep;
		}
		HeadingGiveNumber ();

		if (MenuItemHeadings [0] != null && MenuItemHeadings.Length >= 1) {
			MinHeadingKeep = MenuItemHeadings [0];
		}
		if (MenuItemHeadings.Length > 31) {
			Array.Resize (ref MenuItemHeadings, 31);
			//List<string> InterimList = new List<string> (MenuItemHeadings);
			//MenuItemHeadings = InterimList.GetRange (0, 31).ToArray ();
		}
		if (MenuItemHeadings.Length < 1) {
			MenuItemHeadings = new string[1];
			if (MinHeadingKeep != null) {
				MenuItemHeadings [0] = MinHeadingKeep;
			}
		}
		
		if (DefaultInputArray2p == null || DefaultInputArray2p.Length == 0)
			DefaultInputArray2p = new string[DefaultInputArray.Length];
		if (DefaultInputArray3p == null || DefaultInputArray3p.Length == 0)
			DefaultInputArray3p = new string[DefaultInputArray.Length];
		if (DefaultInputArray4p == null || DefaultInputArray4p.Length == 0)
			DefaultInputArray4p = new string[DefaultInputArray.Length];
		
		if (DefaultInputArrayCopy == null || DefaultInputArrayCopy.Count == 0)
			DefaultInputArrayCopy = new List<string> (DefaultInputArray);
		if (DefaultInputArray2pCopy == null || DefaultInputArray2pCopy.Count == 0)
			DefaultInputArray2pCopy = new List<string> (DefaultInputArray2p);
		if (DefaultInputArray3pCopy == null || DefaultInputArray3pCopy.Count == 0)
			DefaultInputArray3pCopy = new List<string> (DefaultInputArray3p);
		if (DefaultInputArray4pCopy == null || DefaultInputArray4pCopy.Count == 0)
			DefaultInputArray4pCopy = new List<string> (DefaultInputArray4p);
		
		HeadDiff = MenuItemHeadings.Length - DefaultInputArrayCopy.Count;
		if (HeadDiff > 0 && MenuItemHeadings.Length < 32) {
			for (int i = 1; i <= HeadDiff; i++) {
				DefaultInputArrayCopy.Add ("");
			}
		}
		HeadDiff = MenuItemHeadings.Length - DefaultInputArray2pCopy.Count;
		if (HeadDiff > 0 && MenuItemHeadings.Length < 32) {
			for (int i = 1; i <= HeadDiff; i++) {
				DefaultInputArray2pCopy.Add ("");
			}
		}
		HeadDiff = MenuItemHeadings.Length - DefaultInputArray3pCopy.Count;
		if (HeadDiff > 0 && MenuItemHeadings.Length < 32) {
			for (int i = 1; i <= HeadDiff; i++) {
				DefaultInputArray3pCopy.Add ("");
			}
		}
		HeadDiff = MenuItemHeadings.Length - DefaultInputArray4pCopy.Count;
		if (HeadDiff > 0 && MenuItemHeadings.Length < 32) {
			for (int i = 1; i <= HeadDiff; i++) {
				DefaultInputArray4pCopy.Add ("");
			}
		}
		
		if (DefaultInputArray.Length != MenuItemHeadings.Length) {
			DefferentLengthArrayRenew ();
		}
		if (DefaultInputArray2p.Length != MenuItemHeadings.Length) {
			DefferentLengthArrayRenew2 ();
		}
		if (DefaultInputArray3p.Length != MenuItemHeadings.Length) {
			DefferentLengthArrayRenew3 ();
		}
		if (DefaultInputArray4p.Length != MenuItemHeadings.Length) {
			DefferentLengthArrayRenew4 ();
		}
		
	}
	public void DefferentLengthArrayRenew ()
	{
		DefaultInputArrayCopy.RemoveRange (0, DefaultInputArray.Length);
		DefaultInputArrayCopy.InsertRange (0, DefaultInputArray);
		DefaultInputArray = DefaultInputArrayCopy.GetRange (0, MenuItemHeadings.Length).ToArray ();
		ArrayCopyToSOData ();
	}
	public void DefferentLengthArrayRenew2 ()
	{
		DefaultInputArray2pCopy.RemoveRange (0, DefaultInputArray2p.Length);
		DefaultInputArray2pCopy.InsertRange (0, DefaultInputArray2p);
		DefaultInputArray2p = DefaultInputArray2pCopy.GetRange (0, MenuItemHeadings.Length).ToArray ();
		ArrayCopyToSOData2 ();
	}
	public void DefferentLengthArrayRenew3 ()
	{
		DefaultInputArray3pCopy.RemoveRange (0, DefaultInputArray3p.Length);
		DefaultInputArray3pCopy.InsertRange (0, DefaultInputArray3p);
		DefaultInputArray3p = DefaultInputArray3pCopy.GetRange (0, MenuItemHeadings.Length).ToArray ();
		ArrayCopyToSOData3 ();
	}
	public void DefferentLengthArrayRenew4 ()
	{
		DefaultInputArray4pCopy.RemoveRange (0, DefaultInputArray4p.Length);
		DefaultInputArray4pCopy.InsertRange (0, DefaultInputArray4p);
		DefaultInputArray4p = DefaultInputArray4pCopy.GetRange (0, MenuItemHeadings.Length).ToArray ();
		ArrayCopyToSOData4 ();
	}
	public void ArrayCopyToSOData ()
	{
		jInputSOData.DefaultInputArray.Clear ();
		jInputSOData.DefaultInputArray.AddRange (DefaultInputArray);
	}
	public void ArrayCopyToSOData2 ()
	{
		jInputSOData.DefaultInputArray2p.Clear ();
		jInputSOData.DefaultInputArray2p.AddRange (DefaultInputArray2p);
	}
	public void ArrayCopyToSOData3 ()
	{
		jInputSOData.DefaultInputArray3p.Clear ();
		jInputSOData.DefaultInputArray3p.AddRange (DefaultInputArray3p);
	}
	public void ArrayCopyToSOData4 ()
	{
		jInputSOData.DefaultInputArray4p.Clear ();
		jInputSOData.DefaultInputArray4p.AddRange (DefaultInputArray4p);
	}
	public void PlayerNumToSOData ()
	{
		jInputSOData.PlayerNum = PlayerNum;
	}
	public void DropdownToList ()
	{
		ExcludeNum = new List<int> ();
		for (int i = 0; i < MenuItemHeadings.Length; i++) { //この方法は32を周期にループしてしまうので0がチェックされると32,64…も同様になることに注意
			int DecisionNum = 1 << i;
			if ((flags & DecisionNum) != 0) {//Inspectorのドロップダウンで選択されている項目だけ何かする
				ExcludeNum.Add (i);
			}
		}
	}
	void HeadingGiveNumber ()
	{
		MenuItemHeadingsCopy = new string[MenuItemHeadings.Length];
		for (int i = 0; i < MenuItemHeadingsCopy.Length; i++) {
			MenuItemHeadingsCopy [i] = "E" + i + ": " + MenuItemHeadings [i];
		}
	}
	public void DefaultArrayCopyReset ()
	{
		DefaultInputArrayCopy = new List<string> (DefaultInputArray);
		DefaultInputArray2pCopy = new List<string> (DefaultInputArray2p);
		DefaultInputArray3pCopy = new List<string> (DefaultInputArray3p);
		DefaultInputArray4pCopy = new List<string> (DefaultInputArray4p);
	}
	public void AxesAdvanceToSOData ()
	{
		jInputSOData.DeadZone = DeadZone;
		jInputSOData.Gravity = Gravity;
		jInputSOData.Sensitivity = Sensitivity;
	}
	#endif
	
	void InputSetting (string MappingName)
	{
		if (MappingName != null) {
			if (PlayerSelectNum == 2) {
				TemporaryInputArray2p [SelectPosition] = MappingName;
			} else if (PlayerSelectNum == 3) {
				TemporaryInputArray3p [SelectPosition] = MappingName;
			} else if (PlayerSelectNum == 4) {
				TemporaryInputArray4p [SelectPosition] = MappingName;
			} else {
				TemporaryInputArray [SelectPosition] = MappingName;
			}
		} else {
			Debug.LogError ("[jInput] Error!! Mapping Name Not Found!!");
		}
		CurrentryRestore = false;
		SameSettingCheck ();
	}

	void SameSettingCheck ()
	{
		foreach (Transform Child in MenuItemAll.transform) {
			Child.GetComponent<SelectOrnament> ().DuplicationTextColor = false;
		}
		for (int i = 0; i < MaxMenuSize-1; i++) { //change text color when same setting in oneself
			CurrentlyItem = MenuItemAll.transform.GetChild (i).gameObject;
			CurrentlyItemScript = CurrentlyItem.GetComponent<SelectOrnament> ();
			for (int j = i+1; j < MaxMenuSize; j++) {
				OtherItem = MenuItemAll.transform.GetChild (j).gameObject;
				OtherItemScript = OtherItem.GetComponent<SelectOrnament> ();
				if (CurrentlyItem.transform.Find ("TextPrefab").GetComponent<TextMesh> ().text == OtherItem.transform.Find ("TextPrefab").GetComponent<TextMesh> ().text) {
					CurrentlyItemScript.DuplicationTextColor = true;
					OtherItemScript.DuplicationTextColor = true;
				}
			}
		}
		if (PlayerNum != 1) { //put AlertMark when same setting as others
			for (int k = 1; k <= PlayerNum; k++) {
				if (PlayerSelectNum != k) {
					ProvisionalArray = new string[Mapper.InputArray.Length];
					switch (k) {
					case 1:
						Mapper.InputArray.CopyTo (ProvisionalArray, 0);
						break;
					case 2:
						Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
						break;
					case 3:
						Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
						break;
					case 4:
						Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
						break;
					}
					for (int i = 0; i < MaxMenuSize; i++) {
						CurrentlyItem = MenuItemAll.transform.GetChild (i).gameObject;
						CurrentlyItemScript = CurrentlyItem.GetComponent<SelectOrnament> ();
						for (int j =0; j < ProvisionalArray.Length; j++) {
							if (CurrentlyItem.transform.Find ("TextPrefab").GetComponent<TextMesh> ().text != ProvisionalArray [j]) {
								CurrentlyItemScript.AlertMarkDisplaying = false;
							} else {
								CurrentlyItemScript.AlertMarkDisplaying = true;
								break;
							}
						}
					}
				}
			}
		}
		if (JoystickNum != 0) {
			OtherJoystickNumMingleCheck ();
		}
	}

	void OtherJoystickNumMingleCheck ()
	{
		for (int i = 0; i < MaxMenuSize; i++) {
			CurrentlyItem = MenuItemAll.transform.GetChild (i).gameObject;
			CurrentlyItemScript = CurrentlyItem.GetComponent<SelectOrnament> ();
			OtherMingleCheckText = CurrentlyItem.transform.Find ("TextPrefab").GetComponent<TextMesh> ().text;
			if (OtherMingleCheckText.Length > 8) {
				if (OtherMingleCheckText.IndexOf ("Joystick") == 0) {
					OtherMingleJoystickNum = int.Parse (OtherMingleCheckText.Substring (8, 1));
					if (JoystickNum != OtherMingleJoystickNum && OtherMingleJoystickNum != 0) {
						CurrentlyItemScript.AlertMarkDisplaying = true;
					}
				}
			}
		}
		OtherMingleCheckText = null;
		OtherMingleJoystickNum = 0;
	}

	void SaveConfirmation ()
	{
		if (CurrentryRestore != true) {
			SaveNoSelectPosition = false;
			SaveConfirm.SetActive (true);
			SavingSituation = true;
		}
	}

	void SaveProsess ()
	{
		if (MapperScript == null) {
			if (MapperScript = GameObject.Find ("jInputMappingManager").GetComponent<Mapper> ()) {

			} else {
				Debug.LogError ("[jInput] Error!! jInputMappingManager not found!!");
				Debug.LogError ("[jInput] Input Mapping Data Save failure!");
				SavefailureIndicate ();
				return;
			}
		}
		if (MapperScript != null) {
			if (PlayerSelectNum == 2) {
				TemporaryInputArray2p.CopyTo (Mapper.InputArray2p, 0);
				TemporaryInputArray2p.CopyTo (MapperScript.AllJoinInputArray, InputLength);
			} else if (PlayerSelectNum == 3) {
				TemporaryInputArray3p.CopyTo (Mapper.InputArray3p, 0);
				TemporaryInputArray3p.CopyTo (MapperScript.AllJoinInputArray, InputLength * 2);
			} else if (PlayerSelectNum == 4) {
				TemporaryInputArray4p.CopyTo (Mapper.InputArray4p, 0);
				TemporaryInputArray4p.CopyTo (MapperScript.AllJoinInputArray, InputLength * 3);
			} else {
				TemporaryInputArray.CopyTo (Mapper.InputArray, 0);
				TemporaryInputArray.CopyTo (MapperScript.AllJoinInputArray, 0);
			}
			MapperScript.SaveData ();

			CurrentryRestore = true;
			SaveConfirm.SetActive (false);
			SavingSituation = false;
			OperateLineSelectPosition = 2;
			SelectPosition = 0;
			OperateItemLine = false;
		}
	}

	void RestorePrevious ()
	{
		if (CurrentryRestore != true) {
			if (MapperScript == null) {
				if (MapperScript = GameObject.Find ("jInputMappingManager").GetComponent<Mapper> ()) {
					
				} else {
					Debug.LogError ("[jInput] Error!! jInputMappingManager not found!!");
					Debug.LogError ("[jInput] Input Mapping Restore Previous failure!");
					return;
				}
			}

			MapperScript.LoadData ();
			ProvisionalArray = new string[Mapper.InputArray.Length];
			switch (PlayerSelectNum) {
			case 1:
				Mapper.InputArray.CopyTo (ProvisionalArray, 0);
				break;
			case 2:
				Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
				break;
			case 3:
				Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
				break;
			case 4:
				Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
				break;
			}
			for (int i = 0; i < MaxMenuSize; i++) {
				if (0 <= i && i <= 9) {
					if (MenuItemAll.transform.Find ("MapperMenuItem0" + i + "/TextPrefab").GetComponent<TextMesh> ().text != null) {
						MenuItemAll.transform.Find ("MapperMenuItem0" + i + "/TextPrefab").GetComponent<TextMesh> ().text = ProvisionalArray [i];
					} else {
						Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
					}
				} else if (10 <= i && i <= 30) {
					if (MenuItemAll.transform.Find ("MapperMenuItem" + i + "/TextPrefab").GetComponent<TextMesh> ().text != null) {
						MenuItemAll.transform.Find ("MapperMenuItem" + i + "/TextPrefab").GetComponent<TextMesh> ().text = ProvisionalArray [i];
					} else {
						Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
					}
				} else {
					Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
				}
			}
			CurrentryRestore = true;
			OperateLineSelectPosition = 2;
			SelectPosition = 0;
			OperateItemLine = false;
		}
		SameSettingCheck ();
	}

	void DefaultInputSet ()
	{
		ProvisionalArray = new string[DefaultInputArray.Length];
		switch (PlayerSelectNum) {
		case 1:
			DefaultInputArray.CopyTo (ProvisionalArray, 0);
			DefaultInputArray.CopyTo (TemporaryInputArray, 0);
			break;
		case 2:
			DefaultInputArray2p.CopyTo (ProvisionalArray, 0);
			DefaultInputArray2p.CopyTo (TemporaryInputArray2p, 0);
			break;
		case 3:
			DefaultInputArray3p.CopyTo (ProvisionalArray, 0);
			DefaultInputArray3p.CopyTo (TemporaryInputArray3p, 0);
			break;
		case 4:
			DefaultInputArray4p.CopyTo (ProvisionalArray, 0);
			DefaultInputArray4p.CopyTo (TemporaryInputArray4p, 0);
			break;
		}
		for (int i = 0; i < MaxMenuSize; i++) {
			if (0 <= i && i <= 9) {
				if (MenuItemAll.transform.Find ("MapperMenuItem0" + i + "/TextPrefab").GetComponent<TextMesh> ().text != null) {
					MenuItemAll.transform.Find ("MapperMenuItem0" + i + "/TextPrefab").GetComponent<TextMesh> ().text = ProvisionalArray [i];
				} else {
					Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
				}
			} else if (10 <= i && i <= 30) {
				if (MenuItemAll.transform.Find ("MapperMenuItem" + i + "/TextPrefab").GetComponent<TextMesh> ().text != null) {
					MenuItemAll.transform.Find ("MapperMenuItem" + i + "/TextPrefab").GetComponent<TextMesh> ().text = ProvisionalArray [i];
				} else {
					Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
				}
			} else {
				Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
			}
		}
		CurrentryRestore = false;
		SameSettingCheck ();
	}

	public void ResetSet ()
	{
		if (SaveConfirm != null)
			SaveConfirm.SetActive (true);
		if (ExitConfirm != null)
			ExitConfirm.SetActive (true);
		if (SaveSucsess != null)
			SaveSucsess.SetActive (true);
		if (Savefailure != null)
			Savefailure.SetActive (true);
		if (PlayerNumTextObject != null)
			PlayerNumTextObject.SetActive (true);
		if (PlayerNumWindow != null)
			PlayerNumWindow.SetActive (true);
		if (MenuItemAll != null)
			MenuItemAll.SetActive (true);
		if (OperateItemAll != null)
			OperateItemAll.SetActive (true);

		#if (UNITY_EDITOR)
				
		#else
				Start ();
		#endif
	}

	public void SaveSucsessIndicate ()
	{
		if (SaveSucsess != null)
			SaveSucsess.SetActive (true);
	}
	public void SavefailureIndicate ()
	{
		if (Savefailure != null)
			Savefailure.SetActive (true);
	}

	void TidyMenuItemText ()
	{
		try {
			ProvisionalArray = new string[Mapper.InputArray.Length];
			switch (PlayerSelectNum) {
			case 1:
				Mapper.InputArray.CopyTo (ProvisionalArray, 0);
				Mapper.InputArray.CopyTo (TemporaryInputArray, 0);
				break;
			case 2:
				Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
				Mapper.InputArray2p.CopyTo (TemporaryInputArray2p, 0);
				break;
			case 3:
				Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
				Mapper.InputArray3p.CopyTo (TemporaryInputArray3p, 0);
				break;
			case 4:
				Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
				Mapper.InputArray4p.CopyTo (TemporaryInputArray4p, 0);
				break;
			}
			for (int i = 0; i < MaxMenuSize; i++) {
				if (0 <= i && i <= 9) {
					if (MenuItemAll.transform.Find ("MapperMenuItem0" + i + "/TextPrefab").GetComponent<TextMesh> ().text != null) {
						MenuItemAll.transform.Find ("MapperMenuItem0" + i + "/TextPrefab").GetComponent<TextMesh> ().text = ProvisionalArray [i];
					} else {
						Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
					}
				} else if (10 <= i && i <= 30) {
					if (MenuItemAll.transform.Find ("MapperMenuItem" + i + "/TextPrefab").GetComponent<TextMesh> ().text != null) {
						MenuItemAll.transform.Find ("MapperMenuItem" + i + "/TextPrefab").GetComponent<TextMesh> ().text = ProvisionalArray [i];
					} else {
						Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
					}
				} else {
					Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
				}
			}
			CurrentryRestore = true;
			SameSettingCheck ();
		} catch (ArgumentException) {
			Debug.LogError ("[jInput] Error!! It failed to operate of open jInput Mapping window.");
		}
	}

	#if (UNITY_EDITOR)
	void InputNameRegularlyCompare ()
	{
		InputCompareList = new List<string> ();

		for (int i = 0; i < KeyValues.Length; i++) {
			InputCompareList.Add (KeyValues.GetValue (i).ToString ());
		}
		for (int i = 1; i <= 9; i++) {
			for (int j = 1; j <= 20; j++) {
				InputCompareList.Add ("Joystick" + (i) + "Axis" + (j) + "+");
				InputCompareList.Add ("Joystick" + (i) + "Axis" + (j) + "-");
			}
		}
		InputCompareList.Add ("MouseWheel+");
		InputCompareList.Add ("MouseWheel-");
		InputCompareList.Add (""); //未設定は別メッセージにするため適合するようにしておく

		jInputSOData.DefaKeyInconsistencyListText = "";
		bool NotSetDefaKey2p = false;
		bool NotSetDefaKey3p = false;
		bool NotSetDefaKey4p = false;
		ProvisionalArray = new string[DefaultInputArray.Length];
		for (int k=1; k<=PlayerNum; k++) {
			switch (k) {
			case 1:
				DefaultInputArray.CopyTo (ProvisionalArray, 0);
				break;
			case 2:
				DefaultInputArray2p.CopyTo (ProvisionalArray, 0);
				break;
			case 3:
				DefaultInputArray3p.CopyTo (ProvisionalArray, 0);
				break;
			case 4:
				DefaultInputArray4p.CopyTo (ProvisionalArray, 0);
				break;
			}
			for (int x=0; x<ProvisionalArray.Length; x++) {
				if (ProvisionalArray [x] == "") {
					switch (k) {
					case 1:
						if (PlayerNum == 1) {
							Debug.LogError ("[jInput] Default Input Mapping have not been set!! (Elements " + x + ")");
							jInputSOData.DefaKeyInconsistencyListText += " Not Set (" + MenuItemHeadings [x] + ")\n";
						} else {
							Debug.LogError ("[jInput] Default Input Mapping have not been set!! Player1P (Elements " + x + ")");
							jInputSOData.DefaKeyInconsistencyListText += " -Player1P: Not Set (" + MenuItemHeadings [x] + ")\n";
						}
						jInputSOData.DefaKey1pFoldoutBool = true;
						jInputSOData.DefaKeyFoldoutBool = true;
						jInputSOData.SOValueDetermine ();
						Debug.Break ();
						jInputSOData.DefaultInputNameInconsistencyCheck = true;
						Mapper.EditorPlaymodeStop = true;
						DefaultInputNameUnsuitableCheck = true;
						MapperScript.SaveFileDelete ();
						break;
					case 2:
						if (NotSetDefaKey2p != true) {
							Debug.LogWarning ("[jInput] Default Input Mapping have not been set! Player2P");
							NotSetDefaKey2p = true;
						}
						break;
					case 3:
						if (NotSetDefaKey3p != true) {
							Debug.LogWarning ("[jInput] Default Input Mapping have not been set! Player3P");
							NotSetDefaKey3p = true;
						}
						break;
					case 4:
						if (NotSetDefaKey4p != true) {
							Debug.LogWarning ("[jInput] Default Input Mapping have not been set! Player4P");
							NotSetDefaKey4p = true;
						}
						break;
					}

				}
				if (InputCompareList.IndexOf (ProvisionalArray [x]) == -1) {
					if (PlayerNum == 1) {
						jInputSOData.DefaKey1pFoldoutBool = true;
						Debug.LogError ("[jInput] Default Input Mapping have Unsuitable Input Name!!\n              '" + ProvisionalArray [x] + "'  (Elements " + x + ")");
						jInputSOData.DefaKeyInconsistencyListText += " '" + ProvisionalArray [x] + "' (" + MenuItemHeadings [x] + ")\n";
					} else {
						switch (k) {
						case 1:
							jInputSOData.DefaKey1pFoldoutBool = true;
							break;
						case 2:
							jInputSOData.DefaKey2pFoldoutBool = true;
							break;
						case 3:
							jInputSOData.DefaKey3pFoldoutBool = true;
							break;
						case 4:
							jInputSOData.DefaKey4pFoldoutBool = true;
							break;
						}
						Debug.LogError ("[jInput] Default Input Mapping have Unsuitable Input Name!!\n             Player " + k + "P: '" + ProvisionalArray [x] + "'  (Elements " + x + ")");
						jInputSOData.DefaKeyInconsistencyListText += " -Player" + k + "P: '" + ProvisionalArray [x] + "' (" + MenuItemHeadings [x] + ")\n";
					}
					jInputSOData.DefaKeyFoldoutBool = true;
					jInputSOData.SOValueDetermine ();
					Debug.Break ();
					jInputSOData.DefaultInputNameInconsistencyCheck = true;
					Mapper.EditorPlaymodeStop = true;
					DefaultInputNameUnsuitableCheck = true;
				}
				if (Array.IndexOf (ImpossibleMappingKeyArray, ProvisionalArray [x]) != -1) {
					if (PlayerNum == 1) {
						jInputSOData.DefaKey1pFoldoutBool = true;
						Debug.LogError ("[jInput] Default Input Mapping have the impossible mapping key!\n              '" + ProvisionalArray [x] + "'  (Elements " + x + ")");
					} else {
						switch (k) {
						case 1:
							jInputSOData.DefaKey1pFoldoutBool = true;
							break;
						case 2:
							jInputSOData.DefaKey2pFoldoutBool = true;
							break;
						case 3:
							jInputSOData.DefaKey3pFoldoutBool = true;
							break;
						case 4:
							jInputSOData.DefaKey4pFoldoutBool = true;
							break;
						}
						Debug.LogError ("[jInput] Default Input Mapping have the impossible mapping key!\n             Player " + k + "P: '" + ProvisionalArray [x] + "'  (Elements " + x + ")");
					}
					jInputSOData.DefaKeyFoldoutBool = true;
					jInputSOData.SOValueDetermine ();
					Debug.Break ();
					jInputSOData.DefaultInputNameInconsistencyCheck = true;
					Mapper.EditorPlaymodeStop = true;
					DefaultInputNameUnsuitableCheck = true;
				}
			}
		}
		if (DefaultInputNameUnsuitableCheck) {
			//MapperScript.SaveFileDelete (); //1P未設定以外の不適合名はjInput.csのほうからこの処理を行っている
		} else {
			jInputSOData.PlayerNum = PlayerNum;
			jInputSOData.DefaultInputArray.Clear ();
			jInputSOData.DefaultInputArray2p.Clear ();
			jInputSOData.DefaultInputArray3p.Clear ();
			jInputSOData.DefaultInputArray4p.Clear ();
			jInputSOData.DefaultInputArray.InsertRange (0, DefaultInputArray);
			if (PlayerNum >= 2) {
				jInputSOData.DefaultInputArray2p.InsertRange (0, DefaultInputArray2p);
			}
			if (PlayerNum >= 3) {
				jInputSOData.DefaultInputArray3p.InsertRange (0, DefaultInputArray3p);
			}
			if (PlayerNum >= 4) {
				jInputSOData.DefaultInputArray4p.InsertRange (0, DefaultInputArray4p);
			}
			jInputSOData.DefaultInputNameInconsistencyCheck = false;
			AxesAdvanceToSOData ();
			jInputSOData.AxesSetApply ();
		}
	}
	#endif
	
	void Start ()
	{
		if (MapperScript = GameObject.Find ("jInputMappingManager").GetComponent<Mapper> ()) {
			if (Mapper.SetScript != this || Mapper.SetScript == null) {
				Mapper.SetScript = this;
			}
		} else {
			Debug.LogError ("[jInput] Error!! jInputMappingManager not found!!");
		}
		MappingWindowDestroyOn = false;
		SavingSituation = false;
		ExitSituation = false;
		OperateItemLine = false;
		CurrentryRestore = true;
		CloseButtonSelect = false;
		JoystickNumString = null;
		SelectPosition = 0;
		JoystickNum = 0;
		MappingMode = false;
		PlayerSelectNum = 1;
		CloseButtonSelect = false;
		if (PlayerNum < 1 || 4 < PlayerNum) {
			PlayerNum = 4;
		}
		DefaultInputNameUnsuitableCheck = false;
		KeyValues = Enum.GetValues (typeof(KeyCode));
		#if (UNITY_EDITOR)
		InputNameRegularlyCompare ();
		#endif
		if (DefaultInputNameUnsuitableCheck != false) {
			return;
		}
		InputLength = DefaultInputArray.Length;
		if (InputLength <= 0) {
			Debug.LogError ("[jInput] Error!! To verify jInput Settings and re-create the input mapping data!!");
			Debug.Break ();
			#if (UNITY_EDITOR)
			Mapper.EditorPlaymodeStop = true;
			return;
			#else
						return;
			#endif
		}
		TemporaryInputArray = new string[InputLength];
		DefaultInputArray.CopyTo (TemporaryInputArray, 0);
		if (PlayerNum >= 2) {
			TemporaryInputArray2p = new string[InputLength];
			DefaultInputArray2p.CopyTo (TemporaryInputArray2p, 0);
		}
		if (PlayerNum >= 3) {
			TemporaryInputArray3p = new string[InputLength];
			DefaultInputArray3p.CopyTo (TemporaryInputArray3p, 0);
		}
		if (PlayerNum >= 4) {
			TemporaryInputArray4p = new string[InputLength];
			DefaultInputArray4p.CopyTo (TemporaryInputArray4p, 0);
		}

		jCamera = transform.Find ("jInputCamera").GetComponent<Camera> ();
		if (jCamera == null) {
			Debug.LogError ("[jInput] Error!! jInputCamera not found!!");
		}
		Selection = transform.Find ("Selection").gameObject;
		if (Selection) {
			Selection.SetActive (false);
		} else {
			Debug.LogError ("[jInput] Error!! Selection not found!!");
		}
		SaveConfirm = transform.Find ("ConfirmWindow").gameObject;
		if (SaveConfirm) {
			SaveConfirm.SetActive (false);
		} else {
			Debug.LogError ("[jInput] Error!! ConfirmWindow not found!!");
		}
		ExitConfirm = transform.Find ("ExitWindow").gameObject;
		if (ExitConfirm != null) {
			ExitConfirm.SetActive (false);
		} else {
			Debug.LogError ("[jInput] Error!! ExitWindow not found!!");
		}
		SaveSucsess = transform.Find ("SaveResultTexts/SaveSucsessText").gameObject;
		if (SaveSucsess != null) {
			SaveSucsess.SetActive (false);
		}
		Savefailure = transform.Find ("SaveResultTexts/SavefailureText").gameObject;
		if (Savefailure != null) {
			Savefailure.SetActive (false);
		}
		MenuItemAll = transform.Find ("InMapperMenuItems").gameObject;
		if (MenuItemAll == null) {
			Debug.LogError ("[jInput] Error!! InMapperMenuItems not found!!");
		} else {
			if (InputLength != MenuItemAll.transform.childCount) {
				Debug.LogError ("[jInput] Error!! There is an unnecessary thing in InMapperMenuItems!");
			} else {
				MaxMenuSize = InputLength;
			}
			MenuItemAll.SetActive (false);
		}
		PlayerNumWindow = transform.Find ("PlayerNumWindow").gameObject;
		if (PlayerNumWindow == null && PlayerNum != 1) {
			Debug.LogError ("[jInput] Error!! PlayerNumWindow not found!!");
		} else {
			if (PlayerNumWindow.transform.Find ("CloseButton") == null || PlayerNumWindow.transform.FindChild ("CloseButton").gameObject.activeSelf == false) {
				NothingCloseButtonBool = true;
			}
			if (PlayerNum == 1) {
				PlayerNumSituation = false;
			} else {
				PlayerNumSituation = true;
			}
			PlayerNumWindow.SetActive (false);
		}
		PlayerNumTextObject = transform.Find ("PlayerSelectNumText/TextPrefab").gameObject;
		if (PlayerNumTextObject != null) {
			PlayerNumTextMesh = PlayerNumTextObject.GetComponent<TextMesh> ();
			PlayerNumTextObject.SetActive (false);
		} else {
			Debug.LogError ("[jInput] Error!! PlayerSelectNumText not found!!");
		}
		OperateItemAll = transform.Find ("InMapperOperateItems").gameObject;
		if (OperateItemAll != null) {
			if (OperateItemAll.transform.Find ("MapperOperateItem03") == null) {
				MaxOperateMenuSize = OperateItemAll.transform.childCount - 1;
			} else if (OperateItemAll.transform.Find ("MapperOperateItem03").gameObject.activeSelf == false) { //Because transform.Find can find the object of SetActive(false);
				MaxOperateMenuSize = OperateItemAll.transform.childCount - 1;
			} else {
				MaxOperateMenuSize = OperateItemAll.transform.childCount;
			}
			OperateItemAll.SetActive (false);
		} else {
			Debug.LogError ("[jInput] Error!! InMapperOperateItems not found!!");
		}
		if (OpenCloseMethodNum == 1 || OpenCloseMethodNum == 2) {
			MappingWindowPrepare ();
		} else {
			OpenCloseMethodNum = 0;
			gameObject.SetActive (false);
		}
	}

	void Update ()
	{
		if (AxisDelay) {
			AxisDelayTimer += Time.deltaTime;
			if (AxisDelayTimer > 0.25f) {
				AxisDelay = false;
				AxisDelayTimer = 0;
			}
		}
		if (KeyDelay) {
			KeyDelayTimer += Time.deltaTime;
			if (KeyDelayTimer > 0.17f) {
				KeyDelay = false;
				KeyDelayTimer = 0;
			}
		}

		vhRenew ();
		if (verticalPositive > 0.99 && verticalNegative > 0.99) {
			v = 1; //To prevent cannot move the carsor because of same mapping Up and Down.
		} else {
			v = verticalPositive - verticalNegative;
		}
			
		if (horizontalPositive > 0.99 && horizontalNegative > 0.99) {
			h = 1; //To prevent cannot move the carsor because of same mapping Right and Left.
		} else {
			h = horizontalPositive - horizontalNegative;
		}

		if (SavingSituation) {
			if (SaveNoSelectPosition) {
				Selection.transform.position = SaveConfirm.transform.position + SaveConfirm.transform.right * 3.13f + SaveConfirm.transform.up * 1.02f + SaveConfirm.transform.forward * -0.1f;
			} else {
				Selection.transform.position = SaveConfirm.transform.position + SaveConfirm.transform.right * -2.88f + SaveConfirm.transform.up * 1.02f + SaveConfirm.transform.forward * -0.1f;
			}
			if (Selection.activeSelf != true) {
				Selection.SetActive (true);
			}
		} else if (ExitSituation) {
			if (ExitSelectPosition == 0) {
				Selection.transform.position = ExitConfirm.transform.position + ExitConfirm.transform.right * - 5.1f + ExitConfirm.transform.up * 1.02f + ExitConfirm.transform.forward * -0.1f;
			} else if (ExitSelectPosition == 1) {
				Selection.transform.position = ExitConfirm.transform.position + ExitConfirm.transform.right * 0.07f + ExitConfirm.transform.up * 1.02f + ExitConfirm.transform.forward * -0.1f;
			} else if (ExitSelectPosition == 2) {
				Selection.transform.position = ExitConfirm.transform.position + ExitConfirm.transform.right * 5.1f + ExitConfirm.transform.up * 1.02f + ExitConfirm.transform.forward * -0.1f;
			} else {
				ExitSelectPosition = 0;
			}
			if (Selection.activeSelf != true) {
				Selection.SetActive (true);
			}
		} else if (PlayerNumSituation && gameObject.activeSelf != false) {
			switch (PlayerNum) {
			case 1:

				break;
			case 2:
				if (PlayerSelectNum == 1)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * - 2.5f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				if (PlayerSelectNum == 2)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 2.5f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				break;
			case 3:
				if (PlayerSelectNum == 1)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -3.15f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				if (PlayerSelectNum == 2)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 0f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				if (PlayerSelectNum == 3)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 3.15f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				break;
			case 4:
				if (PlayerSelectNum == 1)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -4.18f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				if (PlayerSelectNum == 2)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * -1.39f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				if (PlayerSelectNum == 3)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 1.39f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				if (PlayerSelectNum == 4)
					Selection.transform.position = PlayerNumWindow.transform.position + PlayerNumWindow.transform.right * 4.18f + PlayerNumWindow.transform.up * 0.45f + PlayerNumWindow.transform.forward * -0.1f;
				break;
			}
			if (Selection.activeSelf != true && PlayerNum != 1) {
				Selection.SetActive (true);
			}
		} else {
			if (Selection != null && Selection.activeSelf != false) {
				Selection.SetActive (false);
			}
		}
				
		if (AxisDelay != true) {
			if (SavingSituation) {
				if (h < -0.2 || 0.2 < h) {
					SaveNoSelectPosition = !SaveNoSelectPosition;
					AxisDelay = true;
				}
			} else if (ExitSituation) {
				if (h < -0.2) {
					ExitSelectPosition--;
					AxisDelay = true;
				}
				if (0.2 < h) {
					ExitSelectPosition++;
					AxisDelay = true;
				}
			} else if (PlayerNumSituation && gameObject.activeSelf != false) {
				if (v < -0.2 || v > 0.2) {
					if (NothingCloseButtonBool != true) {
						if (CloseButtonSelect != true) {
							CloseButtonSelect = true;
						} else {
							CloseButtonSelect = false;
						}
						AxisDelay = true;
					}
				}
				if (CloseButtonSelect != true) {
					if (h < -0.2) {
						PlayerSelectNum--;
						AxisDelay = true;
					}
					if (0.2 < h) {
						PlayerSelectNum++;
						AxisDelay = true;
					}
				}
			} else {
				if (MappingMode != true) {
					if (h < -0.2 || 0.2 < h) {
						if (FirstSet) {
							SelectPosition = 0;
							FirstSet = false;
						} else {
							OperateItemLine = !OperateItemLine;
							if (CurrentryRestore) {
								OperateLineSelectPosition = 2;
							} else {
								OperateLineSelectPosition = 0;
							}
						}
						AxisDelay = true;
					}
					if (OperateItemLine != true) {
						if (v < -0.2) {
							if (FirstSet) {
								SelectPosition = 0;
								FirstSet = false;
							} else {
								SelectPosition++;
							}
							AxisDelay = true;
						} else if (v > 0.2) {
							if (FirstSet) {
								SelectPosition = 0;
								FirstSet = false;
							} else {
								SelectPosition--;
							}
							AxisDelay = true;
						}
					} else {
						if (CurrentryRestore != true) {
							if (v < -0.2) {
								OperateLineSelectPosition++;
								AxisDelay = true;
							} else if (v > 0.2) {
								OperateLineSelectPosition--;
								AxisDelay = true;
							}
						} else {
							if (v < -0.2 || v > 0.2) {
								if (MaxOperateMenuSize >= 4) {
									if (OperateLineSelectPosition == 2) {
										OperateLineSelectPosition = 3;
										AxisDelay = true;
									} else {
										OperateLineSelectPosition = 2;
										AxisDelay = true;
									}
								}
							}
						}
					}
				}
			}
		}


		if (SelectPosition < 0) {
			SelectPosition = MaxMenuSize - 1;
		}
		if (SelectPosition > MaxMenuSize - 1) {
			SelectPosition = 0;
		}
		if (OperateLineSelectPosition < 0) {
			OperateLineSelectPosition = MaxOperateMenuSize - 1;
		}
		if (OperateLineSelectPosition > MaxOperateMenuSize - 1) {
			OperateLineSelectPosition = 0;
		}
		if (ExitSelectPosition < 0) {
			ExitSelectPosition = 2;
		}
		if (ExitSelectPosition > 2) {
			ExitSelectPosition = 0;
		}
		if (PlayerSelectNum < 1) {
			PlayerSelectNum = PlayerNum;
		}
		if (PlayerSelectNum > PlayerNum) {
			PlayerSelectNum = 1;
		}

		if (DefaultInputNameUnsuitableCheck != true) {
			if (PlayerNumSituation != true && gameObject.activeSelf != false) {
				PlayerNumWindow.SetActive (false);
				MenuItemAll.SetActive (true);
				OperateItemAll.SetActive (true);
				if (PlayerNum != 1) {
					switch (PlayerSelectNum) {
					case 1:
						PlayerNumTextMesh.text = "Player1";
						break;
					case 2:
						PlayerNumTextMesh.text = "Player2";
						break;
					case 3:
						PlayerNumTextMesh.text = "Player3";
						break;
					case 4:
						PlayerNumTextMesh.text = "Player4";
						break;
					}
				}
			} else {
				PlayerNumTextMesh.text = "";
				PlayerNumTextObject.SetActive (false);
				if (PlayerNum == 1) {
					PlayerNumSituation = false;
					PlayerNumWindow.SetActive (false);
					MenuItemAll.SetActive (true);
					OperateItemAll.SetActive (true);
				} else {
					MenuItemAll.SetActive (false);
					OperateItemAll.SetActive (false);
					PlayerNumWindow.SetActive (true);
				}
			}
		}

		if (FirstSet) {
			OperateItemLine = false;
			CurrentryRestore = true;
			SelectPosition = 0;
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = jCamera.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (PlayerNumSituation && gameObject.activeSelf != false) {
					if (hit.collider.name == "Player1MappingNum" || hit.collider.name == "Player1MappingNum" || hit.collider.name == "Player2MappingNum" || hit.collider.name == "Player3MappingNum" || hit.collider.name == "Player4MappingNum") {
						PlayerMappingNumScript = hit.collider.gameObject.GetComponent<PlayerMappingNumHold> ();
						if (PlayerMappingNumScript != null) {
							PlayerMappingNumber = PlayerMappingNumScript.PlayerMappingNumber;
							switch (PlayerMappingNumber) {
							case 1:
								PlayerSelectNum = 1;
								break;
							case 2:
								PlayerSelectNum = 2;
								break;
							case 3:
								PlayerSelectNum = 3;
								break;
							case 4:
								PlayerSelectNum = 4;
								break;
							}
						}
						TidyMenuItemText ();
						PlayerNumTextObject.SetActive (true);
						FirstSet = true;
						KeyDelay = true;
						AxisDelay = true;
						CloseButtonSelect = false;
						OperateItemLine = false;
						CurrentryRestore = true;
						SelectPosition = 0;
						PlayerNumSituation = false;
					}
					if (hit.collider.name == "CloseButton") {
						if (OpenCloseMethodNum == 0) {
							gameObject.SetActive (false);
							PlayerSelectNum = 1;
							CloseButtonSelect = false;
						} else if (OpenCloseMethodNum == 2) {
							Destroy (gameObject);
						} else {
							MappingWindowCloseByButton ();
						}
					}
				} else {
					if (SavingSituation) {
						if (hit.collider.name == "YesButton") {
							SaveProsess ();
						}
						if (hit.collider.name == "NoButton") {
							SaveConfirm.SetActive (false);
							SavingSituation = false;
						}
					} else if (ExitSituation) {
						if (hit.collider.name == "SaveButton") {
							SaveProsess ();
							if (PlayerNum == 1) {
								if (MappingWindowDestroyOn || OpenCloseMethodNum == 2) {
									Destroy (gameObject);
									MappingWindowDestroyOn = false;
								} else {
									if (OpenCloseMethodNum == 0) {
										gameObject.SetActive (false);
									} else {
										MappingWindowCloseByButton ();
									}
									ExitConfirm.SetActive (false);
									ExitSituation = false;
								}
								if (CloseMappingDelegate != null) {
									CloseMappingDelegate ();
									CloseMappingDelegate = null;
								}
							} else {
								ExitConfirm.SetActive (false);
								ExitSituation = false;
								PlayerNumSituation = true;
								JoystickNum = 0;
							}
							KeyDelay = true;
							AxisDelay = true;
						}
						if (hit.collider.name == "NoSaveButton") {
							if (PlayerNum == 1) {
								if (MappingWindowDestroyOn || OpenCloseMethodNum == 2) {
									Destroy (gameObject);
									MappingWindowDestroyOn = false;
								} else {
									if (OpenCloseMethodNum == 0) {
										gameObject.SetActive (false);
									} else {
										MappingWindowCloseByButton ();
									}
									ExitConfirm.SetActive (false);
									ExitSituation = false;
								}
								if (CloseMappingDelegate != null) {
									CloseMappingDelegate ();
									CloseMappingDelegate = null;
								}
							} else {
								ExitConfirm.SetActive (false);
								ExitSituation = false;
								PlayerNumSituation = true;
								JoystickNum = 0;
							}
							KeyDelay = true;
							AxisDelay = true;
						}
						if (hit.collider.name == "ReturnButton") {
							ExitConfirm.SetActive (false);
							ExitSituation = false;
							KeyDelay = true;
							AxisDelay = true;
						}
					} else {
						if (hit.collider.name.IndexOf ("MapperMenuItem") == 0 && hit.collider.name.Length == 16) {
							if (FirstSet) {
								FirstSet = false;
							}
							if (KeyDelay != true) {
								OperateItemLine = false;
								if (MappingMode != true) {
									SelectPosition = hit.collider.gameObject.GetComponent<SelectOrnament> ().MenuNum;
									MappingMode = true;
									KeyDelay = true;
									AxisDelay = true;
								}
							}
						}
						if (CurrentryRestore) {
							if (hit.collider.name == "MapperOperateItem02") {
								if (FirstSet) {
									FirstSet = false;
								}
								if (KeyDelay != true && MappingMode != true) {
									OperateItemLine = true;
									OperateLineSelectPosition = 2;
									KeyDelay = true;
									AxisDelay = true;
									DefaultInputSet ();
								}
							}
							if (hit.collider.name == "MapperOperateItem03") {
								if (FirstSet) {
									FirstSet = false;
								}
								if (KeyDelay != true && MappingMode != true) {
									if (PlayerNum == 1) {
										if (OpenCloseMethodNum == 0) {
											gameObject.SetActive (false);
										} else if (OpenCloseMethodNum == 2) {
											Destroy (gameObject);
										} else {
											MappingWindowCloseByButton ();
										}
									} else {
										OperateItemLine = true;
										OperateLineSelectPosition = 3;
										PlayerNumSituation = true;
										JoystickNum = 0;
									}
									KeyDelay = true;
									AxisDelay = true;
								}
							}
						} else {
							if (hit.collider.name.IndexOf ("MapperOperateItem") == 0 && hit.collider.name.Length == 19) {
								if (FirstSet) {
									FirstSet = false;
								}
								if (KeyDelay != true && MappingMode != true) {
									OperateItemLine = true;
									OperateLineSelectPosition = hit.collider.gameObject.GetComponent<SelectOrnament2> ().MenuNum;
									KeyDelay = true;
									AxisDelay = true;
									switch (OperateLineSelectPosition) {
									case 0:
										SaveConfirmation ();
										break;
									case 1:
										RestorePrevious ();
										break;
									case 2:
										DefaultInputSet ();
										break;
									case 3:
										ExitSelectPosition = 0;
										ExitConfirm.SetActive (true);
										ExitSituation = true;
										break;
									}

								}
							}
						}
					}
				}
			}
		}

		if (KeyDelay != true) {
			for (int x = 0; x < KeyValues.Length; x++) {
				if (Input.GetKeyDown ((KeyCode)KeyValues.GetValue (x))) {
					if ((KeyCode)KeyValues.GetValue (x) != KeyCode.Escape) {
						for (int y = 0; y < ImpossibleMappingKeyArray.Length; y++) {
							if (KeyValues.GetValue (x).ToString () == ImpossibleMappingKeyArray [y]) {
								return;
							}
						}
						CurrentInput = KeyValues.GetValue (x).ToString ();
						if (MappingMode != true) {
							ProvisionalArray = new string[Mapper.InputArray.Length];
							for (int j = 1; j <=PlayerNum; j++) {
								switch (j) {
								case 1:
									Mapper.InputArray.CopyTo (ProvisionalArray, 0);
									break;
								case 2:
									Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
									break;
								case 3:
									Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
									break;
								case 4:
									Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
									break;
								}
								for (int i = 0; i < ExcludeNum.Count; i++) {
									if (CurrentInput == ProvisionalArray [ExcludeNum [i]].ToString ()) {
										return;
									}
								}
							}
						}
						if (CurrentInput.IndexOf ("Joystick") == 0 && CurrentInput.IndexOf ("Button") == 9) {
							if (JoystickNum != 0 && PlayerNum != 1 && PlayerNumSituation != true) {
								JoystickNumString = CurrentInput.Substring (8, 1);
								if (JoystickNum != int.Parse (JoystickNumString)) {
									return;
								}
							}
							if (JoystickNum == 0 && PlayerNum != 1) {
								JoystickNumString = CurrentInput.Substring (8, 1);
								JoystickNum = int.Parse (JoystickNumString);
								OtherJoystickNumMingleCheck ();
							}
						}
						if (FirstSet && PlayerNumSituation != true) {
							SelectPosition = 0;
							FirstSet = false;
							AxisDelay = true;
							KeyDelay = true;
						}
																
						AnyPressProsessing ();
					}
				}
			}

			for (int i = 0; i < MaxMenuSize; i++) {
				ProvisionalArray = new string[Mapper.InputArray.Length];
				for (int j = 1; j <=PlayerNum; j++) {
					switch (j) {
					case 1:
						Mapper.InputArray.CopyTo (ProvisionalArray, 0);
						break;
					case 2:
						Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
						break;
					case 3:
						Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
						break;
					case 4:
						Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
						break;
					}
					if (jInput.GetKeyDown (ProvisionalArray [i])) {
						for (int k = 0; k < ExcludeNum.Count; k++) {
							if (i == ExcludeNum [k] && MappingMode != true) {
								return;
							}
						}
						CurrentInput = ProvisionalArray [i].ToString ();
						if (CurrentInput.IndexOf ("Joystick") == 0 && CurrentInput.IndexOf ("Axis") == 9) {
							if (JoystickNum != 0 && PlayerNum != 1 && PlayerNumSituation != true) {
								JoystickNumString = CurrentInput.Substring (8, 1);
								if (JoystickNum != int.Parse (JoystickNumString)) {
									return;
								}
							}
							if (JoystickNum == 0 && PlayerNum != 1) {
								JoystickNumString = CurrentInput.Substring (8, 1);
								JoystickNum = int.Parse (JoystickNumString);
								OtherJoystickNumMingleCheck ();
							}
						}
						if (FirstSet && PlayerNumSituation != true) {
							SelectPosition = 0;
							FirstSet = false;
							AxisDelay = true;
							KeyDelay = true;
						}
																
						AnyPressProsessing ();
					}
				}
			}

			if (PlayerNumSituation != true) {
				if (MappingMode && AxisDelay != true) {
					if (Input.GetAxis ("MouseWheel") > 0.2) {
						CurrentInput = "MouseWheel+";
						if (Array.IndexOf (ImpossibleMappingKeyArray, CurrentInput) != -1) {
							return;
						}
						TextComponent.text = CurrentInput;
						InputSetting (CurrentInput);
						MappingMode = false;
						KeyDelay = true;
						AxisDelay = true;
						PreviousText = null;
						CurrentryRestore = false;
					} else if (Input.GetAxis ("MouseWheel") < -0.2) {
						CurrentInput = "MouseWheel-";
						if (Array.IndexOf (ImpossibleMappingKeyArray, CurrentInput) != -1) {
							return;
						}
						TextComponent.text = CurrentInput;
						InputSetting (CurrentInput);
						MappingMode = false;
						KeyDelay = true;
						AxisDelay = true;
						PreviousText = null;
						CurrentryRestore = false;
					}
				}

				for (int i = 1; i <= 9; i++) {
					for (int j = 1; j <= 20; j++) {
						AxisName = "Joystick" + (i) + "Axis" + (j);
						if (Input.GetAxis (AxisName) > 0.17) {
							CurrentInput = AxisName + "+";
							if (Array.IndexOf (ImpossibleMappingKeyArray, CurrentInput) != -1) {
								return;
							}
							if (JoystickNum != 0 && JoystickNum != i && PlayerNum != 1) {
								return;
							}
							if (JoystickNum == 0 && PlayerNum != 1) {
								JoystickNum = i;
								OtherJoystickNumMingleCheck ();
							}
							if (MappingMode && AxisDelay != true) {
								ProvisionalArray = new string[Mapper.InputArray.Length];
								switch (PlayerSelectNum) {
								case 1:
									Mapper.InputArray.CopyTo (ProvisionalArray, 0);
									break;
								case 2:
									Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
									break;
								case 3:
									Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
									break;
								case 4:
									Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
									break;
								}
								for (int k = 0; k < ProvisionalArray.Length; k++) {
									if (CurrentInput == ProvisionalArray [k].ToString ()) {
										return; //Exclude already Mapping key, prosess of already Mapping key is written at different line.(AnyPressProsessing())
									}
								}
								TextComponent.text = CurrentInput;
								InputSetting (CurrentInput);
								MappingMode = false;
								KeyDelay = true;
								AxisDelay = true;
								PreviousText = null;
								CurrentryRestore = false;
								return;
							}
						} else if (Input.GetAxis (AxisName) < -0.17) {
							CurrentInput = AxisName + "-";
							if (Array.IndexOf (ImpossibleMappingKeyArray, CurrentInput) != -1) {
								return;
							}
							if (JoystickNum != 0 && JoystickNum != i && PlayerNum != 1) {
								return;
							}
							if (JoystickNum == 0 && PlayerNum != 1) {
								JoystickNum = i;
								OtherJoystickNumMingleCheck ();
							}
							if (MappingMode && AxisDelay != true) {
								ProvisionalArray = new string[Mapper.InputArray.Length];
								switch (PlayerSelectNum) {
								case 1:
									Mapper.InputArray.CopyTo (ProvisionalArray, 0);
									break;
								case 2:
									Mapper.InputArray2p.CopyTo (ProvisionalArray, 0);
									break;
								case 3:
									Mapper.InputArray3p.CopyTo (ProvisionalArray, 0);
									break;
								case 4:
									Mapper.InputArray4p.CopyTo (ProvisionalArray, 0);
									break;
								}
								for (int k = 0; k < ProvisionalArray.Length; k++) {
									if (CurrentInput == ProvisionalArray [k].ToString ()) {
										return; //Exclude already Mapping key, prosess of already Mapping key is written at different line.(AnyPressProsessing())
									}
								}
								TextComponent.text = CurrentInput;
								InputSetting (CurrentInput);
								MappingMode = false;
								KeyDelay = true;
								AxisDelay = true;
								PreviousText = null;
								CurrentryRestore = false;
								return;
							}
						}
					}
				}
			}
		}

	}

	public void EscBehavior ()
	{
		if (MappingMode) { //when waiting to be pressed the mapping key
			MappingMode = false;
			if (TextComponent != null && PreviousText != null) {
				TextComponent.text = PreviousText;
			}
			PreviousText = null;
		} else if (SavingSituation || ExitSituation) { //when ConfirmWindow or ExitWindow is open
			gameObject.SetActive (true);
			SaveConfirm.SetActive (false);
			SavingSituation = false;
			ExitConfirm.SetActive (false);
			ExitSituation = false;
		} else if (PlayerNumSituation && gameObject.activeSelf != false) { //when player select
			if (OpenCloseMethodNum == 0) {
				gameObject.SetActive (false);
				PlayerSelectNum = 1;
				CloseButtonSelect = false;
			}
		} else if (CurrentryRestore && gameObject.activeSelf != false) { //when current mapping is same as save data in normal state
			if (PlayerNum == 1) {
				if (OpenCloseMethodNum == 0) {
					gameObject.SetActive (false);
				}
			} else {
				PlayerNumSituation = true;
				JoystickNum = 0;
			}
		} else if (CurrentryRestore != true && gameObject.activeSelf != false) { //when current mapping may be different from save data in normal state
			ExitSelectPosition = 0;
			ExitConfirm.SetActive (true);
			ExitSituation = true;
		} else {
			if (gameObject.activeSelf != true) {
				MappingWindowPrepare ();
			}
		}
		KeyDelay = true;
		AxisDelay = true;
	}

	public void BackPlayerSelect ()
	{
		if (PlayerNum != 1) {
			if (MappingMode) { //when waiting to be pressed the mapping key
				MappingMode = false;
				if (TextComponent != null && PreviousText != null) {
					TextComponent.text = PreviousText;
				}
				PreviousText = null;
			}
			if (SavingSituation) { //when ConfirmWindow is open
				SaveConfirm.SetActive (false);
				SavingSituation = false;
				ExitSelectPosition = 0;
				ExitConfirm.SetActive (true);
				ExitSituation = true;
			}
			if (ExitSituation) { //when ExitWindow is open
				return;
			} else if (PlayerNumSituation && gameObject.activeSelf != false) { //when player select
				return;
			} else if (CurrentryRestore && gameObject.activeSelf != false) { //when current mapping is same as save data
				PlayerNumSituation = true;
				JoystickNum = 0;
			} else if (CurrentryRestore != true && gameObject.activeSelf != false) { //when current mapping may be different from save data
				ExitSelectPosition = 0;
				ExitConfirm.SetActive (true);
				ExitSituation = true;
			} else { //when mapping window closed
				Debug.LogWarning ("[jInput] The input mapping window have closed.");
			}
			KeyDelay = true;
			AxisDelay = true;
		} else {
			Debug.LogWarning ("[jInput] To execute BackPlayerSelect() Method is needed two and more of Max Players in Same Place.");
		}
	}

	public void MappingWindowOpen ()
	{
		if (gameObject.activeSelf != true) {
			MappingWindowPrepare ();
		} else {
			Debug.LogWarning ("[jInput] Mapping window have already opened.");
		}
	}

	void MappingWindowPrepare ()
	{
		gameObject.SetActive (true);
		FirstSet = true;
		if (PlayerNum == 1) {
			TidyMenuItemText ();
		}
		SelectPosition = 0;
		JoystickNum = 0;
		PlayerSelectNum = 1;
		CloseButtonSelect = false;
		OperateItemLine = false;
		SaveConfirm.SetActive (false);
		SavingSituation = false;
		ExitConfirm.SetActive (false);
		ExitSituation = false;
		SaveSucsess.SetActive (false);
		Savefailure.SetActive (false);
		KeyDelay = true;
		AxisDelay = true;
		if (PlayerNum == 1) {
			PlayerNumSituation = false;
		} else {
			PlayerNumSituation = true;
		}
	}

	void MappingWindowCloseByButton ()
	{
		Application.LoadLevel("TestScene");
	}

	public void MappingWindowClose ()
	{
		CloseMappingDelegate = null;
		if (OpenCloseMethodNum != 0) {
			Debug.LogError ("[jInput] To execute MappingWindowClose() Method is needed the argument of the Method for LoadLevel when Always OpenWindow is checked.");
		} else {
			CloseWindowExecution ();
		}
	}
	public void MappingWindowClose (MultiDelegate TemporaryDelegate)
	{
		CloseMappingDelegate += TemporaryDelegate;
		CloseWindowExecution ();
	}
	public void MappingWindowDestroy ()
	{
		if (OpenCloseMethodNum != 0) {
			MappingWindowDestroyOn = true;
			CloseWindowExecution ();
		} else {
			Debug.LogError ("[jInput] MappingWindowDestroy() Method is not able to executed when it is not check Always OpenWindow.");
		}

	}
	void CloseWindowExecution ()
	{
		if (MappingMode) { //when waiting to be pressed the mapping key
			MappingMode = false;
			if (TextComponent != null && PreviousText != null) {
				TextComponent.text = PreviousText;
			}
			PreviousText = null;
		}
		if (SavingSituation) { //when ConfirmWindow is open
			SaveConfirm.SetActive (false);
			SavingSituation = false;
			ExitSelectPosition = 0;
			ExitConfirm.SetActive (true);
			ExitSituation = true;
		}
		if (ExitSituation) { //when ExitWindow is open
			return;
		} else if (PlayerNumSituation && gameObject.activeSelf != false) { //when player select
			if (MappingWindowDestroyOn || OpenCloseMethodNum == 2) {
				Destroy (gameObject);
				MappingWindowDestroyOn = false;
			} else {
				if (OpenCloseMethodNum == 0) {
					gameObject.SetActive (false);
					PlayerSelectNum = 1;
					CloseButtonSelect = false;
				}
			}
			if (CloseMappingDelegate != null) {
				CloseMappingDelegate ();
				CloseMappingDelegate = null;
			}
		} else if (CurrentryRestore && gameObject.activeSelf != false) { //when current mapping is same as save data
			if (PlayerNum != 1) {
				PlayerNumSituation = true;
				JoystickNum = 0;
			} else {
				if (MappingWindowDestroyOn || OpenCloseMethodNum == 2) {
					Destroy (gameObject);
					MappingWindowDestroyOn = false;
				} else {
					if (OpenCloseMethodNum == 0) {
						gameObject.SetActive (false);
						PlayerSelectNum = 1;
						CloseButtonSelect = false;
					}
				}
				if (CloseMappingDelegate != null) {
					CloseMappingDelegate ();
					CloseMappingDelegate = null;
				}
			}
		} else if (CurrentryRestore != true && gameObject.activeSelf != false) { //when current mapping may be different from save data
			ExitSelectPosition = 0;
			ExitConfirm.SetActive (true);
			ExitSituation = true;
		} else { //when mapping window closed
			if (MappingWindowDestroyOn || OpenCloseMethodNum == 2) {
				Destroy (gameObject);
				MappingWindowDestroyOn = false;
			}
			if (CloseMappingDelegate != null) {
				CloseMappingDelegate ();
				CloseMappingDelegate = null;
			}
		}
		KeyDelay = true;
		AxisDelay = true;
	}

	void AnyPressProsessing ()
	{
		if (KeyDelay != true) {
			if (SavingSituation) {
				for (int i = 0; i <= 9; i++) {
					if (CurrentInput == "Mouse" + (i)) {
						return;
					}
				}
				if (CurrentInput.Length >= 14) {
					MapNameExclude = CurrentInput.Substring (0, 14);
					if (MapNameExclude == "JoystickButton") {
						return;
					}
				}
				KeyDelay = true;
				AxisDelay = true;
				if (SaveNoSelectPosition) {
					SaveConfirm.SetActive (false);
					SavingSituation = false;
				} else {
					SaveProsess ();
				}
			} else if (ExitSituation) {
				for (int i = 0; i <= 9; i++) {
					if (CurrentInput == "Mouse" + (i)) {
						return;
					}
				}
				if (CurrentInput.Length >= 14) {
					MapNameExclude = CurrentInput.Substring (0, 14);
					if (MapNameExclude == "JoystickButton") {
						return;
					}
				}
				KeyDelay = true;
				AxisDelay = true;
				if (ExitSelectPosition == 0) {
					SaveProsess ();
				}
				if (ExitSelectPosition == 0 || ExitSelectPosition == 1) {
					if (PlayerNum == 1) {
						if (MappingWindowDestroyOn || OpenCloseMethodNum == 2) {
							Destroy (gameObject);
							MappingWindowDestroyOn = false;
						} else {
							if (OpenCloseMethodNum == 0) {
								gameObject.SetActive (false);
							} else {
								MappingWindowCloseByButton ();
							}
							ExitConfirm.SetActive (false);
							ExitSituation = false;
						}
						if (CloseMappingDelegate != null) {
							CloseMappingDelegate ();
							CloseMappingDelegate = null;
						}
					} else {
						ExitConfirm.SetActive (false);
						ExitSituation = false;
						PlayerNumSituation = true;
						JoystickNum = 0;
					}
				} else {
					ExitConfirm.SetActive (false);
					ExitSituation = false;
				}
			} else if (PlayerNumSituation && gameObject.activeSelf != false) {
				for (int i = 0; i <= 9; i++) {
					if (CurrentInput == "Mouse" + (i)) {
						return;
					}
				}
				if (CurrentInput.Length >= 14) {
					MapNameExclude = CurrentInput.Substring (0, 14);
					if (MapNameExclude == "JoystickButton") {
						return;
					}
				}
				if (CloseButtonSelect) {
					if (OpenCloseMethodNum == 0) {
						gameObject.SetActive (false);
						PlayerSelectNum = 1;
						CloseButtonSelect = false;
					} else if (OpenCloseMethodNum == 2) {
						Destroy (gameObject);
					} else {
						MappingWindowCloseByButton ();
					}
					return;
				}
				TidyMenuItemText ();
				PlayerNumTextObject.SetActive (true);
				KeyDelay = true;
				AxisDelay = true;
				FirstSet = true;
				SelectPosition = 0;
				CurrentryRestore = true;
				CloseButtonSelect = false;
				OperateItemLine = false;
				PlayerNumSituation = false;
			} else {
				if (OperateItemLine != true) {
					if (CurrentInput.Length >= 14) {
						MapNameExclude = CurrentInput.Substring (0, 14);
						if (MapNameExclude == "JoystickButton") {
							return;
						}
					}
					if (MappingMode != true) {
						for (int i = 0; i <= 9; i++) {
							if (CurrentInput == "Mouse" + (i)) {
								return;
							}
						}
						MappingMode = true;
						KeyDelay = true;
						AxisDelay = true;
					} else {
						TextComponent.text = CurrentInput;
						InputSetting (CurrentInput);
						MappingMode = false;
						KeyDelay = true;
						AxisDelay = true;
						PreviousText = null;
						CurrentryRestore = false;
					}
				} else {
					for (int i = 0; i <= 9; i++) {
						if (CurrentInput == "Mouse" + (i)) {
							return;
						}
					}
					if (CurrentInput.Length >= 14) {
						MapNameExclude = CurrentInput.Substring (0, 14);
						if (MapNameExclude == "JoystickButton") {
							return;
						}
					}
					KeyDelay = true;
					AxisDelay = true;
					if (CurrentryRestore) {
						if (OperateLineSelectPosition == 2) {
							DefaultInputSet ();
						} else if (OperateLineSelectPosition == 3) {
							if (PlayerNum != 1) {
								PlayerNumSituation = true;
								JoystickNum = 0;
							} else {
								if (OpenCloseMethodNum == 0) {
									gameObject.SetActive (false);
								} else if (OpenCloseMethodNum == 2) {
									Destroy (gameObject);
								} else {
									MappingWindowCloseByButton ();
								}
							}

						}
					} else {
						switch (OperateLineSelectPosition) {
						case 0:
							SaveConfirmation ();
							break;
						case 1:
							RestorePrevious ();
							break;
						case 2:
							DefaultInputSet ();
							break;
						case 3:
							ExitSelectPosition = 0;
							ExitConfirm.SetActive (true);
							ExitSituation = true;
							break;
						}
					}
				}
			}

		}

	}
}