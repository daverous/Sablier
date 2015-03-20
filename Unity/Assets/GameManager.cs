using UnityEngine;
using UnityEngine.UI;
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


	//public PlayerHealth playerHealth;
	public float restartDelay = 5f;

	Animator anim;
	float restartTimer;
	
	public Text hits;
	Character character;
	// Use this for initialization

	void Start () {

		threashold = TotalRounds / 2;

		if (tag =="Text1") {
			
			character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
		}
		
		if (tag == "Text2")
		{
			character = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>();
		}


		int numHits = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getHits();

		//float userHits = character.getHits(); 
		hits.text = ("hits:" + numHits.ToString());

//		float userHits = character.getHits(); 
//		hits.text = ("hits:" + userHits.ToString());
	}	
   
     void Awake() {
        DontDestroyOnLoad(transform.gameObject);
		anim = GetComponent<Animator> ();

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

		anim.SetTrigger ("GameOver");

		restartTimer += Time.deltaTime;

//		if (restartTimer >= restartDelay) {
//			Application.LoadLevel ("Level 1");
//			//Application.LoadLevel (Application.loadedLevel);
//		}


		if (restartTimer >= restartDelay) {
			Application.LoadLevel("TestScene");
			//Application.LoadLevel (Application.loadedLevel);
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
