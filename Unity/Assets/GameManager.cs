using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int TotalRounds = 3;
    private int PlayerOneWins = 0;
    private int PlayerTwoWins = 0;
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

        }
        if (PlayerTwoWins >= threashold)
        {

        }
	}

    public void IncrementPlayerOneWins()
    {
        PlayerOneWins++;
    }
    public void IncrementPlayerTwoWins()
    {
        PlayerTwoWins++;
    }

    public float getThreashold()
    {
        return threashold;
    }

}
