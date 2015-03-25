using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class SelectOrnamentCloseButton : MonoBehaviour
{
		jInputSettings SetScript;
		Material RndMaterial;
		TextMesh TextComponent;
		GameObject Selection;
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
				FontNormalColor = new Color (0.65f, 0.65f, 0.65f, 1.0f);
		[SerializeField]
		Color
				FontSelectColor = new Color (0.97f, 0.95f, 0.95f, 1.0f);
		
		void Start ()
		{
				GameObject SetObject = GameObject.Find ("jInputMappingSet");
				SetScript = SetObject.GetComponent<jInputSettings> ();
				TextComponent = transform.Find ("TextPrefab").gameObject.GetComponent<TextMesh> ();
//				if (!EditorApplication.isPlaying && Application.isEditor) {
//						RndMaterial = GetComponent<Renderer> ().sharedMaterial;
//				} 
		if (false) {
				}
		else {
						RndMaterial = GetComponent<Renderer> ().material;
						Selection = SetObject.transform.Find ("Selection").gameObject;
						if (Selection == null) {
								Debug.LogError ("[jInput] Selection Not Found!!");
						}
				}
		}
		
		void Update ()
		{
				if (SetScript.CloseButtonSelect) {
						RndMaterial.SetColor ("_Color", BackSelectColor);
						TextComponent.color = FontSelectColor;
						if (Selection != null) {
								Selection.SetActive (false);
						}
				} else { //normal(NonSelect)
						RndMaterial.SetColor ("_Color", BackNormalColor);
						TextComponent.color = FontNormalColor;
						if (Selection != null) {
								Selection.SetActive (true);
						}
				}


				#if (UNITY_EDITOR)
				if (!EditorApplication.isPlaying && Application.isEditor) {
						if (TempStateScript == null) {
								TempStateScript = GameObject.Find ("jInputMappingSet").GetComponent<TempStateToConfDesign> ();
						}
						if (TempStateScript != null && TempStateScript.TempState == 1) {
								RndMaterial.SetColor ("_Color", BackSelectColor);
								TextComponent.color = FontSelectColor;
						} else {
								RndMaterial.SetColor ("_Color", BackNormalColor);
								TextComponent.color = FontNormalColor;
						}
				}
				#endif
		}
}
