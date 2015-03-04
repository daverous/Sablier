using UnityEngine;
using System.Collections;

public class CanvasScript : MonoBehaviour {
	Canvas canvas;
	public Camera camera;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();
		canvas.worldCamera = camera; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
