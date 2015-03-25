﻿using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SelectOrnament2 : MonoBehaviour
{
	[HideInInspector]
	public int
		MenuNum;
	string MenuNumString;
	jInputSettings SetScript;
	Material RndMaterial;
	TextMesh TextComponent;
	int NameCheck;
		#if (UNITY_EDITOR)
	TempStateToConfDesign TempStateScript;
		#endif
	[Space(4)]
	[SerializeField]
	Color
		BackNormalColor = new Color (0.5f, 0.4f, 0.5f, 0.9f);
	[SerializeField]
	Color
		BackSelectColor = new Color (0.75f, 0.75f, 1.0f, 0.95f);
	[Space(7)]
	[SerializeField]
	Color
		FontNormalColor = new Color (0.8f, 0.75f, 0.75f, 1.0f);
	[SerializeField]
	Color
		FontSelectColor = new Color (0.97f, 0.95f, 0.95f, 1.0f);
	Color BackLightsOutColor = new Color (0.1f, 0.1f, 0.2f, 0.6f);
	Color FontLightsOutColor = new Color (0.2f, 0.2f, 0.25f, 0.6f);

	void Awake ()
	{
		NameCheck = gameObject.name.IndexOf ("MapperOperateItem");
		if (NameCheck != 0 || gameObject.name.Length != 19) {
			Debug.LogError ("[jInput] Error!! To be necessary MapperOperateItem Object naming 'MapperOperateItem'+ serial number of double figures 00 to 99.");
		} else {
			MenuNumString = gameObject.name.Substring (gameObject.name.Length - 2, 2);
			MenuNum = int.Parse (MenuNumString);
		}
	}

	void Start ()
	{
		if (SetScript == null)
			SetScript = GameObject.Find ("jInputMappingSet").GetComponent<jInputSettings> ();
		if (TextComponent == null)
			TextComponent = transform.Find ("TextPrefab").gameObject.GetComponent<TextMesh> ();
//		if (!EditorApplication.isPlaying && Application.isEditor) {
//			RndMaterial = GetComponent<Renderer> ().sharedMaterial;
//		} 
		else {
			RndMaterial = GetComponent<Renderer> ().material;
		}
	}

	void Update ()
	{
		if (RndMaterial != null) {
			if (SetScript.FirstSet) {
				RndMaterial.SetColor ("_Color", new Color (0.2f, 0.2f, 0.25f, 0.7f));
			} else if (SetScript.MappingMode) { //BackColor of wait Key-in
				RndMaterial.SetColor ("_Color", BackLightsOutColor);
			} else if (SetScript.OperateLineSelectPosition == MenuNum && SetScript.OperateItemLine) {
				if (SetScript.CurrentryRestore) { //BackColor of select, Same setting of Save & Current
					if (MenuNum == 0 || MenuNum == 1) {

					} else {
						RndMaterial.SetColor ("_Color", BackSelectColor);
					}
				} else { //BackColor of select
					if (MenuNum == 0) {
						RndMaterial.SetColor ("_Color", BackSelectColor + new Color (0.05f, 0.05f, 0.05f, 0.95f));
					} else {
						RndMaterial.SetColor ("_Color", BackSelectColor);
					}
				}
			} else { //normal(NonSelect)
				if (SetScript.CurrentryRestore) { //BackColor of normal, Same setting of Save & Current
					if (MenuNum == 0 || MenuNum == 1) {
						RndMaterial.SetColor ("_Color", BackLightsOutColor);
					} else {
						RndMaterial.SetColor ("_Color", BackNormalColor);
					}
				} else { //BackColor of normal
					if (MenuNum == 0) {
						RndMaterial.SetColor ("_Color", BackNormalColor + new Color (0.02f, 0.02f, 0.03f, 0.9f));
					} else {
						RndMaterial.SetColor ("_Color", BackNormalColor);
					}
				}
			}
		}


		if (SetScript.FirstSet) {
			TextComponent.color = FontLightsOutColor;
		} else if (SetScript.MappingMode) { //wait Key-in
			TextComponent.color = FontLightsOutColor + new Color (0.1f, 0.1f, 0.1f, 0.1f);
		} else {
			if (SetScript.CurrentryRestore) { //Same setting of Save & Current
				if (SetScript.OperateLineSelectPosition == MenuNum && SetScript.OperateItemLine) {//Select
					if (MenuNum == 0 || MenuNum == 1) {

					} else {
						TextComponent.color = FontSelectColor;
					}
				} else { //normal(NonSelect)
					if (MenuNum == 0 || MenuNum == 1) {
						TextComponent.color = FontLightsOutColor;
					} else {
						TextComponent.color = FontNormalColor;
					}
				}
			} else { //Not Same setting of Save&Current
				if (SetScript.OperateLineSelectPosition == MenuNum && SetScript.OperateItemLine) {//Select
					if (MenuNum == 0) {
						TextComponent.color = FontSelectColor + new Color (0.12f, 0.12f, 0.12f, 1.0f);
					} else {
						TextComponent.color = FontSelectColor;
					}
				} else { //normal(NonSelect)
					if (MenuNum == 0) {
						TextComponent.color = FontNormalColor + new Color (0.12f, 0.12f, 0.12f, 1.0f);
						;
					} else {
						TextComponent.color = FontNormalColor;
					}
				}
			}
		}

		#if (UNITY_EDITOR)
		if (!EditorApplication.isPlaying && Application.isEditor) {
			if (TempStateScript == null) {
				TempStateScript = GameObject.Find ("jInputMappingSet").GetComponent<TempStateToConfDesign> ();
			}
			if (TempStateScript != null && TempStateScript.TempState == 1) {
				if (MenuNum == 0) {
					RndMaterial.SetColor ("_Color", BackSelectColor + new Color (0.05f, 0.05f, 0.05f, 0.95f));
					TextComponent.color = FontSelectColor + new Color (0.3f, 0.3f, 0.3f, 1.0f);
				}
				/*
								 else {
										RndMaterial.SetColor ("_Color", BackSelectColor);
										TextComponent.color = FontSelectColor;
								}
								*/
			} else if (TempStateScript != null && TempStateScript.TempState == 2) {
				RndMaterial.SetColor ("_Color", BackLightsOutColor);
				TextComponent.color = FontLightsOutColor + new Color (0.1f, 0.1f, 0.1f, 0.1f);
			} else {
				if (MenuNum == 0) {
					RndMaterial.SetColor ("_Color", BackNormalColor + new Color (0.02f, 0.02f, 0.03f, 0.9f));
					TextComponent.color = FontNormalColor + new Color (0.12f, 0.12f, 0.12f, 1.0f);
					;
				} else {
					RndMaterial.SetColor ("_Color", BackNormalColor);
					TextComponent.color = FontNormalColor;
				}
			}
		}
		#endif
	}

}
