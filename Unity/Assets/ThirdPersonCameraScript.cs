using UnityEngine;
using System.Collections;





public class ThirdPlayerCamera : MonoBehaviour
{
    public GameObject character;
    private Transform charTransform;
    public float height = 5f;
    public float distance = 5f;
    public float speed = 1f;
    private float vertLock;
    private CamStates camState;

    #region properties
    public enum CamStates
    {
        Locked,          // Locked to other player
        Free            // Free to move wherever
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
        //vertLock = this.transform.position.y + height;
    }
    void LateUpdate()
    {
        // TODO      Pull values from controller/keyboard to check if Cam state should be Locked
        switch (CamState)
        {
            case CamStates.Free:
                Vector3 targetPoint = charTransform.position;
                //you need to rotate the camera around the center of the sphere, the opposite of rot        
                Vector3 follow = targetPoint + charTransform.up * height - charTransform.forward * distance;
                transform.position = Vector3.Lerp(transform.position, follow, Time.deltaTime * speed);
                //transform.position = new Vector3(this.transform.position.x, vertLock, this.transform.position.z);

                transform.LookAt(targetPoint);
                break;
            default:
                break;

        }
        // make camera look at center of sphere
        //rotatingCamera.transform.LookAt(transform.position);
    }


}