using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;
public class RoundWonScript : MonoBehaviour {

	public Text RoundText;
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



		RoundText.text = "Round " + currentRound;
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
