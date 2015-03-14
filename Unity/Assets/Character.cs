﻿using UnityEngine;
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



	public Image VisualHealth1;
	public Image VisualHealth2;
	public Image VisualHealth3;
	public Image VisualHealth4;
	public Text hn1;
	public Text hn2;
	public Text hn3;
	public Text hn4;

	public Text hit1;
	public Text hit2;

    private float curhealth = 100f;
    private int hits;
    private bool dead; 
    private float maxVelocity = 20f;
    private float moveSpeed = 15f;
    private bool inRange; 
    public float maxHealth = 100f;
    private float comboPower = 0f;

    private float horizontal = 0.0f;
    private float vertical = 0.0f;
	private float horizontal2 = 0.0f;
	private float vertical2 = 0.0f;
    public float Weight;
    public float jumpForce = 0.3f;

	//Sarah: Adding a public for the running clip//

	public AudioClip runningSound;
    public AudioClip swordClangSound;
    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;
   
    public Vector3 moveDirection;
    private Transform opponent; //Transform for opponent 
    private bool isGrounded = true;
    private playerNum pNum;
    private playerNum opponentName;
    private bool isJumping;
	public bool isBlocking = false;
    private bool isMoving;
    #endregion

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

  
    #region Function
    void Start()
    {
        isJumping = false;
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
            isJumping = false;
            if (isMoving) {
            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            float hitVol = other.relativeVelocity.magnitude * velToVol;
            if (other.relativeVelocity.magnitude < velocityClipSplit)
                source.PlayOneShot(runningSound, hitVol);
        }
        }
    }

    public bool isCharacterGrounded()
    {
        return isGrounded;

    }

    public Transform getOpponentTransform() {
        return opponent;
    }

    public bool isCharacterJumping()
    {
        return isJumping;
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
    public void setCharNotGrounded()
    {
        isGrounded = false;
    }
    public void beenHit(float damage)
    {
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(swordClangSound);
		curhealth -= damage;

		if (curhealth <= 0)
		{
			dead = true;
            GameManager gm = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<GameManager>();
            //increment wins of other player
            if (gameObject.tag == "Player2")
            {
                gm.IncrementPlayerOneWins();
            }
            if (gameObject.tag == "Player")
            {
                gm.IncrementPlayerTwoWins();
            }
            Application.LoadLevel("GameOverScene");
		}

		double temp = damage * 0.010;
		float damage_value = (float)temp;
		if (gameObject.tag == "Player") {
			VisualHealth1.fillAmount = VisualHealth1.fillAmount - damage_value;
			VisualHealth3.fillAmount = VisualHealth3.fillAmount - damage_value;
			hn1.text = "HEALTH:"+curhealth;
			hn3.text = "HEALTH:"+curhealth;
		} else if (gameObject.tag == "Player2") {
			VisualHealth2.fillAmount = VisualHealth2.fillAmount - damage_value;
			VisualHealth4.fillAmount = VisualHealth4.fillAmount - damage_value;
			hn2.text = "HEALTH:"+curhealth;
			hn4.text = "HEALTH:"+curhealth;
		}

        
    }

    public playerNum getOpponentName()
    {
        return opponentName;
    }

    public void incrementHits()
    {
        Debug.Log("here");
        if (comboPower <= 90)
        {
            comboPower += 10;
        }
        else
        {
            comboPower = 100;
        }
        Debug.Log("hits" + hits);
        hits++;
    }

    void Update()
    {
		//horizontal = 0;

		
     // TODO add different axis for each controller
		
//		this.transform.LookAt(opponent.position);
        if (gameObject.tag == "Player")
        {

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            if (horizontal != 0 || vertical!=0) {
                isMoving = true;
            }
            moveDirection = new Vector3(horizontal, 0, vertical).normalized;
            if (Input.GetAxis("Jump1") == 1 && isGrounded)
            {
                isJumping = true;
                Rigidbody rb = GameObject.FindGameObjectWithTag(gameObject.tag).GetComponent<Rigidbody>();
                //GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
                Vector3 jumpVec = rb.transform.position - new Vector3(0, 0, 0);
                //rb.transform.position += jumpVec * Time.deltaTime * 5;
                rb.velocity = jumpVec * jumpForce;
            }

			if (Input.GetAxis("P1Blocking") == 1)
			{
				isBlocking = true;
				Debug.Log("Blocking true");
			}
			else if (Input.GetAxis("P1Blocking") == 0)
			{
				isBlocking = false;
				Debug.Log("Blocking false");
			}
		
			
        }
        else if (gameObject.tag == "Player2")
        {
            
			horizontal2 = Input.GetAxis("Horizontal2");
            vertical2 = Input.GetAxis("Vertical2");
            moveDirection = new Vector3(horizontal2, 0, vertical2).normalized;


            if (Input.GetAxis("Jump2") == 1 && isGrounded)
            {
                isJumping = true;
                Rigidbody rb = GameObject.FindGameObjectWithTag(gameObject.tag).GetComponent<Rigidbody>();
                //GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
                Vector3 jumpVec = rb.transform.position - new Vector3(0, 0, 0);
                //rb.transform.position += jumpVec * Time.deltaTime * 5;
                rb.velocity = jumpVec * jumpForce;
            }

			if (Input.GetAxis("P2Blocking") == 1)
			{
				isBlocking = true;
			}
			else if (Input.GetAxis("P2Blocking") == 0)
			{
				isBlocking = false;
			}
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
        //Debug.Log(pNum+": "+moveDirection);
    }

    public playerNum getPNum()
    {
        return pNum;
    }

    public Rigidbody getRigidBody()
    {
        return GameObject.FindObjectOfType<Rigidbody>();
    }
    public float getComboVal()
    {
        return comboPower;
    }
    public bool isDead()
    {
        return dead;
    }

}
    #endregion