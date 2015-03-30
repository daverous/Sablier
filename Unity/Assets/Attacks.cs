using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public class Attacks : MonoBehaviour
{
		public AudioClip swordSwipeSound;
		public AudioClip swordClashSound;
		public AudioClip blockSound;

		private AudioSource source; 

		private Character thisCharacter;
		private string thisCharacterTag;
		private Character thisOpponent;


		public float quickAttackDamage = 5f;
		public float heavyAttackDamage = 10f;
		public float powerMoveSpeed = 10f;
		public float reducedAttackDamage = 2f;
		public float volume = 5f;

		private Animator animator;
		private Animator opponent_animator;
		private int collision_trigger = 0;
		// Use this for initialization
		private Vector3 desiredVelocity;
		private float lastSqrMag;
		bool canPowerMove = true;
		bool isPowerMoving = false; 
		Rigidbody rb;

		PlayerIndex playerIndex = (PlayerIndex)0;
		PlayerIndex player2Index = (PlayerIndex)1;
		GamePadState controller1State;
		GamePadState controller2State;

		float temp = 0;

		void Start ()
		{
				source = GetComponent<AudioSource> ();

                
				//inRange = false;
				thisCharacterTag = transform.root.tag;
				thisCharacter = GameObject.FindGameObjectWithTag (thisCharacterTag).GetComponent<Character> ();
				if (gameObject.tag == "Player")
						thisOpponent = GameObject.FindGameObjectWithTag ("Player2").GetComponent<Character> ();
				else
						thisOpponent = GameObject.FindGameObjectWithTag ("Player").GetComponent<Character> ();
				animator = GameObject.FindGameObjectWithTag (thisCharacterTag).GetComponent<Animator> ();
				//opponent_animator = GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ();
				rb = GameObject.FindGameObjectWithTag (thisCharacterTag).GetComponent<Rigidbody> ();
				thisCharacter.resetCurrentAttack ();
		}

		// Update is called once per frame
		void Update ()
		{
				controller1State = GamePad.GetState (playerIndex);
				controller2State = GamePad.GetState (player2Index);
				#region checkMoving
				float sqrMag = (thisCharacter.getOpponentTransform ().position - transform.position).sqrMagnitude;

				if (sqrMag > lastSqrMag) {
						// rigidbody has reached target and is now moving past it
						// stop the rigidbody by setting the velocity to zero
						desiredVelocity = Vector3.zero;
				} 
		
				// make sure you update the lastSqrMag
				lastSqrMag = sqrMag;
		
		
		
				#endregion
				//var vPositive = jInput.GetAxis (Mapper.InputArray [1]);
		        
		
		
		
				if (thisCharacterTag == "Player") {
						#region player1 
						if (controller1State.Buttons.X == ButtonState.Pressed && controller1State.Buttons.LeftShoulder == ButtonState.Pressed) {
//								Debug.Log ("Heavy Attacking");
								performHeavyAttack ();
						}

//			Perform quick 
			else if (controller1State.Buttons.X == ButtonState.Pressed) {
								performQuickAttack ();

						} else if (controller1State.Buttons.X == ButtonState.Released) {
								//curAttack = AttackType.Empty;
                
								animator.SetBool ("Chain", false);
						}

						if (controller1State.Buttons.Y == ButtonState.Pressed) {

								if (thisCharacter.getCharPowerBar () >= 1 && canPowerMove) {
//					(Time.deltaTime/3+0.01f)
										canPowerMove = false;	
										thisCharacter.setCharPowerBar (0);
										performPowerMove ();
//					isPowerMoving = true;
				
								}
						}
						if (controller1State.Buttons.Y == ButtonState.Released) {
								if (isPowerMoving) {
										rb.velocity = Vector3.zero;
								}
								temp = 0;
								isPowerMoving = false;
								canPowerMove = true;

						}
				}
				#endregion
				if (thisCharacterTag == "Player2") {

						#region player2
						if (controller2State.Buttons.X == ButtonState.Pressed && controller2State.Buttons.LeftShoulder == ButtonState.Pressed) {
//								Debug.Log ("Heavy Attacking");
								performHeavyAttack ();
						} else if (controller2State.Buttons.X == ButtonState.Pressed) {
								performQuickAttack ();
						} else if (controller2State.Buttons.X == ButtonState.Released) {
								//curAttack = AttackType.Empty;
								animator.SetBool ("Chain", false);
						}

						if (controller2State.Buttons.Y == ButtonState.Pressed) {
				
								if (thisCharacter.getCharPowerBar () >= 1 && canPowerMove) {
										//					(Time.deltaTime/3+0.01f)
										canPowerMove = false;	
										thisCharacter.setCharPowerBar (0);
										performPowerMove ();
										//					isPowerMoving = true;
					
								}
						}
						if (controller2State.Buttons.Y == ButtonState.Released) {
								if (isPowerMoving) {
										rb.velocity = Vector3.zero;
								}
								temp = 0;
								isPowerMoving = false;
								canPowerMove = true;
				
						}
				}




				#endregion
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DamageHeavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|DamageHeavy")) {
						animator.SetBool ("Damaged", false);
				}
				if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|DamageHeavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DamageHeavy")) {
						animator.applyRootMotion = false;
				}
				if (thisCharacterTag == ("Player") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide")) {
						thisCharacter.resetCurrentAttack ();
				}
				if (thisCharacterTag == ("Player2") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Heavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward")) {
						thisCharacter.resetCurrentAttack ();
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack")) {
						animator.applyRootMotion = true;
				}

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward")) {
						animator.SetBool ("Attacking", false);
						if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0f &&
								animator.GetCurrentAnimatorStateInfo (0).normalizedTime <= 0.1f) {
								animator.SetBool ("Chain", false);
						}
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy")) {
						animator.SetBool ("Heavy", false);	
				}
				//		Run Animations
				if (thisCharacter.moveDirection != Vector3.zero) {
						animator.SetBool ("Running", true);
				}
				if (thisCharacter.moveDirection == Vector3.zero) {
						animator.SetBool ("Running", false);
				}
				// Jump Animations
				if (thisCharacter.isJumping) {
						animator.SetBool ("Jumping", true);
				}
				if (thisCharacter.isJumping == false) {
						animator.SetBool ("Jumping", false);
				}
		}


		void OnCollisionEnter (Collision other)
		{
            
				if ((other.collider.tag == "Weapon")) {
						source.PlayOneShot (swordClashSound, volume);
						thisCharacter.setWeaponHitToTrue ();
				}
				/*if ((other.transform.root.name == thisCharacter.getOpponentName ().ToString ())) {

						//Debug.Log(curAttack.ToString() + thisCharacter.getPNum().ToString());
						collision_trigger = 1;
                        if (GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().isCharBlocking())
                              thisCharacter.setCurrentAttack(Character.AttackType.Reduced);
						switch (thisCharacter.getCurrentAttack()) {
                        case Character.AttackType.Empty:
								break;
                        case Character.AttackType.Reduced:
								GameObject.FindGameObjectWithTag (thisCharacter.getOpponentName ().ToString ()).GetComponent<Character> ().beenHit (reducedAttackDamage);
                    // As attacks are reduced, no hits are counted. 
                    //thisCharacter.incrementHits();
								break;
                        case Character.AttackType.Heavy:
								GameObject.FindGameObjectWithTag (thisCharacter.getOpponentName ().ToString ()).GetComponent<Character> ().beenHit (heavyAttackDamage);
								thisCharacter.incrementHits ();
								break;
                        case Character.AttackType.Power:
								break;
						//GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);                  
                        case Character.AttackType.Quick:
								Rigidbody rb = GameObject.FindGameObjectWithTag (thisCharacter.getOpponentName ().ToString ()).GetComponent<Rigidbody> ();
								rb.AddForce (0, 5, 10);
								GameObject.FindGameObjectWithTag (thisCharacter.getOpponentName ().ToString ()).GetComponent<Character> ().beenHit (quickAttackDamage);
								thisCharacter.incrementHits ();
                    //Vector3 direction = Ray.direction;       
                    //hit.rigidbody.AddForce(Ray.direction * force);
								break;
						default:
								break;

						}
						//curAttack = AttackType.Empty;
				}*/
				

////	
//                if (other.transform.root.tag == "PowerUp") {
//                        PowerUpScript ps = other.gameObject.GetComponent<PowerUpScript> ();
//                        float pbAmount = thisCharacter.getCharPowerBar ();
//                        thisCharacter.setCharPowerBar (pbAmount + ps.getPowerUpAmount ());
//                }
		}

		void OnCollisionExit (Collision other)
		{
				if (other.collider.tag == "Weapon") {
						thisCharacter.setWeaponHitToFalse ();
				}
		}
		private void performHeavyAttack ()
		{
				thisCharacter.setCurrentAttack (Character.AttackType.Heavy);
				animator.applyRootMotion = true;
				animator.SetBool ("Heavy", true);
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy")) {
						source.PlayOneShot (swordSwipeSound, volume);
				}    
		}
		private void performQuickAttack ()
		{
				thisCharacter.setCurrentAttack (Character.AttackType.Quick);
				animator.SetBool ("Attacking", true);
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward")) {
						source.PlayOneShot (swordSwipeSound, volume);
						animator.SetBool ("Chain", true);
				}          
		}

		private void performPowerMove ()
		{

				thisCharacter.startPowerParticle ();
				temp = thisCharacter.turnCharToFaceOpponentNew ();
				thisCharacter.setCurrentAttack (Character.AttackType.Power);
				Vector3 startPoint = transform.root.position;
				Vector3 endPoint = thisCharacter.getOpponentTransform ().position;
//		endPoint.x += 4;
//		endPoint.y += 5;
//		endPoint.z += 4;
				transform.root.position = endPoint;
				//animator.SetBool ("Heavy", true);
//						Vector3 dir = (endPoint - startPoint);
//						rb.velocity = dir;        
		}
}
