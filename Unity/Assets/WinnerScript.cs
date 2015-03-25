using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinnerScript : MonoBehaviour {
	
	public Text winner;
	Character character;
	//GameManager manager;

	// Use this for initialization
	void Start () {
		if (tag =="WinnerText") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
		//manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

		int winnerNum = StaticStore.getWinnerName (); //.getPlayer2Hits ();


		winner.text = "Player " + winnerNum + " wins";
		//}
		//if (tag == "Text2") {
		//hits.text = "Number 2 hits: " + hit2Num;
		//}
	}
}
