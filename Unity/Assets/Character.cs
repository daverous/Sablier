using UnityEngine;
using System.Collections;

#region structs
public enum playerNum
{
    Player,
    Player2
}

#endregion
public class Character : MonoBehaviour
{

    #region Vars
    public int Health;
    public float Damage;
    public float TurnSpeed;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    public float Weight;
    public float jumpForce = 500f;
    private float maxVelocity = 20f;
    private float moveSpeed = 15f;
    private Vector3 moveDirection;
    private Transform opponent; //Transform for opponent 
    public bool isGrounded = true;
    private playerNum pNum;
    #endregion


   
    #region Function
    void Start()
    {

        if (gameObject.tag == "Player")
        {
            opponent = GameObject.FindWithTag("Player2").transform;
            pNum = playerNum.Player;
        }
        else if (gameObject.tag == "Player2")
        {
            opponent = GameObject.FindWithTag("Player").transform;
            pNum = playerNum.Player2;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.name);
        if (other.collider.name == "planet")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision info)
    {
        if (info.collider.name == "planet")
        {
            isGrounded = false;
        }
    }

    void Update()
    {

     // TODO add different axis for each controller
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //rigidbody.AddForce (0, jumpForce, 0);
            Vector3 jumpVec = rigidbody.transform.position - new Vector3(0, 0, 0);
            //Vector3 jumpVec = new Vector3(0, jumpForce, 0);
            //Vector3 jumpVec = this.transform.position - pl
            rigidbody.transform.position += jumpVec * Time.deltaTime * 5;
        }
    }

    void FixedUpdate()
    {


        #region testcode
        //transform.LookAt(opponent);
        //if (vertical >= 0)
        //{
        //    this.transform.position = Vector3.Lerp(this.transform.position, opponent.position, vertical * (moveSpeed / 4) * Time.deltaTime);
        //}
        //if (vertical < 0)
        //{
        //    //this.transform.position = Vector3.Lerp(this.transform.position,this.transform.position+(Vector3.back*runSpeed*Time.deltaTime),vertical*(runSpeed/3)*Time.deltaTime);
        //    this.transform.Translate(Vector3.back * (moveSpeed / 2 * Time.deltaTime));
        //    //rigidbody.AddForce(0,0,vertical*runSpeed*Time.deltaTime);
        //}
        //transform.RotateAround(opponent.position, Vector3.up, -1 * horizontal * (moveSpeed) * Time.deltaTime);
        ////		}
        ////rigidbody.AddForce(0,0,vertical*runSpeed);
        //if (rigidbody.velocity.magnitude > maxVelocity && isGrounded)
        //{
        //    rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
        //}
        //transform.RotateAround(opponent.position, Vector3.up, -1 * horizontal * (moveSpeed) * Time.deltaTime);
        #endregion

        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);

    }

    public playerNum getPNum()
    {
        return pNum;
    }

}
    #endregion