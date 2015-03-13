using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int TotalRounds = 3; //total number of rounds
    private int PlayerOneWins = 0;// how many wins player 1 has
    private int PlayerTwoWins = 0; // how many wins player 2 has
    private int lastWin = 0; // Stores the last player to have won a round ; 1 if p1, 2 if p2
    float threashold;
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
    }
    public void IncrementPlayerTwoWins()
    {
        lastWin = 2;
        PlayerTwoWins++;
    }

    public float getThreashold()
    {
        return threashold;
    }

}
