﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public class GuiScript : MonoBehaviour
{

		bool pause = false;
		string header = "menu";
		Character thisChar;
		public GameObject input;
		int player = 0;
		public int positionIndex = 0;
		bool canIncrement = true;


        PlayerIndex playerIndex = (PlayerIndex)0;
        PlayerIndex player2Index = (PlayerIndex)1;
        GamePadState controller1State;
        GamePadState controller2State;
#region Buttons


#endregion
		private string[] selStrings = new string[] { "Resume", "Restart", "Quit" };


		void Start ()
		{


            controller1State = GamePad.GetState(playerIndex);
            controller2State = GamePad.GetState(player2Index);
        

		}
		void Update ()
		{
            if (controller1State.Buttons.Start == ButtonState.Pressed)
            {
						header = "Player 1 has Paused";
						thisChar = GameObject.FindGameObjectWithTag ("Player").GetComponent<Character> ();
						player = 1;
						pause = true;


				}
            if (controller2State.Buttons.Start == ButtonState.Pressed)
            {
						header = "Player 2 has Paused";
						thisChar = GameObject.FindGameObjectWithTag ("Player").GetComponent<Character> ();
						player = 2;
						pause = true;
				}

				switch (player) {
				case 0:
						break;
				case 1:
                        //float vPositive = jInput.GetAxis (Mapper.InputArray [1]);
                        //float vNegative = jInput.GetAxis (Mapper.InputArray [11]);
						float vertical = controller1State.ThumbSticks.Left.Y;
                        //Debug.Log ("Vertical" + vertical);
						if (vertical < -0.1) {
								if (positionIndex <= 2 && canIncrement) {
										positionIndex++;
										canIncrement = false;
								}
						} else if (vertical > 0.1) {
								if (positionIndex >= 1 && canIncrement) {
										positionIndex--;
										canIncrement = false;
								}

						} else {
								canIncrement = true;
						} 
						break;
				case 2:
                        //vPositive = jInput.GetAxis (Mapper.InputArray2p [1]);
                        //vNegative = jInput.GetAxis (Mapper.InputArray2p [11]);
						vertical = controller2State.ThumbSticks.Left.Y;

						if (vertical < -0.5) {
								if (positionIndex <= 2 && canIncrement) {
										positionIndex++;
								}
						} else if (vertical > 0.5) {
								if (positionIndex >= 1 && canIncrement) {
										positionIndex--;
								}
						} else {
								canIncrement = true;
						} 
						break;
				}
		}
		void OnGUI ()
		{

				// if game is paused, draw this
				if (pause) {
						drawPause ();
				}
		}
		// Draw the menu 
		public Vector2 scrollPosition = Vector2.zero;
		void drawPause ()
		{

				var centerX = Screen.width / 2;
				var centerY = Screen.height / 2;

				// location of the menu 
				var menuLeft = centerX - 50;
				var menuTop = centerY - 50;
				var menuWidth = 100;
				var menuHeight = 100;
				var special = centerX - 100;
				var buttonX = menuLeft + 10;
				var buttonWidth = 200;
				var buttonHeight = 30;

				GUI.Box (new Rect (special, menuTop, buttonWidth, buttonHeight), header);
				positionIndex = GUI.SelectionGrid (new Rect (menuLeft, menuTop + 1 * buttonHeight, menuWidth, menuHeight), positionIndex, selStrings, 1);


				// make time go slow, to make game feel paused
				Time.timeScale = 0.00001f;
				// Make a background box 

				switch (player) {
				case 0:
						break;
				case 1:
						if (false) {
								perform ();
						}
						break;
				case 2:
						if (false) {
								perform ();
						}
						break;
				}
		}




		void perform ()
		{

				//Resume
				if (positionIndex == 0) {
						pause = false;
						thisChar.setCharNotGrounded ();
						Time.timeScale = 1.0f;

				}

				// Restart 
				if (positionIndex == 1) {
						// Restart counters and such
						Time.timeScale = 1.0F;
						pause = false;
						Destroy (GameObject.FindWithTag ("EventSystem"));
						Destroy (GameObject.FindObjectOfType<GameManager> ());

						Application.LoadLevel (Application.loadedLevelName);

				}
				
				// Quit (Only works in the Build. Does not work in the eidtor!) 
				if (positionIndex == 2) {
						Debug.Log ("Aplication would quit in build");
						Application.Quit ();
				}
        
		}

		// Use this for initialization


}
