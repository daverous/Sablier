using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;
public class RoundWonScript : MonoBehaviour {

	public Text RoundText;
    public Text p1Kills;
    public Text p2Kills;
    PlayerIndex playerIndex = (PlayerIndex)0;
    PlayerIndex player2Index = (PlayerIndex)1;
    GamePadState controller1State;
    Animator animator;
    GamePadState controller2State;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        //-1 because increm Has already occured
        int currentRound = StaticStore.currentRound - 1;
        int p1KillsVa = StaticStore.player1Kills;



		RoundText.text = "Round " + currentRound;
       
        p2Kills.text = "P2 Kills: " + StaticStore.player2Kills;
        p1Kills.text = "P1 Kills: " + p1KillsVa;
        controller1State = GamePad.GetState(playerIndex);
        controller2State = GamePad.GetState(player2Index);
        if (controller1State.Buttons.A == ButtonState.Pressed || controller2State.Buttons.A == ButtonState.Pressed)
        {
            switch (currentRound) { 
                case 0:    Application.LoadLevel("TestScene");
                    break;
                case 1: Application.LoadLevel("RoundTwoScene");
                    break;
                case 2: Application.LoadLevel("RoundThreeScene");
                    break;
        }
        }
	
	}
}
