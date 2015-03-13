using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int TotalRounds = 3;
    private int PlayerOneWins = 0;
    private int PlayerTwoWins = 0;
    private int lastWin = 0;
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
