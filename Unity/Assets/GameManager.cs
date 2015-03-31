using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public int TotalRounds = 3; //total number of rounds
   
    float threashold;

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
		if (StaticStore.PlayerOneWins == 1) {
			star1.fillAmount = 1;
			star5.fillAmount = 1;
		}
        if (StaticStore.PlayerOneWins == 2)
        {
			star6.fillAmount = 1;
			star2.fillAmount = 1;
			star5.fillAmount = 1;
			star1.fillAmount = 1;
		}

        if (StaticStore.PlayerTwoWins == 1)
        {
			star4.fillAmount = 1;
			star7.fillAmount = 1;
		}
        if (StaticStore.PlayerTwoWins == 2)
        {
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
        if (StaticStore.currentRound == 2)
        {
			Destroy (Round11);
			Destroy (Round12);

		} else if (StaticStore.currentRound == 3) {
			Destroy(Round21);
			Destroy(Round22);

		}

        if (StaticStore.PlayerOneWins > threashold)
        {
            Application.LoadLevel("GameOverFinalPlayer1");
        }
        if (StaticStore.PlayerTwoWins > threashold)
        {
            Application.LoadLevel("GameOverFinalPlayer2");
        }

//		anim.SetTrigger ("GameOver");

		//restartTimer += Time.deltaTime;


		//Check if the user clicks restart and go to TestScene, else if 
		//the user click exit, stop running Unity. 

		if (restartTimer >= restartDelay) { 
			Application.LoadLevel("TestScene");

		}
	}

    public static int getCurrentRound()
    {
        return StaticStore.currentRound;
    }
    public void IncrementPlayerOneWins()
    {
        Debug.Log(StaticStore.currentRound);
		StaticStore.setWinnerName (1);
        
		StaticStore.currentRound++;
        StaticStore.PlayerOneWins++;
   
        getHits();
            if(StaticStore.currentRound == (TotalRounds + 1)) {
                Debug.Log("hersdsaasdsadsadsasdasade");  
                Application.LoadLevel("GameOverFinalPlayer1");
            }
        else {
            Debug.Log("here");  
                Application.LoadLevel("RoundWonPlayer1");
            }
    }




	public int getNumberOfRounds(){
		return StaticStore.currentRound;
	}


    public void IncrementPlayerTwoWins()
    {
		StaticStore.currentRound++;
		StaticStore.setWinnerName (2);
        StaticStore.PlayerTwoWins++;
        getHits();
         if(StaticStore.currentRound == (TotalRounds + 1)) {
                Application.LoadLevel("GameOverFinalPlayer2");
            }
        else {
                Application.LoadLevel("RoundWonPlayer2");
            }
    

    }

    public float getThreashold()
    {
        return threashold;
    }

    void getHits()
    {
//		StaticStore ss = this.GetComponent<StaticStore> ();
        StaticStore.setPlayer1Kills(GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getBaddiesKilled());
        StaticStore.setPlayer2Kills(GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>().getBaddiesKilled());
		//StaticStore.setWinerName(GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().getHits());
        //p2Hits = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>().getHits();

    }

}
