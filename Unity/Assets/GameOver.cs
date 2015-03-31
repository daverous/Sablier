using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour {

	public Text gameOver;
	Character character;

	private static List<int> list; // = new List<int>();

	void Start () {
		if (tag =="WinnerText") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
		//manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

	
	// Update is called once per frame
	void Update () {

		int go = StaticStore.getNumberOfRounds ();
        //list = StaticStore.getNumberOfRoundWinners ();

		if (list.Count == 2) { //If length of list is two, compare the elements.
			if (list.IndexOf (0).Equals (list.IndexOf (1))) {
				gameOver.text = "Game is Over!";
			}
		}
		else if (go == 3) {
			gameOver.text = "Game Over!";
		}
	}
}
