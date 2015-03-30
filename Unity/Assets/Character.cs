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
		private float CharPowerBar = 0.0f;

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
		public bool isJumping;
		private bool isBlocking;
		private bool isMoving;

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
    #endregion



		public enum AttackType
		{
				Quick,
				Heavy,
				Power,
				Empty,
				Reduced
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
		public float turnCharToFaceOpponentNew ()
		{    
//		if (lerpTime >= 0.2f) {
//			Debug.Log ("reset");
//			lerpTime = 0;
//				}

				lerpTime += Time.deltaTime * 0.1f;
				Quaternion targetRotation = Quaternion.LookRotation (getOpponentTransform ().position - transform.root.position);
				float str = Mathf.Min (10 * Time.deltaTime, 1);
				float angle = Quaternion.Angle (transform.root.rotation, Quaternion.Slerp (transform.root.rotation, targetRotation, str));
				//if (angle > 1) {
						
				transform.root.rotation = Quaternion.Lerp (transform.root.rotation, targetRotation, 0.5f);
				//}
				return angle;
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
				if (other.collider.name == "Hand.L_end" && !hitWeapon) {
						if (detectOpponentMovement ())
								Debug.Log ("here");

               
						switch (this.getCurrentAttack ()) {
						case Character.AttackType.Empty:
								break;
						case Character.AttackType.Reduced:
								this.thisOpponent.beenHit (reducedAttackDamage);
                            // As attacks are reduced, no hits are counted. 
                            //thisCharacter.incrementHits();
								break;
						case Character.AttackType.Heavy:
								this.thisOpponent.beenHit (heavyAttackDamage);
								this.incrementHits ();
								break;
						case Character.AttackType.Power:
								break;
						//GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);                  
						case Character.AttackType.Quick:
                            //Rigidbody rb = GameObject.FindGameObjectWithTag(this.getOpponentName().ToString()).GetComponent<Rigidbody>();
                            //rb.AddForce(0, 5, 10);
								this.thisOpponent.beenHit (quickAttackDamage);
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
		
				return (opponent_animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") || opponent_animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder"));
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
		public void beenHit (float damage)
		{
				StartCoroutine (displayBlood ());
				//
				curhealth -= damage;
				if (curhealth <= 0) {
						dead = true;
						GameManager gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
						//increment wins of other player


						//int currentround = staticstore.getnumberofrounds();
						
						////check if its only one round that has been played and return the winner.
						//if (currentround == 1) {
						//    if (gameobject.tag == "player2") {
						//        gm.incrementplayeronewins ();
						//        application.loadlevel ("roundwonplayer1");
						//    }
						//    if (gameobject.tag == "player") {
						//        gm.incrementplayertwowins ();
						//        application.loadlevel ("roundwonplayer2");
						//    }
						//} 

						//if (currentround == 3){
						//    if (gameobject.tag == "player2") {
						//            gm.incrementplayeronewins ();
						//            application.loadlevel ("gameoverfinalplayer1");
						//    }
						//    if (gameobject.tag == "player") {
						//            gm.incrementplayertwowins ();
						//            application.loadlevel ("gameoverfinalplayer2");
						//    }
						//}

						if (gameObject.tag == "Player2") {
								gm.IncrementPlayerOneWins ();
								Application.LoadLevel ("GameOverFinalPlayer1");
						}
						if (gameObject.tag == "Player") {
								gm.IncrementPlayerTwoWins ();
								Application.LoadLevel ("GameOverFinalPlayer2");
						}

						//Application.LoadLevel ("GameOverFinalPlayer1");

						int currentRound = StaticStore.getNumberOfRounds ();
						
//						//Check if its only one round that has been played and return the winner.
//						if (currentRound == 1) {
//							if (gameObject.tag == "Player2") {
//								gm.IncrementPlayerOneWins ();
//								Application.LoadLevel ("RoundWonPlayer1");
//							}
//							if (gameObject.tag == "Player") {
//								gm.IncrementPlayerTwoWins ();
//								Application.LoadLevel ("RoundWonPlayer2");
//							}
//						} 

						//	if (currentRound == 3){
						if (gameObject.tag == "Player2") {
								gm.IncrementPlayerOneWins ();
								Application.LoadLevel ("GameOverFinalPlayer1");
						}
						if (gameObject.tag == "Player") {
								gm.IncrementPlayerTwoWins ();
								Application.LoadLevel ("GameOverFinalPlayer2");
						}
						//	}

						//Application.LoadLevel ("GOScene");

				}
				animator.SetBool ("Damaged", true);
				

				animator.applyRootMotion = true;
				source.pitch = Random.Range (lowPitchRange, highPitchRange);
				source.PlayOneShot (swordHitSound);
				if (gameObject.tag == "Player2") {
						GameObject.FindGameObjectWithTag ("PlayerTwoCamera").GetComponent<CameraShakeScript> ().startShake ();
				}
				if (gameObject.tag == "Player") {
						GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShakeScript> ().startShake ();
				}
				if (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Damaged")) {
						Time.timeScale = 0.00001f;
				} else {
						Time.timeScale = 1;
				}
				//DisplayBubble();
				
				//DisplayBubble();
				double temp = damage * 0.010;
				float damage_value = (float)temp;
				if (gameObject.tag == "Player") {
						VisualHealth1.fillAmount = VisualHealth1.fillAmount - damage_value;
						VisualHealth3.fillAmount = VisualHealth3.fillAmount - damage_value;
						hn1.text = "HEALTH:" + curhealth;
						hn3.text = "HEALTH:" + curhealth;
						//Display Bubbles
						Bubble1.fillAmount = 1;
						Bubble4.fillAmount = 1;
						StartCoroutine ("timer");

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
						Bubble6.fillAmount = 1;
						Bubble8.fillAmount = 1;
						StartCoroutine ("timer");

		
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
				//Debug.DrawLine(this.transform.position, opponent.transform.position, Color.red);
				controller1State = GamePad.GetState (playerIndex);
				controller2State = GamePad.GetState (player2Index);
				if (gameObject.tag == "Player") {
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

//			performJump
						if (controller1State.Buttons.A == ButtonState.Pressed && isGrounded) {
//								Debug.Log ("jump");
								performJump ();
						}

//			performBlock
						if (controller1State.Buttons.B == ButtonState.Pressed) {
								isBlocking = true;
								turnCharToFaceOpponentNew ();
//				Debug.Log("Blocking true");
						} else if (controller1State.Buttons.B == ButtonState.Released) {
								isBlocking = false;
//				Debug.Log("Blocking false");
						}

						CharPowerBar = CharPowerBar + Time.deltaTime / 30;
						if (CharPowerBar >= 1)
								CharPowerBar = 1;
						PowerBar1.fillAmount = CharPowerBar;
						PowerBar3.fillAmount = CharPowerBar;
						UpdatePowerBarColor (PowerBar1);
						UpdatePowerBarColor (PowerBar3);
			
				}
				if (gameObject.tag == "Player2") {
            
						//var hPositive = jInput.GetAxis (Mapper.InputArray2p [0]);
						//var hNegative = jInput.GetAxis (Mapper.InputArray2p [10]);
						horizontal2 = controller2State.ThumbSticks.Left.X;
		
						//            horizontal = Input.GetAxis("Horizontal");
						//var vPositive = jInput.GetAxis (Mapper.InputArray2p [1]);
						//var vNegative = jInput.GetAxis (Mapper.InputArray2p [11]);
						vertical2 = controller2State.ThumbSticks.Left.Y;
						moveDirection = new Vector3 (horizontal2, 0, vertical2).normalized;

			 
						if (controller2State.Buttons.A == ButtonState.Pressed && isGrounded) {
								performJump ();
						}

						if (controller2State.Buttons.B == ButtonState.Pressed) {
								turnCharToFaceOpponentNew ();
								isBlocking = true;
						} else if (controller2State.Buttons.B == ButtonState.Released) {
								isBlocking = false;
						}
						CharPowerBar = CharPowerBar + Time.deltaTime / 30;
						if (CharPowerBar >= 1)
								CharPowerBar = 1;
						PowerBar2.fillAmount = CharPowerBar;
						PowerBar4.fillAmount = CharPowerBar;
						UpdatePowerBarColor (PowerBar2);
						UpdatePowerBarColor (PowerBar4);
			
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