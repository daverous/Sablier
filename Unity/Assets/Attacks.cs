using UnityEngine;
using System.Collections;

public class Attacks : MonoBehaviour
{
    public AudioClip swordSwipeSound;
    private AudioSource source; 

    private Character thisCharacter;
    private string thisCharacterTag;
	private Character thisOpponent;


    public float quickAttackDamage = 5f;
    public float heavyAttackDamage = 10f;
    public float powerMoveSpeed = 10f;
	public float reducedAttackDamage = 2f;
    public float volume = 5f;

    private AttackType curAttack;
    private Animator animator;
	private Animator opponent_animator;
	private int collision_trigger = 0;
    // Use this for initialization
	private Vector3 desiredVelocity;
	private float lastSqrMag;
	bool canPowerMove = true;
	bool isPowerMoving = false; 
	Rigidbody rb;
	float temp = 0;
    public enum AttackType
    {
        Quick, Heavy, Power, Empty, Reduced
    }
    void Start()
    {
		source = GetComponent<AudioSource>();

		curAttack = AttackType.Empty;
        //inRange = false;
        thisCharacterTag = transform.root.tag;
        thisCharacter = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Character>();
		if (gameObject.tag == "Player")
			thisOpponent = GameObject.FindGameObjectWithTag("Player2").GetComponent<Character>();
		else
			thisOpponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        animator = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Animator>();
		opponent_animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		rb = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		#region checkMoving
		float sqrMag = (thisCharacter.getOpponentTransform().position- transform.position).sqrMagnitude;

		if ( sqrMag > lastSqrMag )
		{
			// rigidbody has reached target and is now moving past it
			// stop the rigidbody by setting the velocity to zero
			desiredVelocity = Vector3.zero;
		} 
		
		// make sure you update the lastSqrMag
		lastSqrMag = sqrMag;
		
		
		
		#endregion
		
		
		
		
		if (thisCharacterTag == "Player")
        #region player1
        {
			if (jInput.GetButton (Mapper.InputArray [3]) && jInput.GetButton (Mapper.InputArray [2]))
            {
                Debug.Log("Heavy Attacking");
                performHeavyAttack();
            }

//			Perform quick 
			else if (jInput.GetButton (Mapper.InputArray [2]))
            {
                performQuickAttack();

            }
           
			else if (!jInput.GetButton (Mapper.InputArray [2]))
            {
                //curAttack = AttackType.Empty;
                
                animator.SetBool("Chain", false);
            }

			if (jInput.GetButton (Mapper.InputArray [4]))
            {

				if (thisCharacter.CharPowerBar <= 0) {
					canPowerMove = false;
					if (isPowerMoving) {
						rb.velocity = Vector3.zero;
					}
					isPowerMoving = false;
				}
				else if (thisCharacter.CharPowerBar > 0 && canPowerMove){
//					(Time.deltaTime/3+0.01f)
					if (!isPowerMoving) {
						thisCharacter.performJump();
						isPowerMoving = true;
					}
					else {
					thisCharacter.CharPowerBar = thisCharacter.CharPowerBar - 0.004f ;
					performPowerMove();
					isPowerMoving = true;
					}
				}
            }
			if (!jInput.GetButton (Mapper.InputArray [4])) {
				if (isPowerMoving) {
					rb.velocity = Vector3.zero;
				}
				temp = 0;
				isPowerMoving = false;
				canPowerMove = true;

        }
		}
        #endregion
        if (thisCharacterTag == "Player2")
        {

            #region player2
			if (jInput.GetButton (Mapper.InputArray2p [3]) && jInput.GetButton (Mapper.InputArray2p [2]))
            {
                Debug.Log("Heavy Attacking");
                performHeavyAttack();
            }

			else if (jInput.GetButton (Mapper.InputArray2p [2]))
            {
                performQuickAttack();
            }

			else if (!jInput.GetButton (Mapper.InputArray2p [2]))
            {
                //curAttack = AttackType.Empty;
                animator.SetBool("Chain", false);
            }

//			performPowerMove
			if (jInput.GetButton (Mapper.InputArray2p [4]))
        {
			if (thisCharacter.CharPowerBar > 50){
				thisCharacter.CharPowerBar = thisCharacter.CharPowerBar - Time.deltaTime/5;
				performPowerMove();
			}
        }




            #endregion
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") ||
               animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
        {
            animator.SetBool("Attacking", false);
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.1f)
            {
                animator.SetBool("Chain", false);
            }
        }
        //		Run Animations
        if (thisCharacter.moveDirection != Vector3.zero)
        {
            animator.SetBool("Running", true);
        }
        if (thisCharacter.moveDirection == Vector3.zero)
        {
            animator.SetBool("Running", false);
        }
    }

	void OnCollisionEnter(Collision other)
    {
		if ((other.transform.root.name == "Player" || other.transform.root.name == "Player2") && (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") || animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder")) && collision_trigger == 0)
        {
            //Debug.Log(curAttack.ToString() + thisCharacter.getPNum().ToString());
			collision_trigger = 1;
            if (GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().isCharBlocking())
				curAttack = AttackType.Reduced;

            switch (curAttack)
            {
                case AttackType.Empty:
                    break;
				case AttackType.Reduced:
					GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(reducedAttackDamage);
                    // As attacks are reduced, no hits are counted. 
                    //thisCharacter.incrementHits();
					break;
                case AttackType.Heavy:
                 
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(heavyAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                case AttackType.Power:
                    break;
                    //GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);                  
                case AttackType.Quick:
                    Rigidbody rb = GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Rigidbody>();
                    rb.AddForce(0,5,10);
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
                    thisCharacter.incrementHits();
                    //Vector3 direction = Ray.direction;       
                    //hit.rigidbody.AddForce(Ray.direction * force);
                    break;
                default:
                    break;

            }
            //curAttack = AttackType.Empty;
        }
    }

    void OnCollisionExit(Collision info)
    {
		collision_trigger = 0;
        if (info.collider.name == thisCharacter.getOpponentName().ToString())
        {
            //inRange = false;
        }
    }

    private void performHeavyAttack()
    {
    }
    private void performQuickAttack()
    {
             curAttack = AttackType.Quick;
                animator.SetBool("Attacking", true);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") ||
                   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
                {
                    source.PlayOneShot(swordSwipeSound, volume);
                    animator.SetBool("Chain", true);
                }          
    }

    private void performPowerMove()
    {

		if (temp < 0.2f) {
						temp = thisCharacter.turnCharToFaceOpponentNew ();
				}
		else {
						curAttack = AttackType.Power;
						Vector3 startPoint = transform.root.position;
						Vector3 endPoint = thisCharacter.getOpponentTransform ().position;
						Vector3 dir = (endPoint - startPoint);
						rb.velocity = dir;

				}
        
    }
}
