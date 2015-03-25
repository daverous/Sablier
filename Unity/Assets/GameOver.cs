using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Text gameOver;
	Character character;

	void Start () {
		if (tag =="WinnerText") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
		//manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

	
	// Update is called once per frame
	void Update () {
		int go = StaticStore.getNumberOfRounds ();

		if (go == 3) {
			gameOver.text = "Game Over!";
		}
	}
}
