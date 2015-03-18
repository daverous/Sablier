﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int TotalRounds = 3; //total number of rounds
    private int PlayerOneWins = 0;// how many wins player 1 has
    private int PlayerTwoWins = 0; // how many wins player 2 has
    private int lastWin = 0; // Stores the last player to have won a round ; 1 if p1, 2 if p2
    float threashold;

    private int p1Hits = 0; //stores hits for previous rounds
    private int p2Hits = 0;
	// Use this for initialization
	void Start () {
        threashold = TotalRounds / 2;
	}
	
   
     void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Update is called once per frame
	void Update () {
        if (PlayerOneWins >= threashold)
        {
            Debug.Log("MAX Number of rounds reached- P1 has won");
        }
        if (PlayerTwoWins >= threashold)
        {
            Debug.Log("MAX Number of rounds reached- P2 has won");
        }
	}

    public void IncrementPlayerOneWins()
    {
        lastWin = 1;
        PlayerOneWins++;
        getHits();
       
        
    }

    public int getPlayer1Hits()
    {
        return p1Hits;
    }
    public int getPlayer2Hits()
    {
        return p2Hits;
    }
    public void IncrementPlayerTwoWins()
    {
        lastWin = 2;
        PlayerTwoWins++;
        getHits();
    }

    public float getThreashold()
    {
        return threashold;
    }

    void getHits()
    {
        p1Hits = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getHits();
        p2Hits = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>().getHits();

    }

}
