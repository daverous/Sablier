using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundWonScript : MonoBehaviour {

	public Text RoundText;
	Character character;

	// Use this for initialization
	void Start () {
		character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {

		int currentRound = StaticStore.getNumberOfRounds ();



		RoundText.text = "Round " + currentRound;
	
	}
}
