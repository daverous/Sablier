using UnityEngine;
using System.Collections;
using XInputDotNetPure;
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
    public int rotateSpeed = 10;
		public float CharPowerBar = 0.0f;
        private float blockCost = 0.005f;
		public GameObject blood;
		public GameObject powerParticle;
		public Image VisualHealth1;
		public Image VisualHealth2;
		public Image VisualHealth3;
		public Image VisualHealth4;
		public Image PowerBar1;
		public Image PowerBar2;
		public Image PowerBar3;
		public Image PowerBar4;
		public Image Bubble1;
		public Image Bubble2;
		public Image Bubble3;
		public Image Bubble4;
		public Image Bubble5;
		public Image Bubble6;
		public Image Bubble7;
		public Image Bubble8;
		public Text hn1;
		public Text hn2;
		public Text hn3;
		public Text hn4;
		private bool hitWeapon;
		private float curhealth;
		private int hits;
		private bool dead;
		public float moveSpeed = 15f;
		private bool inRange;
		public float maxHealth = 100f;
		private int baddiesKilled = 0;
		private string thisCharacterTag;
		private float horizontal = 0.0f;
		private float vertical = 0.0f;
		private float horizontal2 = 0.0f;
		private float vertical2 = 0.0f;
		public float Weight;
		public float jumpForce = 0.3f;
		public AudioClip blockSound;
		public AudioClip runningSound;
		public AudioClip swordHitSound;
		public AudioClip BunnyKillsound;
		private AudioSource source;
		private float lowPitchRange = .75F;
		private float highPitchRange = 1.5F;
		private float velToVol = .2F;
		private float velocityClipSplit = 10F;
		public Vector3 moveDirection;
		private Transform opponent; //Transform for opponent 
		[SerializeField]
		private bool isGrounded;
		private playerNum pNum;
		private playerNum opponentName;
		public bool isJumping;
		private bool isBlocking;
		private bool isMoving;
		private bool canMove;
		private bool canBlock;
		PlayerIndex playerIndex = (PlayerIndex)0;
		PlayerIndex player2Index = (PlayerIndex)1;
		GamePadState controller1State;
		GamePadState controller2State;
		private Animator animator;
		float lerpTime = 0;
		private AttackType curAttack;
		private Character thisOpponent;
		private string thisOpponentTag;
		private Animator opponent_animator;
		public float quickAttackDamage = 5f;
		public float heavyAttackDamage = 10f;
		public float powerMoveSpeed = 10f;
		public float reducedAttackDamage = 2f;
        private bool startedBlock;
    #endregion



		public enum AttackType
		{
				Quick,
				Heavy,
				Power,
				Empty,
				Reduced
		}


		public void incrementBaddiesKilled ()
		{
				baddiesKilled++;
		}

		public void incrementBaddiesKilledByAmount (int val)
		{
				baddiesKilled += val;
		}

		public int getBaddiesKilled ()
		{
				return baddiesKilled;
		}

		public AttackType getCurrentAttack ()
		{
				return curAttack;
		}

		public void resetCurrentAttack ()
		{
				curAttack = AttackType.Empty;
		}

		public void setCurrentAttack (AttackType t)
		{
				curAttack = t;
		}

		public void setWeaponHitToTrue ()
		{
				hitWeapon = true;
		}

		public void setWeaponHitToFalse ()
		{
				hitWeapon = false;
		}

		void Awake ()
		{

				source = GetComponent<AudioSource> ();

		}

  
    #region Functions
		void Start ()
		{
        
				thisCharacterTag = transform.root.tag;
				hits = 0;
				isGrounded = true;
				curhealth = 100f;
				isBlocking = false;
				isJumping = false;
				dead = false;
				canMove = true;
				animator = GameObject.FindGameObjectWithTag (thisCharacterTag).GetComponent<Animator> ();

				if (gameObject.tag == "Player") {
						opponent = GameObject.FindWithTag ("Player2").transform;
						pNum = playerNum.Player;
						opponentName = playerNum.Player2;
						thisOpponent = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Character> ();
						opponent_animator = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Animator> ();
				} else if (gameObject.tag == "Player2") {
						opponent = GameObject.FindWithTag ("Player").transform;
						pNum = playerNum.Player2;
						opponentName = playerNum.Player;
						thisOpponent = GameObject.FindGameObjectWithTag ("Player").GetComponent<Character> ();
						opponent_animator = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
				}
				// inits cur health as max health
				curhealth = maxHealth;

				ColorInit ();
				Physics.IgnoreLayerCollision (8, 9, true);


		}

		public float angles ()
		{
				// Get a copy of your forward vector
				Vector3 forward = transform.up;
				// Zero out the y component of your forward vector to only get the direction in the X,Z plane
				forward.z = 0;
				float headingAngle = Quaternion.LookRotation (forward).eulerAngles.y;

				Vector3 otherForward = getOpponentTransform ().up;
				otherForward.z = 0;
				float heading2Angle = Quaternion.LookRotation (otherForward).eulerAngles.y;
				float temp = headingAngle - heading2Angle;
            

				temp = Vector3.Distance (transform.position, getOpponentTransform ().position);

				return temp;

				//Quaternion targetRotation = Quaternion.LookRotation(getOpponentTransform().position - transform.root.position);
				//float str = Mathf.Min(10 * Time.deltaTime, 1);

				//float angle = Quaternion.Angle(transform.root.rotation, Quaternion.Slerp(transform.root.rotation, targetRotation, str));
				//Debug.Log(angle);
				//return angle;
		}

		public void turnCharToFaceOpponent ()
		{    

				Quaternion targetRotation = Quaternion.LookRotation (getOpponentTransform ().position - transform.root.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
		}

		// Return value is if character has been fully rotated
		public void setCharPowerBar (float set)
		{
				CharPowerBar = set;	
		}

		public float getCharPowerBar ()
		{
				return CharPowerBar;
		}

		public float getCurHealthAsPercentage ()
		{
				return 100 * (curhealth / maxHealth);
		}

		void OnCollisionEnter (Collision other)
		{		

				if (other.collider.tag == "PowerUp") {
						float pu = other.gameObject.GetComponent<PowerUpScript> ().getPowerUpAmount ();
						if (CharPowerBar <= 1 - pu) {
								CharPowerBar += pu;
						} else {
								CharPowerBar = 1;
						}
//			TODO neeed PowerUp noiseupdateHealth(damage);
						Destroy (other.gameObject);
				}

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy")) {

						animator.SetBool ("Aerial", true);
				}
				if (other.collider.tag == "HealthUp") {
						float pu = other.gameObject.GetComponent<PowerUpScript> ().getPowerUpAmount ();
                        

						if (curhealth <= 100 - pu) {
								curhealth += pu;
						} else {
								curhealth = 100;
						}
						//Debug.Log("curhealth" + curhealth);
						updateHealth (pu * -1);
						//			TODO neeed PowerUp noise
						Destroy (other.gameObject);
				}
				if (other.collider.name == "planet") {
						isGrounded = true;
						isJumping = false;
						
						if (isMoving) {
								source.pitch = Random.Range (lowPitchRange, highPitchRange);
								float hitVol = other.relativeVelocity.magnitude * velToVol;
								if (other.relativeVelocity.magnitude < velocityClipSplit)
										source.PlayOneShot (runningSound, hitVol);
						}
				}
				//Hit bad guy
				if (other.collider.tag == "Scorch") {
					StartCoroutine (vibrateTimer (0.2f));
					beenHit (5, opponent.gameObject);
				}
				
				if (other.collider.transform.root.tag == "Baddy" && curAttack != AttackType.Empty) {
						StartCoroutine (vibrateTimer (0.2f));
						Destroy (other.gameObject);
						incrementBaddiesKilled ();
						source.PlayOneShot (BunnyKillsound);
				}
			
				if (other.collider.name == "Hand.L_end" && !hitWeapon) {
                    if (curAttack != AttackType.Empty)
                    {
                        if (thisOpponent.isBlocking)
                        {
                            curAttack = AttackType.Reduced;
                        }
                    }   
//                        if (detectOpponentMovement ())
////								Debug.Log ("here");

               
						switch (this.getCurrentAttack ()) {
						case Character.AttackType.Empty:
								break;
						case Character.AttackType.Reduced:
                                this.thisOpponent.beenHit(reducedAttackDamage, this.gameObject);
                            // As attacks are reduced, no hits are counted. 
                            //thisCharacter.incrementHits();
								break;
						case Character.AttackType.Heavy:
								StartCoroutine (vibrateTimer (0.3f));
                                
								this.thisOpponent.beenHit (heavyAttackDamage, this.gameObject);
								this.incrementHits ();
								break;
						case Character.AttackType.Power:
								break;
						//GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);                  
						case Character.AttackType.Quick:
								StartCoroutine (vibrateTimer (0.2f));
								Rigidbody rb = GameObject.FindGameObjectWithTag (this.getOpponentName ().ToString ()).GetComponent<Rigidbody> ();
								rb.AddForce (0, 5, 10);
								this.thisOpponent.beenHit (quickAttackDamage,  this.gameObject);
								this.incrementHits ();
                            //Vector3 direction = Ray.direction;       
                            //hit.rigidbody.AddForce(Ray.direction * force);
								break;
						default:
								break;

						}
						//curAttack = AttackType.Empty;
                
				}
		}

		bool detectOpponentMovement ()
		{
		
				return (opponent_animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") || 
						opponent_animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
						opponent_animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DashForward"));
		}

		public bool isCharacterBlocking ()
		{
				return isBlocking;
		
		}

		public bool isCharacterGrounded ()
		{
				return isGrounded;

		}

		public Transform getOpponentTransform ()
		{
				return opponent;
		}

		public bool isCharacterJumping ()
		{
				return isJumping;
		}

		public int getHits ()
		{
				return hits;
		}

		void OnCollisionExit (Collision info)
		{
				if (info.collider.name == "planet") {
						isGrounded = false;
						
				}

		}

		public void setCharNotGrounded ()
		{
				isGrounded = false;
		}

		IEnumerator displayPowerParticle ()
		{
				powerParticle.SetActive (true);
		
				yield return new WaitForSeconds (2f);
				powerParticle.SetActive (false);
		}

		IEnumerator displayBlood ()
		{
				blood.SetActive (true);
        
				yield return new WaitForSeconds (1.5f);
				blood.SetActive (false);
		}

		public void startPowerParticle ()
		{
				StartCoroutine (displayPowerParticle ());
		}

		public void beenHit (float damage, GameObject hurter)
		{
				StartCoroutine (displayBlood ());
				//

				curhealth -= damage;
				if (curhealth <= 0) {
						dead = true;
						GameManager gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
						thisOpponent.incrementBaddiesKilledByAmount (10);
                        
						if (gameObject.tag == "Player2") {
                            
								if (baddiesKilled <= thisOpponent.getBaddiesKilled ()) {
										gm.IncrementPlayerOneWins ();
								}
						}
						if (gameObject.tag == "Player") {
								if (baddiesKilled <= thisOpponent.getBaddiesKilled ()) {
										gm.IncrementPlayerTwoWins ();
								}
						}

				}
				transform.LookAt(hurter.transform);
				animator.SetBool ("Damaged", true);
				

				animator.applyRootMotion = true;
				source.pitch = Random.Range (lowPitchRange, highPitchRange);
				source.PlayOneShot (swordHitSound);
                if (isBlocking)
                {
                    source.PlayOneShot(blockSound);
                    StartCoroutine(vibrateTimer(0.5f));

                }
                else
                {
                    if (gameObject.tag == "Player2")
                    {
                        GameObject.FindGameObjectWithTag("PlayerTwoCamera").GetComponent<CameraShakeScript>().startShake();
                    }
                    if (gameObject.tag == "Player")
                    {
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>().startShake();
                    }
                    if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
                    {
                        Time.timeScale = 0.00001f;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                }
				//DisplayBubble();
				
				//DisplayBubble();
				updateHealth (damage);
		}

		void updateHealth (float damage)
		{
				double temp = damage * 0.010;
				float damage_value = (float)temp;
				if (gameObject.tag == "Player") {
						VisualHealth1.fillAmount = VisualHealth1.fillAmount - damage_value;
						VisualHealth3.fillAmount = VisualHealth3.fillAmount - damage_value;
						hn1.text = "HEALTH:" + curhealth;
						hn3.text = "HEALTH:" + curhealth;
						//Display Bubbles
						if (damage > 0) {
								Bubble1.fillAmount = 1;
								Bubble4.fillAmount = 1;
								StartCoroutine ("timer");
						}

				} else if (gameObject.tag == "Player2") {
						VisualHealth2.fillAmount = VisualHealth2.fillAmount - damage_value;
						VisualHealth4.fillAmount = VisualHealth4.fillAmount - damage_value;
						hn2.text = "HEALTH:" + curhealth;
						hn4.text = "HEALTH:" + curhealth;
						//			Debug.Log(VisualHealth2.fillAmount);
						UpdateHealthColor (VisualHealth1);
						UpdateHealthColor (VisualHealth2);
						UpdateHealthColor (VisualHealth3);
						UpdateHealthColor (VisualHealth4);
						//Display bubbles
						if (damage > 0) {
								Bubble6.fillAmount = 1;
								Bubble8.fillAmount = 1;
								StartCoroutine ("timer");
						}

				}
		}

		IEnumerator timer ()
		{
				yield return new WaitForSeconds (1);
				if (gameObject.tag == "Player") {
						Bubble1.fillAmount = 0;
						Bubble4.fillAmount = 0;
				} else if (gameObject.tag == "Player2") {
						Bubble6.fillAmount = 0;
						Bubble8.fillAmount = 0;
				}
		}

		IEnumerator vibrateTimer (float time)
		{
            
				PlayerIndex ind = (PlayerIndex)0;
				if (gameObject.tag == "Player") {
						ind = playerIndex;
				} else if (gameObject.tag == "Player2") {
						ind = player2Index;
				}
				GamePad.SetVibration (ind, 0.5f, 0.5f);
				yield return new WaitForSeconds (time);
				GamePad.SetVibration (ind, 0, 0);
		}

		public void ColorInit ()
		{
				VisualHealth1.color = new Color32 (2, 251, 96, 255);
				VisualHealth2.color = new Color32 (2, 251, 96, 255);
				VisualHealth3.color = new Color32 (2, 251, 96, 255);
				VisualHealth4.color = new Color32 (2, 251, 96, 255);
		}

		public void UpdateHealthColor (Image healthbar)
		{
				if (healthbar.fillAmount < 0.9f && healthbar.fillAmount > 0.7f)
						healthbar.color = new Color32 (65, 238, 131, 194);
				else if (healthbar.fillAmount < 0.7f && healthbar.fillAmount > 0.5f)
						healthbar.color = new Color32 (211, 251, 98, 255);
				else if (healthbar.fillAmount < 0.5f && healthbar.fillAmount > 0.3f)
						healthbar.color = new Color32 (201, 232, 91, 230);
				else if (healthbar.fillAmount < 0.3f)
						healthbar.color = new Color32 (255, 9, 96, 230);
		}

		public playerNum getOpponentName ()
		{
				return opponentName;
		}

		public void incrementHits ()
		{
				if (CharPowerBar <= 1) {
						CharPowerBar += 0.01f;
				} else {
						CharPowerBar = 1;
				}
				hits++;
		}

		void Update ()
		{
				if(moveDirection.z == -1)
				{
					animator.SetBool("BackStepping", true);	
				} 
				if(moveDirection.z > -1)
				{
					animator.SetBool("BackStepping", false);	
				} 
				if(moveDirection.x == -1)
				{
					animator.SetFloat("ClockWork", 9);	
				} 
				if(moveDirection.x == 1)
				{
					animator.SetFloat("ClockWork", 3);	
				} 
				if(isGrounded){
					animator.SetBool ("Airborne", false);
				}
				else
				{
					animator.SetBool ("Airborne", true);
				}
				//Debug.DrawLine(this.transform.position, opponent.transform.position, Color.red);
				controller1State = GamePad.GetState (playerIndex);
				controller2State = GamePad.GetState (player2Index);
				
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Skyblade|Taunt")||
					animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Taunt")){
						turnCharToFaceOpponent();
				}

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Taunt") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Taunt") ||
					    animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward") ||
					    animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack") ||
					    animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Block") ||
					    animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Block") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy")) 
				{
						canMove = false;
					
				} else {
						canMove = true;
				}

				if (!canMove) {
						moveDirection = Vector3.zero.normalized;
				}

				if (gameObject.tag == "Player") {
						if (canMove) {
								//var hPositive = jInput.GetAxis (Mapper.InputArray [0]);
								//var hNegative = jInput.GetAxis (Mapper.InputArray [10]);
								horizontal = controller1State.ThumbSticks.Left.X;
								//var vPositive = jInput.GetAxis (Mapper.InputArray [1]);
								//var vNegative = jInput.GetAxis (Mapper.InputArray [11]);
								vertical = controller1State.ThumbSticks.Left.Y;
								if (horizontal != 0 || vertical != 0) {
										isMoving = true;
								}
								moveDirection = new Vector3 (horizontal, 0, vertical).normalized;
						}
//			performJump
						if (controller1State.Buttons.A == ButtonState.Pressed && isGrounded) {
//								Debug.Log ("jump");
								performJump ();
						}

                        if (controller1State.Buttons.LeftShoulder == ButtonState.Pressed &&
							!(animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|DashBack")))
                        {	
							animator.applyRootMotion = true;
							animator.SetBool("bDash", true);
                        }

						if (controller1State.Buttons.RightShoulder == ButtonState.Pressed &&
						    !(animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|DashForward")))
						{
							animator.applyRootMotion = true;
							animator.SetBool("fDash", true);
						}
                        if (controller1State.Buttons.Y == ButtonState.Pressed)
                        {
							animator.SetBool("Taunt", true);
                        }
						if (controller1State.Buttons.LeftShoulder == ButtonState.Released ||
						    animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|DashBack"))
						{
							animator.SetBool("bDash", false);
						}
						
						if (controller1State.Buttons.RightShoulder == ButtonState.Released||
						    animator.GetCurrentAnimatorStateInfo (0).IsName("SkyBlade|DashForward"))
						{
							animator.SetBool("fDash", false);
						}
						if (controller1State.Buttons.Y == ButtonState.Released)
						{
							animator.SetBool("Taunt", false);
						}


//			performBlock
						if (controller1State.Buttons.B == ButtonState.Pressed) {
                            if (CharPowerBar > blockCost)
                            {
                                startedBlock = true;
                            } 
                                if (startedBlock)
                                {
                                    if (CharPowerBar < blockCost)
                                    {
                                        startedBlock = false;
                                    }
                                    isBlocking = true;
                                    
                                    CharPowerBar -= blockCost;
                                    turnCharToFaceOpponent();
                                    animator.SetBool("Blocking", true);

                                    animator.SetBool("Blocking", true);

                                    canMove = false;
                                }
                        
//				Debug.Log("Blocking true");
						} 
                    if (controller1State.Buttons.B == ButtonState.Released || !startedBlock) {
								isBlocking = false;
								animator.SetBool ("Blocking", false);
								canMove = true;
//				Debug.Log("Blocking false");
						}

						CharPowerBar = CharPowerBar + Time.deltaTime / 30;
						if (CharPowerBar >= 1)
								CharPowerBar = 1;
						PowerBar1.fillAmount = CharPowerBar;
						PowerBar3.fillAmount = CharPowerBar;
						UpdatePowerBarColor (PowerBar1);
						UpdatePowerBarColor (PowerBar4);
			
				}
				if (gameObject.tag == "Player2") {
						if (canMove) {
								//var hPositive = jInput.GetAxis (Mapper.InputArray2p [0]);
								//var hNegative = jInput.GetAxis (Mapper.InputArray2p [10]);
								horizontal2 = controller2State.ThumbSticks.Left.X;
			
								//            horizontal = Input.GetAxis("Horizontal");
								//var vPositive = jInput.GetAxis (Mapper.InputArray2p [1]);
								//var vNegative = jInput.GetAxis (Mapper.InputArray2p [11]);
								vertical2 = controller2State.ThumbSticks.Left.Y;
								moveDirection = new Vector3 (horizontal2, 0, vertical2).normalized;

						}
						if (controller2State.Buttons.A == ButtonState.Pressed && isGrounded) {
								performJump ();
						}

						if (controller2State.Buttons.LeftShoulder == ButtonState.Pressed &&
						    !animator.GetCurrentAnimatorStateInfo (0).IsName("PipeBlade|DashBack"))
						{
							animator.SetBool("bDash", true);
						}
						
						if (controller2State.Buttons.RightShoulder == ButtonState.Pressed &&
						    !animator.GetCurrentAnimatorStateInfo (0).IsName("PipeBlade|DashForward"))
						{
							animator.SetBool("fDash", true);
						}
						if (controller2State.Buttons.Y == ButtonState.Pressed)
						{
							animator.SetBool("Taunt", true);
						}
						if (controller2State.Buttons.LeftShoulder == ButtonState.Released ||
						    animator.GetCurrentAnimatorStateInfo (0).IsName("PipeBlade|DashBack"))
						{
							animator.SetBool("bDash", false);
						}
						
						if (controller2State.Buttons.RightShoulder == ButtonState.Released ||
						    animator.GetCurrentAnimatorStateInfo (0).IsName("PipeBlade|DashForward"))
						{
							animator.SetBool("fDash", false);
						}
						if (controller2State.Buttons.Y == ButtonState.Released)
						{
							animator.SetBool("Taunt", false);
						}
						if (controller2State.Buttons.B == ButtonState.Pressed) {
                            if (CharPowerBar > blockCost)
                            {
                                startedBlock = true;
                            }
                            if (startedBlock)
                            {
                                if (CharPowerBar < blockCost)
                                {
                                    startedBlock = false;
                                }
                                isBlocking = true;

                                CharPowerBar -= blockCost;
                                turnCharToFaceOpponent();
                                animator.SetBool("Blocking", true);

                                animator.SetBool("Blocking", true);

                                canMove = false;
                            }
                        }
                        else if (controller2State.Buttons.B == ButtonState.Released || !startedBlock)
                        {
								animator.SetBool ("Blocking", false);
								isBlocking = false;
								canMove = true;
						}
						CharPowerBar = CharPowerBar + Time.deltaTime / 30;
						if (CharPowerBar >= 1)
								CharPowerBar = 1;
						PowerBar2.fillAmount = CharPowerBar;
						PowerBar4.fillAmount = CharPowerBar;
						UpdatePowerBarColor (PowerBar2);
						UpdatePowerBarColor (PowerBar3);
			
				}

		}

		public void UpdatePowerBarColor (Image powerbar)
		{
				if (powerbar.fillAmount < 0.9f && powerbar.fillAmount > 0.7f)
						powerbar.color = new Color32 (231, 41, 41, 255);
				else if (powerbar.fillAmount < 0.7f && powerbar.fillAmount > 0.5f)
						powerbar.color = new Color32 (242, 16, 123, 255);
				else if (powerbar.fillAmount < 0.5f && powerbar.fillAmount > 0.3f)
						powerbar.color = new Color32 (211, 242, 16, 255);
				else if (powerbar.fillAmount < 0.3f)
						powerbar.color = new Color32 (94, 255, 215, 255);

		}

		void FixedUpdate ()
		{
				GetComponent<Rigidbody> ().MovePosition (GetComponent<Rigidbody> ().position + transform.TransformDirection (moveDirection) * moveSpeed * Time.deltaTime);
		}
		
		public playerNum getPNum ()
		{
				return pNum;
		}

		public Rigidbody getRigidBody ()
		{
				return GameObject.FindObjectOfType<Rigidbody> ();
		}

		public bool isDead ()
		{
				return dead;
		}

		public bool isCharBlocking ()
		{
				return isBlocking;
		}


		public void performJump ()
		{
				isJumping = true;
				Rigidbody rb = GameObject.FindGameObjectWithTag (gameObject.tag).GetComponent<Rigidbody> ();
				//GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
				Vector3 jumpVec = rb.transform.position - new Vector3 (0, 0, 0);
				//rb.transform.position += jumpVec * Time.deltaTime * 5;
				rb.velocity = jumpVec * jumpForce;
		}

}
    #endregion