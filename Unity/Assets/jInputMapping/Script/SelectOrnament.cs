using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SelectOrnament : MonoBehaviour
{
	[HideInInspector]
	public int
		MenuNum;
	string MenuNumString;
	[HideInInspector]
	public string
		HoldingText;
	[HideInInspector]
	public bool
		DuplicationTextColor;
	jInputSettings SetScript;
	bool InputWaiting;
	TextMesh TextComponent;
	int NameCheck;
	Material RndMaterial;
	
	MenuItemsCommonSetting CommonSettingScript;
	Vector2 HeadingRelativePosi;
	string HeadingText;
	GameObject HeadingObject;
	GameObject AlertMarkObject;
	[HideInInspector]
	public bool
		AlertMarkDisplaying;
	bool AlertMarkCheck;
		#if (UNITY_EDITOR)
	TempStateToConfDesign TempStateScript;
		#endif
	[Space(4)]
	[SerializeField]
	Color
		BackNormalColor = new Color (0.45f, 0.35f, 0.35f, 0.9f);
	[SerializeField]
	Color
		BackSelectColor = new Color (0.75f, 0.75f, 0.9f, 0.95f);
	[SerializeField]
	Color
		BackWaitInput = new Color (1.0f, 0.4f, 0.35f, 0.95f);
	[Space(7)]
	[SerializeField]
	Color
		FontNormalColor = new Color (0.85f, 0.85f, 0.85f, 1.0f);
	[SerializeField]
	Color
		FontSelectColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField]
	Color
		FontWaitInput = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField]
	Color
		FontOtherInput = new Color (0.6f, 0.6f, 0.6f, 0.5f);
	[Space(7)]
	[SerializeField]
	Color
		SameFontNormal = new Color (0.7f, 0.7f, 0.85f, 1.0f);
	[SerializeField]
	Color
		SameFontSelect = new Color (0.7f, 0.7f, 1.0f, 1.0f);
	[SerializeField]
	Color
		SameFontWaitInput = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	[SerializeField]
	Color
		SameFontOtherInput = new Color (0.8f, 0.8f, 1.0f, 0.4f);
	
	
	void Awake ()
	{
		if (SetScript == null) {
			SetScript = GameObject.Find ("jInputMappingSet").GetComponent<jInputSettings> ();
			HeadingTextPour ();
		}
	}
	
	void Start ()
	{
		NameCheck = gameObject.name.IndexOf ("MapperMenuItem");
		if (NameCheck != 0 || gameObject.name.Length != 16) {
			Debug.LogError ("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
		} else {
			MenuNumString = gameObject.name.Substring (gameObject.name.Length - 2, 2);
			MenuNum = int.Parse (MenuNumString);
		}
		HeadingText = SetScript.MenuItemHeadings [MenuNum];
		if (CommonSettingScript = transform.parent.GetComponent<MenuItemsCommonSetting> ()) {
			HeadingRelativePosi = CommonSettingScript.HeadingRelativePosi;
		} else {
			Debug.LogError ("[jInput] Error!! MenuItemsCommonSetting script Not Found!");
		}
		if (TextComponent == null)
			TextComponent = transform.Find ("TextPrefab").GetComponent<TextMesh> ();
		if (!EditorApplication.isPlaying && Application.isEditor)
			TextComponent.text = "Sample Text";
		if (HeadingObject == null)
			HeadingObject = transform.Find ("HeadingTextPrefab").gameObject;
		HeadingObject.GetComponent<TextMesh> ().text = HeadingText;
		HeadingObject.transform.position = transform.position + transform.right * HeadingRelativePosi.x + transform.up * HeadingRelativePosi.y;
		if (!EditorApplication.isPlaying && Application.isEditor) {
			RndMaterial = GetComponent<Renderer> ().sharedMaterial;
		} else {
			RndMaterial = GetComponent<Renderer> ().material;
		}
	}
	
	void DuplicationInput ()
	{
		DuplicationTextColor = true;
	}
	void  IndividualInput ()
	{
		DuplicationTextColor = false;
	}
	
	void Update ()
	{
		if (CommonSettingScript != null) {
			HeadingRelativePosi = CommonSettingScript.HeadingRelativePosi;
			HeadingObject.transform.position = transform.position + transform.right * HeadingRelativePosi.x + transform.up * HeadingRelativePosi.y;
		}

		if (DuplicationTextColor && InputWaiting != true) {
			if (SetScript.SelectPosition == MenuNum && SetScript.OperateItemLine != true && SetScript.FirstSet != true) {
				if (SetScript.MappingMode) {
					TextComponent.color = SameFontWaitInput;//With sameText, Color of wait Key-in
				} else {
					TextComponent.color = SameFontSelect;//With sameText, Select Color of normal
				}
			} else {
				if (SetScript.MappingMode) {
					TextComponent.color = SameFontOtherInput;//With sameText, NonSelect Color of wait Key-in
				} else {
					TextComponent.color = SameFontNormal;//With sameText, NonSelect Color of normal
				}
			}
		} else {
			if (SetScript.SelectPosition == MenuNum && SetScript.OperateItemLine != true && SetScript.FirstSet != true) {
				if (SetScript.MappingMode) {
					TextComponent.color = FontWaitInput;//Select TextColor of wait Key-in
				} else {
					TextComponent.color = FontSelectColor;//Select TextColor of normal
				}
			} else {
				if (SetScript.MappingMode) {
					TextComponent.color = FontOtherInput;//NonSelect TextColor of wait Key-in
				} else {
					TextComponent.color = FontNormalColor;//NonSelect TextColor of normal
				}
			}
		}
		
		if (SetScript.SelectPosition == MenuNum && SetScript.OperateItemLine != true && SetScript.FirstSet != true) {
			SetScript.TextComponent = TextComponent;
			if (SetScript.FirstSet != true) {
				if (SetScript.MappingMode) { //BackColor of wait Key-in
					RndMaterial.SetColor ("_Color", BackWaitInput);
					if (InputWaiting != true) {
						InputWaiting = true;
						SetScript.PreviousText = HoldingText;
						TextComponent.text = "Input...";
					}
				} else { //BackColor of select
					RndMaterial.SetColor ("_Color", BackSelectColor);
					HoldingText = TextComponent.text;
					InputWaiting = false;
				}
			}
		} else {	//BackColor of normal
			if (SetScript.MappingMode) { //Other wait Key-in
				RndMaterial.SetColor ("_Color", BackNormalColor - new Color (0.1f, 0.05f, 0.05f, 0.1f));
				HoldingText = TextComponent.text;
				InputWaiting = false;
			} else { //normal
				RndMaterial.SetColor ("_Color", BackNormalColor);
				HoldingText = TextComponent.text;
				InputWaiting = false;
			}
		}


		#if (UNITY_EDITOR)
		if (!EditorApplication.isPlaying && Application.isEditor) {
			if (TempStateScript == null) {
				TempStateScript = GameObject.Find ("jInputMappingSet").GetComponent<TempStateToConfDesign> ();
			}
			if (TempStateScript != null && TempStateScript.TempState == 1) { //select
				if (TempStateScript.SameKeyCheck && gameObject.name == "MapperMenuItem00") {
					TextComponent.color = SameFontSelect;
					RndMaterial.SetColor ("_Color", BackSelectColor);
				} else if (TempStateScript.SameKeyCheck && gameObject.name == "MapperMenuItem01") {
					TextComponent.color = SameFontNormal;
					RndMaterial.SetColor ("_Color", BackNormalColor);
				} else { //Non Same Key
					if (gameObject.name == "MapperMenuItem00") {
						TextComponent.color = FontSelectColor;
						RndMaterial.SetColor ("_Color", BackSelectColor);
					}
				}
			} else if (TempStateScript != null && TempStateScript.TempState == 2) {
				if (TempStateScript.SameKeyCheck && gameObject.name == "MapperMenuItem00") { //wait Input with Same Key
					TextComponent.color = SameFontWaitInput;
					RndMaterial.SetColor ("_Color", BackWaitInput);
				} else if (TempStateScript.SameKeyCheck && gameObject.name == "MapperMenuItem01") { //Other wait Input with Same Key
					TextComponent.color = SameFontOtherInput;
					RndMaterial.SetColor ("_Color", BackNormalColor - new Color (0.1f, 0.05f, 0.05f, 0.1f));
				} else { //Non Same Key
					if (gameObject.name == "MapperMenuItem00") { //wait Input
						TextComponent.color = FontWaitInput;
						RndMaterial.SetColor ("_Color", BackWaitInput);
					} else { //Other wait Input
						TextComponent.color = FontOtherInput;
						RndMaterial.SetColor ("_Color", BackNormalColor - new Color (0.1f, 0.05f, 0.05f, 0.1f));
					}
				}
			} else { //noamal
				if (TempStateScript.SameKeyCheck) {
					if (gameObject.name == "MapperMenuItem00" || gameObject.name == "MapperMenuItem01") {
						TextComponent.color = SameFontNormal;
						RndMaterial.SetColor ("_Color", BackNormalColor);
					}
				} else { //Non Same Key
					TextComponent.color = FontNormalColor;
					RndMaterial.SetColor ("_Color", BackNormalColor);
				}
			}
		}
		#endif
		
		if (AlertMarkDisplaying) {
			if (AlertMarkCheck != true) {
				AlertMarkCheck = true;
				AlertMarkObject = Instantiate (CommonSettingScript.AlertMarkPrefab) as GameObject;
				AlertMarkObject.transform.position = transform.position + new Vector3 (-(transform.lossyScale.x * 0.47f), (transform.lossyScale.y * 0.22f), -1);
				AlertMarkObject.transform.parent = transform;
			}
		} else {
			AlertMarkCheck = false;
			if (AlertMarkObject != null) {
				Destroy (AlertMarkObject);
			}
		}
		
	}

	#if (UNITY_EDITOR)
	public void HeadingTextPour ()
	{
		if (SetScript != null && SetScript.MenuItemHeadings.Length > MenuNum)
			HeadingText = SetScript.MenuItemHeadings [MenuNum];
		if (HeadingObject == null)
			HeadingObject = transform.Find ("HeadingTextPrefab").gameObject;
		if (HeadingObject != null)
			HeadingObject.GetComponent<TextMesh> ().text = HeadingText;
	}
	#endif

}
