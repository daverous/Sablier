using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class StaticStore {

   	public static int player1Kills = 0;
    public static int player2Kills = 0;
    public static int player1TotalKills = 0;
    public static int player2TotalKills = 0;
	private static int winnerName;
    public static int currentRound = 1;
	private static int numOfRounds;
	private static List<int> list = new List<int>();

	static GameManager gm;

    public static void resetAll()
    {
        player1Kills = 0;
        player2Kills = 0;
        winnerName = 0;
        currentRound = 1;
        numOfRounds = 0;
        player1TotalKills = 0;
        player2TotalKills = 0;
    }
	public static void setPlayer1Kills(int val) {
        player1TotalKills += val;
        player1Kills = val;
		}

    public static void setPlayer2Kills(int val)
    {
        player2TotalKills += val;
        player2Kills = val;
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

	public static int getPlayer1Kills() {
        return player1Kills;
	}

	public static int getPlayer2Kills() {
        return player2Kills;
	}

	public static int getWinnerName() {
		return winnerName;
	}

	
	public static List<int> getNumberOfRoundWinners(){

		return list;
	}
}