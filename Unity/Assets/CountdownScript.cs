using UnityEngine;
using System.Collections;

public class CountdownScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 0.0001f;
        GameObject.FindGameObjectWithTag("Round1").SetActive(true);

	}
}
