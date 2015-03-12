using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour {

    bool pause;
    void onGui()
    {
        // if game is paused, draw this
        if (pause)
        {
            drawPause();
        }
    }
    // Draw the menu 
    void drawPause()
    {
        var centerX = Screen.width / 2;
        var centerY = Screen.height / 2;

        // location of the menu 
        var menuLeft = centerX - 50;
        var menuTop = centerY - 50;
        var menuWidth = 100;
        var menuHeight = 100;

        var buttonX = menuLeft + 10;
        var buttonWidth = 80;
        var buttonHeight = 20;
        var buttonDist = 25; // distance between each button 

        // Make a background box 
        GUI.Box(new Rect(menuLeft, menuTop, menuWidth, menuHeight), "Menu");

        // Start / Resume 
        if (GUI.Button(new Rect(buttonX, menuTop + 1 * buttonDist, buttonWidth, buttonHeight), "Resume"))
        {
            pause = false;
        }

        // Restart 
        if (GUI.Button(new Rect(buttonX, menuTop + 2 * buttonDist, buttonWidth, buttonHeight), "Restart"))
        {
            // Restart counters and such
            Destroy(GameObject.FindObjectOfType<GameManager>());
            Application.LoadLevel(Application.loadedLevel);
        }

        // Quit (Only works in the Build. Does not work in the eidtor!) 
        if (GUI.Button(new Rect(buttonX, menuTop + 3 * buttonDist, buttonWidth, buttonHeight), "Quit"))
        {
            Application.Quit();
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
