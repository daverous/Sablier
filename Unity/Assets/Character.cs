using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public float Damage;
    public float TurnSpeed;
<<<<<<< HEAD
   
=======



	public Image VisualHealth;

>>>>>>> a52d5698ca3c67807d9a902271e903c5f33eee51
    private float curhealth;
    private int hits;
    private bool dead; 
    private float maxVelocity = 20f;
    private float moveSpeed = 15f;
    private bool inRange; 
    public float maxHealth = 100f;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
	private float horizontal2 = 0.0f;
	private float vertical2 = 0.0f;
    public float Weight;
    public float jumpForce = 350f;

	private Animator animator;
   
    public Vector3 moveDirection;
    private Transform opponent; //Transform for opponent 
    private bool isGrounded = true;
    private playerNum pNum;
    private playerNum opponentName;
    #endregion



  
    #region Function
    void Start()
    {
        dead = false;

        if (gameObject.tag == "Player")
        {
            opponent = GameObject.FindWithTag("Player2").transform;
            pNum = playerNum.Player;
            opponentName = playerNum.Player2;

        }
        else if (gameObject.tag == "Player2")
        {
            opponent = GameObject.FindWithTag("Player").transform;
            pNum = playerNum.Player2;
            opponentName = playerNum.Player;
        }
        // inits cur health as max health
        curhealth = maxHealth;

<<<<<<< HEAD
		animator = GetComponent<Animator>();
=======


>>>>>>> a52d5698ca3c67807d9a902271e903c5f33eee51
    }






    public float getCurHealthAsPercentage()
    {
        return 100*(curhealth / maxHealth);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "planet")
        {
			//Debug.Log("hit ground");
            isGrounded = true;
        }
		if (other.gameObject.name == "Player" || other.gameObject.name == "Player2") {
			Debug.Log("hit others");
			VisualHealth.fillAmount = VisualHealth.fillAmount - 0.1f;

		}
        
    }

    public bool isCharacterGrounded()
    {
        return isGrounded;

    }


    public int getHits()
    {
        return hits;
    }
    void OnCollisionExit(Collision info)
    {
        if (info.collider.name == "planet")
        {
            isGrounded = false;
        }

    }

    public void beenHit(float damage)
    {
        curhealth -= damage;
        if (curhealth <= 0)
        {
            dead = true;
        }
    }

    public playerNum getOpponentName()
    {
        return opponentName;
    }

    public void incrementHits()
    {
        hits++;
    }

    void Update()
    {
		//horizontal = 0;

		
     // TODO add different axis for each controller
		animator.SetLookAtPosition(opponent.position);
//		this.transform.LookAt(opponent.position);
        if (gameObject.tag == "Player")
        {

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            moveDirection = new Vector3(horizontal, 0, vertical).normalized;
            if (Input.GetAxis("Jump 1") == 1 && isGrounded)
            {
                //GetComponent<Rigidbody>().AddForce (0, jumpForce, 0);
                Vector3 jumpVec = GetComponent<Rigidbody>().transform.position - new Vector3(0, 0, 0);
                //Vector3 jumpVec = new Vector3(0, jumpForce, 0);
                //Vector3 jumpVec = this.transform.position - pl
                GetComponent<Rigidbody>().transform.position += jumpVec * Time.deltaTime * 5;
            }
//			Attack Animations
			if(Input.GetButtonDown("QuickAttack1")){
				animator.SetBool("Attacking", true);
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide")||
				   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
					animator.SetBool("Chain", true);
			}
			if(Input.GetButtonUp("QuickAttack1")){
				animator.SetBool("Chain", false);
			}
		
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide")||
			   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder")){
				animator.SetBool("Attacking", false);
				if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f&&
				   animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f){
					animator.SetBool("Chain", false);
				}
			}
        }
        else if (gameObject.tag == "Player2")
        {
            horizontal2 = Input.GetAxis("Horizontal2");
            vertical2 = Input.GetAxis("Vertical2");
            moveDirection = new Vector3(horizontal2, 0, vertical2).normalized;


            if (Input.GetAxis("Jump 2") == 1 && isGrounded)
            {
                //GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
                Vector3 jumpVec = GetComponent<Rigidbody>().transform.position - new Vector3(0, 0, 0);
                //Vector3 jumpVec = new Vector3(0, jumpForce, 0);
                //Vector3 jumpVec = this.transform.position - pl
				//Vector3 jumpVec = rigidbody.transform.position - new Vector3(0, 0, 0);
				GetComponent<Rigidbody>().transform.position += jumpVec * Time.deltaTime * 5;
            }
			//			Attack Animations
			if(Input.GetButtonDown("QuickAttack2")){
				animator.SetBool("Attacking", true);
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide")||
				   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
					animator.SetBool("Chain", true);
			}
			if(Input.GetButtonUp("QuickAttack2")){
				animator.SetBool("Chain", false);
			}
			
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide")||
			   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder")){
				animator.SetBool("Attacking", false);
				if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f&&
				   animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f){
					animator.SetBool("Chain", false);
				}
			}
        }
//		Run Animations
		if(moveDirection != Vector3.zero){
			animator.SetBool("Running", true);
		}
		if(moveDirection == Vector3.zero){
			animator.SetBool("Running", false);
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

			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
		Debug.Log(pNum+": "+moveDirection);
    }

    public playerNum getPNum()
    {
        return pNum;
    }

}
    #endregion