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
	public float CharPowerBar = 0.0f;


	public Image VisualHealth1;
	public Image VisualHealth2;
	public Image VisualHealth3;
	public Image VisualHealth4;
	public Image PowerBar1;
	public Image PowerBar2;
	public Image PowerBar3;
	public Image PowerBar4;
	public Text hn1;
	public Text hn2;
	public Text hn3;
	public Text hn4;

    private float curhealth;
    private int hits;
    private bool dead; 
    public float moveSpeed = 15f;
    private bool inRange; 
    public float maxHealth = 100f;
    private float comboPower;

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
    private bool isGrounded;
    private playerNum pNum;
    private playerNum opponentName;
    private bool isJumping;
	private bool isBlocking;
    private bool isMoving;
    #endregion

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

  
    #region Functions
    void Start()
    {
        comboPower = 0f;
		hits = 0;
        isGrounded = true;
        curhealth = 100f;
        isBlocking = false;
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

		ColorInit ();

    }

    public void turnCharToFaceOpponent()
    {
        Quaternion targetRotation = Quaternion.LookRotation(getOpponentTransform().position - transform.root.position);
        float str = Mathf.Min(2 * Time.deltaTime, 1);
        if (transform.root.rotation != Quaternion.Lerp(transform.root.rotation, targetRotation, str))
        {
            transform.root.rotation = Quaternion.Lerp(transform.root.rotation, targetRotation, str);
        }
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

	public bool isCharacterBlocking()
	{
		return isBlocking;
		
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
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
			Debug.Log(VisualHealth2.fillAmount);
			UpdateHealthColor(VisualHealth1);
			UpdateHealthColor(VisualHealth2);
			UpdateHealthColor(VisualHealth3);
			UpdateHealthColor(VisualHealth4);
		}
    }

	public void ColorInit(){
		VisualHealth1.color = new Color32 (2, 251, 96, 255);
		VisualHealth2.color = new Color32 (2, 251, 96, 255);
		VisualHealth3.color = new Color32 (2, 251, 96, 255);
		VisualHealth4.color = new Color32 (2, 251, 96, 255);
	}

	public void UpdateHealthColor(Image healthbar){
		if (healthbar.fillAmount < 0.9f && healthbar.fillAmount > 0.7f)
			healthbar.color = new Color32 (65, 238, 131, 194);
		else if (healthbar.fillAmount < 0.7f && healthbar.fillAmount >0.5f)
			healthbar.color = new Color32 (211, 251, 98, 255);
		else if(healthbar.fillAmount < 0.5f && healthbar.fillAmount > 0.3f)
			healthbar.color = new Color32 (201, 232, 91, 230);
		else if(healthbar.fillAmount < 0.3f)
			healthbar.color = new Color32 (255, 9, 96, 230);
	}

    public playerNum getOpponentName()
    {
        return opponentName;
    }

    public void incrementHits()
    {
        if (comboPower <= 90)
        {
            comboPower += 10;
        }
        else
        {
            comboPower = 100;
        }
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
                performJump();
            }

			if (Input.GetAxis("P1Blocking") == 1)
			{
				isBlocking = true;
				turnCharToFaceOpponent();
//				Debug.Log("Blocking true");
			}
			else if (Input.GetAxis("P1Blocking") == 0)
			{
				isBlocking = false;
//				Debug.Log("Blocking false");
			}
			CharPowerBar = CharPowerBar + Time.deltaTime/30;
			PowerBar1.fillAmount = CharPowerBar;
			PowerBar3.fillAmount = CharPowerBar;
			UpdatePowerBarColor(PowerBar1);
			UpdatePowerBarColor(PowerBar3);
			
        }
        else if (gameObject.tag == "Player2")
        {
            
			horizontal2 = Input.GetAxis("Horizontal2");
            vertical2 = Input.GetAxis("Vertical2");
            moveDirection = new Vector3(horizontal2, 0, vertical2).normalized;


            if (Input.GetAxis("Jump2") == 1 && isGrounded)
            {
                performJump();
            }

			if (Input.GetAxis("P2Blocking") == 1)
			{
				turnCharToFaceOpponent();
				isBlocking = true;
			}
			else if (Input.GetAxis("P2Blocking") == 0)
			{
				isBlocking = false;
			}
			CharPowerBar = CharPowerBar + Time.deltaTime/30;
			PowerBar2.fillAmount = CharPowerBar;
			PowerBar4.fillAmount = CharPowerBar;
			UpdatePowerBarColor(PowerBar2);
			UpdatePowerBarColor(PowerBar4);
			
        }

    }

	public void UpdatePowerBarColor(Image powerbar){
		if (powerbar.fillAmount < 0.9f && powerbar.fillAmount > 0.7f)
			powerbar.color = new Color32 (231, 41, 41, 255);
		else if (powerbar.fillAmount < 0.7f && powerbar.fillAmount >0.5f)
			powerbar.color = new Color32 (242, 16, 123, 255);
		else if(powerbar.fillAmount < 0.5f && powerbar.fillAmount > 0.3f)
			powerbar.color = new Color32 (211, 242, 16, 255);
		else if(powerbar.fillAmount < 0.3f)
			powerbar.color = new Color32 (94, 255, 215, 255);

	}

    void FixedUpdate()
    {
			GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
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

    public bool isCharBlocking()
    {
        return isBlocking;
    }

    void performJump()
    {
        isJumping = true;
        Rigidbody rb = GameObject.FindGameObjectWithTag(gameObject.tag).GetComponent<Rigidbody>();
        //GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
        Vector3 jumpVec = rb.transform.position - new Vector3(0, 0, 0);
        //rb.transform.position += jumpVec * Time.deltaTime * 5;
        rb.velocity = jumpVec * jumpForce;
    }

}
    #endregion