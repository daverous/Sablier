using UnityEngine;
using System.Collections;

public class RoundOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            Destroy(GameObject.FindWithTag("EventSystem"));
            Destroy(GameObject.FindObjectOfType<GameManager>());
            Application.LoadLevel("TestScene");
        }
	}
}
