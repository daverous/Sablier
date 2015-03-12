using UnityEngine;
using System.Collections;


//https://www.youtube.com/watch?v=TicipSVT-T8
public class CameraScript : MonoBehaviour
{
    public Transform CameraTarget;
    private float x = 0.0f;
    private float y = 0.0f;


    public int ZoomRate = 20;
    private float lerpRate = 0.01f;
    public float distance = 8f;
    public float jumpCamHeight = 10f;
    public float controllerSensitivityX = 200f;
    public float controllerSensitivityY = 100f;
    private CamStates camState;
    float verticalLookRotation;
    Character thisChar;
    Vector2 rotationSpeed = new Vector2( 100, 100 );

    public float cameraTargetHeight = 4.0f;
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
    //checks if first person mode is on
    //stores cameras distance from player

    // Use this for initialization
    void Start()
    {
        thisChar = CameraTarget.root.GetComponent<Character>();
        //Vector3 Angles = transform.eulerAngles;
        //x = Angles.x;
        //y = Angles.y;
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
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void LateUpdate()
    {
        //if (camState == CamStates.Jumping)
        //{
        ////    Transform planetTrans = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        ////    Vector3 pos = planetTrans.position - transform.position * jumpCamHeight;
        ////var newRot = Quaternion.LookRotation(pos);
        ////transform.rotation = Quaternion.Lerp(transform.rotation, newRot, lerpRate);
        //    //transform.LookAt(planetTrans);
        //}
        //else
        //{
            // Rotate the camera
            //Vector2 camRotation = Vector2.zero;
            //camRotation = new Vector2(x, y);
            //camRotation.x *= rotationSpeed.x;
            //camRotation.y *= rotationSpeed.y;
            //camRotation *= Time.deltaTime;
            

            //// Rotate the character around world-y using x-axis of joystick
            //CameraTarget.root.Rotate(0, x, 0, Space.World);
        CameraTarget.root.Rotate(Vector3.up * x * controllerSensitivityX * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, CameraTarget.rotation, lerpRate);
        //verticalLookRotation += y * controllerSensitivityY * Time.deltaTime;
        //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -40, 40);
        transform.localEulerAngles = Vector3.right * 40;
        //Vector3 follow = CameraTarget.root.position + CameraTarget.root.up * cameraTargetHeight - CameraTarget.root.forward * distance;
            //transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime * lerpRate);
            //transform.LookAt(CameraTarget);
            //transform.Rotate(-camRotation.y, -camRotation.x, 0);

            if (camState == CamStates.Locked)
            {
                //Debug.Log("LOCKED CAM ");
                transform.LookAt(thisChar.getOpponentTransform());
            }

            else
            {
                //Debug.Log("UNLOCKED CAM ");
            }
        //}
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
