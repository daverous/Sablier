using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SlideOutOnClickA : MonoBehaviour {

	Animator animator;
    PlayerIndex playerIndex = (PlayerIndex)0;
    PlayerIndex player2Index = (PlayerIndex)1;
    GamePadState controller1State;
    GamePadState controller2State;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        controller1State = GamePad.GetState(playerIndex);
        controller2State = GamePad.GetState(player2Index);
        if (controller1State.Buttons.A == ButtonState.Pressed || controller2State.Buttons.A == ButtonState.Pressed)
        {
			animator.SetBool("aIsPressed", true);
				}
	//	anim.SetTrigger("StartOnClick");
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("pressaslideoutstay")){
			Application.LoadLevel ("TestScene");
			}
	}
}
