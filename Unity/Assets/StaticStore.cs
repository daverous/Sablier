using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class StaticStore {

   	private static int player1Hits;
	private static int player2Hits;
	private static int winnerName;
    public static int currentRound = 1;
	private static int numOfRounds;
	private static List<int> list = new List<int>();

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

	public static void setNumberOfRoundWinners(List<int> val) {
		list = val;
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

	
	public static List<int> getNumberOfRoundWinners(){

		return list;
	}
}