using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int TotalRounds = 3; //total number of rounds
    private static int PlayerOneWins;// how many wins player 1 has
    private static int PlayerTwoWins; // how many wins player 2 has
    private static int lastWin = 0; // Stores the last player to have won a round ; 1 if p1, 2 if p2
    float threashold;

	private static int curRound = 1;
    private static int p1Hits = 0; //stores hits for previous rounds
    private static int p2Hits = 0;
	// Use this for initialization
	public Image Round11;
	public Image Round12;
	public Image Round21;
	public Image Round22;

	public Image star1;
	public Image star2;
	public Image star3;
	public Image star4;
	public Image star5;
	public Image star6;
	public Image star7;
	public Image star8;
	//public PlayerHealth playerHealth;
	public float restartDelay = 5f;
	//int[] table;
	public static List<int> list = new List<int>();

	Animator anim;
	float restartTimer;

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


		int numHits = 4;// GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getHits();
        //if ((Application.loadedLevelName != "GameOverFinalPlayer1") || (Application.loadedLevelName != "GameOverFinalPlayer2"))
        //{
        //    DisplaysStars();
        //}

        if (Application.loadedLevelName != "GameOverFinalPlayer1") {  //|| (Application.loadedLevelName != "GameOverFinalPlayer2"))
        
            DisplaysStars();
        }
		//Debug.Log ("here is " + Application.loadedLevelName);
	}	

   	void DisplaysStars(){
		if (PlayerOneWins == 1) {
			star1.fillAmount = 1;
			star5.fillAmount = 1;
		}
		if (PlayerOneWins == 2) {
			star6.fillAmount = 1;
			star2.fillAmount = 1;
			star5.fillAmount = 1;
			star1.fillAmount = 1;
		}

		if (PlayerTwoWins == 1) {
			star4.fillAmount = 1;
			star7.fillAmount = 1;
		}
		if (PlayerTwoWins == 2) {
			star4.fillAmount = 1;
			star7.fillAmount = 1;
			star8.fillAmount = 1;
			star3.fillAmount = 1;
		}
	}


     void Awake() {
//        DontDestroyOnLoad(this);
		anim = GetComponent<Animator>();

    }

	// Update is called once per frame
	void Update () {
		if (curRound == 2) {
			Destroy (Round11);
			Destroy (Round12);

		} else if (curRound == 3) {
			Destroy(Round21);
			Destroy(Round22);

		}

        if (PlayerOneWins > threashold)
        {
            Debug.Log("MAX Number of rounds reached- P1 has won");
        }
        if (PlayerTwoWins > threashold)
        {
            Debug.Log("MAX Number of rounds reached- P2 has won");
        }

//		anim.SetTrigger ("GameOver");

		//restartTimer += Time.deltaTime;


		//Check if the user clicks restart and go to TestScene, else if 
		//the user click exit, stop running Unity. 

		if (restartTimer >= restartDelay) { 
			Application.LoadLevel("TestScene");

		}
	}

    public void IncrementPlayerOneWins()
    {
        lastWin = 1;
		StaticStore.setWinnerName (lastWin);
		curRound++;
		PlayerOneWins++;
		Debug.Log (PlayerOneWins);     
        getHits();  
	
    }

    public int getPlayer1Hits()
    {
        return p1Hits;
    }


	public int getWinnerName() {

		return lastWin;
	}

    public int getPlayer2Hits()
    {
        return p2Hits;
    }

	public int getNumberOfRounds(){
		return curRound;
	}

	public List<int> getNumberOfRoundWinners(){
		//table[] = lastWin;
		list.Add(lastWin);
		return list;
	}

    public void IncrementPlayerTwoWins()
    {
		curRound++;
        lastWin = 2;
		StaticStore.setWinnerName (lastWin);
        PlayerTwoWins++;
        getHits();

    }

    public float getThreashold()
    {
        return threashold;
    }

    void getHits()
    {
//		StaticStore ss = this.GetComponent<StaticStore> ();
		StaticStore.setPlayer1Hits(GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getHits());
		StaticStore.setPlayer2Hits(GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>().getHits());
		//StaticStore.setWinerName(GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getHits());
        //p2Hits = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>().getHits();

    }

}
