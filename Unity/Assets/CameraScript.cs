using UnityEngine;
using System.Collections;





public class CameraScript : MonoBehaviour
{
    public GameObject character;
    private Transform charTransform;
    public float height = 5f;
    public float distance = 5f;
    public float speed = 5f;
    private float vertLock;
    private CamStates camState;
    float zAxisValue;
    float xAxisValue;
    Character thisChar;

    #region properties
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
    #endregion
    void Start()
    {
        charTransform = character.transform;
        camState = CamStates.Free;
        thisChar = character.transform.root.GetComponent<Character>();
        //vertLock = this.transform.position.y + height;
    }

    void Update()
    {
        float locked =0;
#region get input
        if (thisChar.getPNum().ToString() == "Player") {
            locked = Input.GetAxis("Lock1");
            xAxisValue = Input.GetAxis("CameraHor1");
             zAxisValue = Input.GetAxis("CameraVer1");
        }
        if (thisChar.getPNum().ToString() == "Player2") {
            xAxisValue = Input.GetAxis("CameraHor2");
            zAxisValue = Input.GetAxis("CameraVer2");
            locked = Input.GetAxis("Lock2");
        }
#endregion
        //change states
        if (thisChar.isCharacterJumping())
        {
            camState = CamStates.Jumping;
        }
        if (locked == 1)
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
        Vector3 targetPoint;
        Vector3 follow;
        // TODO      Pull values from controller/keyboard to check if Cam state should be Locked
        switch (CamState)
        {
            case CamStates.Free:
                     targetPoint = charTransform.position;
                    //you need to rotate the camera around the center of the sphere, the opposite of rot        
                     follow = targetPoint + charTransform.up * height - charTransform.forward * distance;
                    transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime * speed);
                    //transform.position = new Vector3(this.transform.position.x, vertLock, this.transform.position.z);
                    moveCamera();
                    transform.LookAt(targetPoint);
                     break;
            case CamStates.Locked:
                Vector3 targetPoint2 = thisChar.getOpponentTransform().position;
                     targetPoint = charTransform.position;
                    //you need to rotate the camera around the center of the sphere, the opposite of rot        
                     follow = targetPoint + charTransform.up * height - charTransform.forward * distance;
                    transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime * speed);
                    transform.LookAt(targetPoint2);
                break;
            case CamStates.Jumping:

                break;
            default:
                break;

        }
        // make camera look at center of sphere
        //rotatingCamera.transform.LookAt(transform.position);
    }


    //TODO add correct axis to this
    private void moveCamera()
    {
        
        if (Camera.current != null)
        {
            Camera.current.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
        }
    }

}