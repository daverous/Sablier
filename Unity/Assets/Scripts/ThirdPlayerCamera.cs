using UnityEngine;
using System.Collections;


struct CameraPosition
{
    private Vector3 position;
    private Transform xForm;
    public Vector3 Position { get { return position; } set { position = value; } }
    public Transform XForm { get { return xForm; } set { xForm = value; } }

    public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
    {
        position = pos;
        xForm = transform;
        xForm.name = camName;
        xForm.parent = parent;
        xForm.localPosition = Vector3.zero;
        xForm.localPosition = position;
    }
}

public class ThirdPlayerCamera : MonoBehaviour {

    #region fields
    private GameObject player;
    private Transform playerTarget;
    public float damping = 1;
    private Vector3 offset;
    public float distance = 5f;
    public float height = 5f;
    public float speed = 3f;
    private float vertLock;
    #endregion


    #region function
    void Start() {
        player = GameObject.FindWithTag ("Player");
        playerTarget = player.transform;
        offset = transform.position - player.transform.position;
        vertLock = this.transform.position.y + height;
    }   
       	void LateUpdate () {
            Vector3 targetPosition = playerTarget.position + playerTarget.up * height - playerTarget.forward * distance;
            Debug.DrawRay(playerTarget.position, playerTarget.up * height, Color.red);
            Debug.DrawRay(playerTarget.position, -1f * playerTarget.forward * distance, Color.blue);
            Debug.DrawLine(playerTarget.position, targetPosition, Color.green);

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime*speed);
		transform.position =  new Vector3(this.transform.position.x, vertLock, this.transform.position.z);
        transform.LookAt(playerTarget);
	}
}

#endregion

