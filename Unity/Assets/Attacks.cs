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
        private float heavyCost = .1f;
        private float powerCost = 0.5f;
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

				if (animator.GetCurrentAnimatorStateInfo(0).IsName("PipeBlade|Taunt")||
		    		animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Taunt")||
		    		animator.GetAnimatorTransitionInfo (0).IsName ("SkyBlade|Taunt -> SkyBlade|IdleAtSide"))
				{
					GetComponent<Collider>().enabled = false;
					GetComponent<Collider>().enabled = false;
					GameObject.Find ("Flame").GetComponent<Collider>().enabled = false;
				}
				else
				{	
					GetComponent<Collider>().enabled = true;
					GameObject.Find ("Flame").GetComponent<Collider>().enabled = true;
				}
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
		
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavyEndF")||
		   			animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavyEndF")){
					animator.SetBool("Aerial", false);
				}
		
				if (thisCharacterTag == "Player") {
						#region player1 
						if (controller1State.Triggers.Right > 0) {

                            if (thisCharacter.CharPowerBar > heavyCost)
                            {
                                thisCharacter.CharPowerBar -= heavyCost;
                                performHeavyAttack();
                            }
						}

//			Perform quick 
			else if (controller1State.Buttons.X == ButtonState.Pressed) {
								performQuickAttack ();

						} else if (controller1State.Buttons.X == ButtonState.Released) {
								//curAttack = AttackType.Empty;
                
								animator.SetBool ("Chain", false);
						}

						if (controller1State.Triggers.Left > 0) {

								if (thisCharacter.getCharPowerBar () >= 1 && canPowerMove) {
//					(Time.deltaTime/3+0.01f)
										canPowerMove = false;	
										thisCharacter.setCharPowerBar (0);
										performPowerMove ();
//					isPowerMoving = true;
				
								}
						}
                        if (controller1State.Triggers.Left == 0)
                        {
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
						if (controller2State.Triggers.Right > 0)
                        {

                            if (thisCharacter.CharPowerBar > heavyCost)
                            {
                                thisCharacter.CharPowerBar -= heavyCost;
                                performHeavyAttack();
                            }
						} else if (controller2State.Buttons.X == ButtonState.Pressed) {
								performQuickAttack ();
						} else if (controller2State.Buttons.X == ButtonState.Released) {
								//curAttack = AttackType.Empty;
								animator.SetBool ("Chain", false);
						}

						if (controller2State.Triggers.Left > 0) {
				
								if (thisCharacter.getCharPowerBar () >= powerCost && canPowerMove) {
										//					(Time.deltaTime/3+0.01f)
										canPowerMove = false;
                                        thisCharacter.CharPowerBar -= powerCost;
										performPowerMove ();
										//					isPowerMoving = true;
					
								}
						}
                        if (controller2State.Triggers.Left == 0)
                        {
								if (isPowerMoving) {
										rb.velocity = Vector3.zero;
								}
								temp = 0;
								isPowerMoving = false;
								canPowerMove = true;
				
						}

						if(animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Heavy")){
							animator.SetBool("Heavy", false);
						}
				}




				#endregion
				
				//Animation Attack Assignment
				if(animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack") ||
				   animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward") ||
				   animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickSecond") ||
				   animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
				   animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") ||
				   animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickBack") ||
		   			animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DashForward"))
				{
					thisCharacter.setCurrentAttack (Character.AttackType.Quick);
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Heavy") ||
				   	animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy") ||
		   			animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavyEndF") ||
				   	animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy") ||
				   	animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavyEndF") ||
				   	animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy"))
				{
					thisCharacter.setCurrentAttack (Character.AttackType.Heavy);
				}







				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DamageHeavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|DamageHeavy")) {
						animator.SetBool ("Damaged", false);
				}
				if (!(animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy") ||
		      			animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickBack") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|DamageHeavy") ||
		  			    animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Heavy") ||
					    animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
					    animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") ||
		    			animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DamageHeavy")||
		     			animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DashForward")||
		      			animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy")||
		   				animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavyEndF"))) {
						
						animator.applyRootMotion = false;
				}
				if (thisCharacterTag == ("Player") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavyEndF") &&
		    			!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") &&
		    			!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickBack") &&
		    			!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DashForward")&&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide")) {
						thisCharacter.resetCurrentAttack ();
				}
				if (thisCharacterTag == ("Player2") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|Heavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack") &&
		    			!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickSecond") &&
						!animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward")) {
						thisCharacter.resetCurrentAttack ();
				}
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickBack")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|Heavy")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|DamageHeavy")||
		   		 	animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DamageHeavy")) {
						animator.applyRootMotion = true;
				}

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack")) {
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
            	if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|AerialHeavy")||
		   			animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy")){
					animator.SetBool("Aerial", true);
				}
				if ((other.collider.tag == "Weapon")) {
						source.PlayOneShot (swordClashSound, volume);
						thisCharacter.setWeaponHitToTrue ();
				}

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
						animator.applyRootMotion = true;
						source.PlayOneShot (swordSwipeSound, volume);
				}    
		}
				
		private void performQuickAttack ()
		{
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavy")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|AerialHeavyEndF")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavyEndF")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickBack")||
		    		animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|AerialHeavy")){
					animator.applyRootMotion = true;
				}
				thisCharacter.setCurrentAttack (Character.AttackType.Quick);
				animator.SetBool ("Attacking", true);
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickFromSide") ||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|QuickOverShoulder") ||
		   				animator.GetCurrentAnimatorStateInfo (0).IsName ("SkyBlade|DashForward") ||
		    			animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickBack")||
						animator.GetCurrentAnimatorStateInfo (0).IsName ("PipeBlade|QuickForward")) {
						source.PlayOneShot (swordSwipeSound, volume);
						animator.SetBool ("Chain", true);
						animator.applyRootMotion = true;

				}          
		}
	
		private void performPowerMove ()
		{

				thisCharacter.startPowerParticle ();
				thisCharacter.turnCharToFaceOpponent ();
				thisCharacter.setCurrentAttack (Character.AttackType.Power);
				Vector3 startPoint = transform.root.position;
            Transform opp = thisCharacter.getOpponentTransform ();
				Vector3 endPoint = opp.position+(opp.forward*5);
				transform.root.position = endPoint;
				//animator.SetBool ("Heavy", true);
//						Vector3 dir = (endPoint - startPoint);
//						rb.velocity = dir;        
		}
}
