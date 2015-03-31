using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CameraScript : MonoBehaviour
{
    #region Fields
    public Transform CameraTarget;
    private float x = 0.0f;
    private float y = 0.0f;
    public float controllerSensitivityX;
    public float controllerSensitivityY;
    private CamStates camState;
    public float minimumY = -40F;
    public float maximumY = 50F;
    float rotationY = 0F;
    Character thisChar;
	bool canLock = true;
    PlayerIndex playerIndex = (PlayerIndex)0;
    PlayerIndex player2Index = (PlayerIndex)1;
    GamePadState controller1State;
    GamePadState controller2State;
    #endregion
    public enum CamStates
    {
        Locked,          // Locked to other player
        Free,            // Free to move wherever
        Jumping         //jumping whenever character is jumping
    }
    public CamStates CamState
    {
        get
        {
            return this.camState;
        }
    }


    // Use this for initialization
    void Start()
    {
        thisChar = CameraTarget.root.GetComponent<Character>();
        rotationY = -40f;

    }

    void Update()
    {
        controller1State = GamePad.GetState(playerIndex);
        controller2State = GamePad.GetState(player2Index);
        float locked = 0;
        if (thisChar.getPNum().ToString() == "Player")
        {

            locked = controller1State.Triggers.Right;
            //var hPositive = jInput.GetAxis (Mapper.InputArray [12]);
            //var hNegative = jInput.GetAxis (Mapper.InputArray [13]);
			x = controller1State.ThumbSticks.Right.X;
            //var vPositive = jInput.GetAxis (Mapper.InputArray [7]);
            //var vNegative = jInput.GetAxis (Mapper.InputArray [6]);
            y = controller1State.ThumbSticks.Right.Y;
        }
        if (thisChar.getPNum().ToString() == "Player2")
        {
            locked = controller2State.Triggers.Right;
            //var hPositive = jInput.GetAxis (Mapper.InputArray2p [12]);
            //var hNegative = jInput.GetAxis (Mapper.InputArray2p [13]);
            x = controller2State.ThumbSticks.Right.X;
            //var vPositive = jInput.GetAxis (Mapper.InputArray2p [7]);
            //var vNegative = jInput.GetAxis (Mapper.InputArray2p [6]);
            y = controller2State.ThumbSticks.Right.Y;
		}
		//Debug.Log("locked" + locked);
        if (thisChar.isCharacterJumping())
        {
            //Debug.Log("jumping");
            camState = CamStates.Jumping;
        }
        else if (locked > 0.3)
        {
            camState = CamStates.Locked;
        }
        
        else
        {
			canLock = true;
            camState = CamStates.Free;
        }
    }

    void LateUpdate()
    {
        if (camState == CamStates.Locked)
        {
            float canLockVal = thisChar.angles();
            if (canLock)
            {


                if (canLockVal <= 50)
                {
                    thisChar.transform.LookAt(thisChar.getOpponentTransform());
                    //canLock = false;
                }

            }
                if (canLockVal > 50)
                {
                    canLock = false;
                }
			}
           
               
        

            CameraTarget.root.Rotate(Vector3.up * x * controllerSensitivityX * Time.deltaTime);
           
                rotationY += y * controllerSensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            
            //}
        
    }


}
