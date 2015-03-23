﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hits1Script : MonoBehaviour {
    public Text hits;
    Character character;
	// Use this for initialization
	void Start () {
	    if (tag =="Text1") {
            
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        
        if (tag == "Text2")
        {
            character = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>();
        }
	}
	
	// Update is called once per frame
	void Update () {

            float userHits = character.getHits();

		if (tag =="Text1") {
			hits.text = ("HITS:" + userHits.ToString());		
			}
		
		if (tag == "Text2")
		{
			hits.text = (userHits.ToString()+":HITS");
		}
            
    }
}
