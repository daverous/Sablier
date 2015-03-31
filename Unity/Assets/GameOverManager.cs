using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;

public class GameOverManager : MonoBehaviour {
    public Text p1KillsText;
    public Text p2KillsText;
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
        p2KillsText.text = "P2 Total Kills: " + StaticStore.player2Kills;
        p1KillsText.text = "P1 Total Kills: " + StaticStore.player1Kills;
       controller1State = GamePad.GetState(playerIndex);
        controller2State = GamePad.GetState(player2Index);
        StaticStore.resetAll();
        if (controller1State.Buttons.A == ButtonState.Pressed || controller2State.Buttons.A == ButtonState.Pressed)
        {
            Application.LoadLevel("IntroFinal");
        }
    }
}
