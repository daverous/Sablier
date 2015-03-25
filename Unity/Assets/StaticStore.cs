﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class StaticStore {

   	private static int player1Hits;
	private static int player2Hits;
	private static int winnerName;
	private static int numOfRounds;

	static GameManager gm;

	public static void setPlayer1Hits(int val) {
		player1Hits = val;
		}

	public static void setPlayer2Hits(int val) {
		player2Hits = val;
	}

	public static void setWinnerName(int val) {
		winnerName = val;
	}

	public static void setNumberOfRounds(int val) {
		numOfRounds = val;
	}
	// Use this for initialization

//	void Update() {
//			player1Hits = gm.getPlayer1Hits ();
//			player2Hits = gm.getPlayer2Hits ();
//	}


	public static int getNumberOfRounds(){
			return numOfRounds;
	}

	public static int getPlayer1Hits() {
		return player1Hits;
	}

	public static int getPlayer2Hits() {
		return player2Hits;
	}

	public static int getWinnerName() {
		return winnerName;
	}
}