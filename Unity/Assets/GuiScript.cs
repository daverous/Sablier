using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour
{

    bool pause = false;
    string header = "menu";

    void Update()
    {
        if (Input.GetAxis("PlayerOneStart") == 1)
        {
            header = "Player 1 has Paused";
            pause = true;
        }
        if (Input.GetAxis("PlayerTwoStart") == 1)
        {
            header = "Player 2 has Paused";
            pause = true;
        }
    }
    void OnGUI()
    {

        // if game is paused, draw this
        if (pause)
        {
            drawPause();
        }
    }
    // Draw the menu 
    public Vector2 scrollPosition = Vector2.zero;
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
        
        // make time go slow, to make game feel paused
        Time.timeScale = 0.00001f;
        // Make a background box 
        GUI.Box(new Rect(menuLeft, menuTop, menuWidth, menuHeight), header);

     
    
        // Start / Resume 
        if (GUI.Button(new Rect(buttonX, menuTop + 1 * buttonDist, buttonWidth, buttonHeight), "Resume"))
        {
            pause = false;
            Debug.Log(pause);

            Time.timeScale = 1.0f;

        }

        // Restart 
        if (GUI.Button(new Rect(buttonX, menuTop + 2 * buttonDist, buttonWidth, buttonHeight), "Restart"))
        {
            // Restart counters and such
            Time.timeScale = 1.0F;
            pause = false;
            Destroy(GameObject.FindWithTag("EventSystem"));
            Destroy(GameObject.FindObjectOfType<GameManager>());
            
            Application.LoadLevel(Application.loadedLevelName);
            
        }

        // Quit (Only works in the Build. Does not work in the eidtor!) 
        if (GUI.Button(new Rect(buttonX, menuTop + 3 * buttonDist, buttonWidth, buttonHeight), "Quit"))
        {
            Debug.Log("Aplication would quit in build");
            Application.Quit();
        }
    }

    // Use this for initialization
    void Start()
    {

    }


}
