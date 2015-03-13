using UnityEngine;
using System.Collections;

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
        float locked = 0;
        if (thisChar.getPNum().ToString() == "Player")
        {

            locked = Input.GetAxis("Lock1");
            x = Input.GetAxis("CameraHor1");
            y = Input.GetAxis("CameraVer1") ;
        }
        if (thisChar.getPNum().ToString() == "Player2")
        {
            x = Input.GetAxis("CameraHor2");
            y = Input.GetAxis("CameraVer2");
            locked = Input.GetAxis("Lock2");
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
            camState = CamStates.Free;
        }
    }

    void LateUpdate()
    {
        if (camState == CamStates.Locked)
        {
            //Debug.Log("LOCKED CAM ");
            Quaternion targetRotation = Quaternion.LookRotation(thisChar.getOpponentTransform().position - CameraTarget.root.position);
            Debug.Log(targetRotation);
            float str = Mathf.Min(2 * Time.deltaTime, 1);
            if (CameraTarget.root.rotation != Quaternion.Lerp(CameraTarget.root.rotation, targetRotation, str))
            {
                CameraTarget.root.rotation = Quaternion.Lerp(CameraTarget.root.rotation, targetRotation, str);
            }
        }

            CameraTarget.root.Rotate(Vector3.up * x * controllerSensitivityX * Time.deltaTime);
           
                rotationY += y * controllerSensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            
            //}
        
    }

}
