using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform CameraTarget;
    private float x = 0.0f;
    private float y = 0.0f;


    public int ZoomRate = 20;
    private int lerpRate = 5;
    public float distance = 8f;
    private float currentDistance;

    private CamStates camState;
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
    private bool click = false;
    //stores cameras distance from player

    // Use this for initialization
    void Start()
    {
        thisChar = CameraTarget.root.GetComponent<Character>();
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
        currentDistance = distance;
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
            Debug.Log("jumping");
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
        if (camState == CamStates.Jumping)
        {
            Vector3 follow = CameraTarget.position + CameraTarget.up * cameraTargetHeight - CameraTarget.forward * distance;
            transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime * lerpRate);  
        }
        else
        {
            // Rotate the camera
            Vector2 camRotation = Vector2.zero;
            camRotation = new Vector2(x, y);
            camRotation.x *= rotationSpeed.x;
            camRotation.y *= rotationSpeed.y;
            camRotation *= Time.deltaTime;

            //// Rotate the character around world-y using x-axis of joystick
            CameraTarget.root.Rotate(0, camRotation.x, 0, Space.World);
            //// Rotate only the camera with y-axis input
            transform.Rotate(-camRotation.y, -camRotation.x, 0);

           // HEEEEEEEEEEEEEEEEEEEEEEEERRRRRRRRRRRRRREEEEE
            //could be here:  Vector3 rotation = new Vector3(Camera.main.transform.eulerAngles.x + Input.touches[touchNum].deltaPosition.y * Time.deltaTime,
            //                                 Camera.main.transform.eulerAngles.y + Input.touches[touchNum].deltaPosition.x * Time.deltaTime, 0);


            // HEEEEEEEEEEEEEEEEEEEEEEEERRRRRRRRRRRRRREEEEE
             //Camera.main.transform.localEulerAngles = rotation;

            //float targetRotantionAngle = CameraTarget.eulerAngles.y;
            //float cameraRotationAngle = transform.eulerAngles.y;
            //x = Mathf.LerpAngle(cameraRotationAngle, targetRotantionAngle, lerpRate * Time.deltaTime);

            //y = ClampAngle(y, -15, 25);
            //Quaternion rotation = Quaternion.Euler(y, x, 0);

            //Vector3 position = CameraTarget.position - (rotation * Vector3.forward * distance);
            //Vector3 cameraTargetPosition = new Vector3(CameraTarget.position.x, CameraTarget.position.y + cameraTargetHeight, CameraTarget.position.z);
            //position = CameraTarget.position - (rotation * Vector3.forward * currentDistance + new Vector3(0, -cameraTargetHeight, 0));

            //transform.rotation = rotation;
            //transform.position = position;

            //you need to rotate the camera around the center of the sphere, the opposite of rot        
            Vector3 follow = CameraTarget.position + CameraTarget.up * cameraTargetHeight - CameraTarget.forward * distance;
            transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime * lerpRate);
            //transform.position = new Vector3(this.transform.position.x, vertLock, this.transform.position.z);
            //float targetRotantionAngle = CameraTarget.eulerAngles.y;
            //float cameraRotationAngle = transform.eulerAngles.y;
            //float x = Mathf.LerpAngle(cameraRotationAngle, targetRotantionAngle, lerpRate * Time.deltaTime);
            transform.LookAt(CameraTarget);

            if (camState == CamStates.Locked)
            {
                //Debug.Log("LOCKED CAM ");
                transform.LookAt(thisChar.getOpponentTransform());
            }

            else
            {
                //Debug.Log("UNLOCKED CAM ");
            }
        }
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
