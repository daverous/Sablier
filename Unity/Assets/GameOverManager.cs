using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GameOverManager : MonoBehaviour {

    PlayerIndex playerIndex = (PlayerIndex)0;
    PlayerIndex player2Index = (PlayerIndex)1;
    GamePadState controller1State;
    GamePadState controller2State;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
       controller1State = GamePad.GetState(playerIndex);
        controller2State = GamePad.GetState(player2Index);
        if (controller1State.Buttons.A == ButtonState.Pressed || controller2State.Buttons.A == ButtonState.Pressed)
        {
            Application.LoadLevel("IntroFinal");
        }
    }
}
