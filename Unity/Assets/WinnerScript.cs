using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinnerScript : MonoBehaviour {
	
	public Text winner;
	Character character;
//	GameManager manager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//		
		//	float userHits = character.getHits();
		//		
		//		hits.text = ("hits:" + userHits.ToString());
		int hitNum = 23;
		int hit2Num = 20;

	//	int one = manager.getWinner();

		//if (tag == "Text1") {
//
//		if (one == 1) {
//				winner.text = "Player 1 wins"; // + userHits.ToString();
//		}
//
		winner.text = "Player 2 wins";
		//}
		//if (tag == "Text2") {
		//hits.text = "Number 2 hits: " + hit2Num;
		//}
	}
}
