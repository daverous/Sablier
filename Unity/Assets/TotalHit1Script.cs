using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalHit1Script : MonoBehaviour {

	public Text hits;
	Character character;

	//GameManager manager;

	// Use this for initialization
	void Start () {
		if (tag =="Text1") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
		
		if (tag == "Text2")
		{
			character = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>();
		}
//
//		
//		float userHits = character.getHits();
//		
//		h.text = ("hits:" + userHits.ToString());

	}
	
	// Update is called once per frame
	void Update () {
//		
	//	float userHits = character.getHits();
//		
//		hits.text = ("hits:" + userHits.ToString());
		//string hitNum = manager.getPlayer1Hits;
		int hitNum = 23;
		int hit2Num = 20;

		//if (tag == "Text1") {

		hits.text = "Number 1 hits: " + hitNum; //.ToString(); // + userHits.ToString();
		//}
		//if (tag == "Text2") {
			//hits.text = "Number 2 hits: " + hit2Num;
		//}
	}
}
