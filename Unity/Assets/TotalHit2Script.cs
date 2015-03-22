using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalHit2Script : MonoBehaviour {
	
	public Text hits2;
	Character character;
	// Use this for initialization
	void Start () {
		if (tag =="Text1") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	

		//int hitNum = 23;
		int hit2Num = 20;
		
		//if (tag == "Text1") {
		
		hits2.text = "Number 2 hits: " + hit2Num; // + userHits.ToString();
		//}
	}
}
