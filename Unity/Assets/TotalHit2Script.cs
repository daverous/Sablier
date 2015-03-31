using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalHit2Script : MonoBehaviour {
	
	public Text hits2;
	Character character;
//	GameManager manager;

	// Use this for initialization
	void Start () {
		if (tag =="Text1") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
//		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		int hit2Num = StaticStore.getPlayer2Kills ();

		hits2.text = "Number 2 kills: " + hit2Num; 

	}
}
