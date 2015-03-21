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
    public float powerMoveSpeed = 1f;
	public float reducedAttackDamage = 2f;
    public float volume = 5f;

    private AttackType curAttack;
    private Animator animator;
	private Animator opponent_animator;
	private int collision_trigger = 0;
    // Use this for initialization

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
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCharacterTag == "Player")
        #region player1
        {
            if (Input.GetAxis("HeavyAttack1") == 1 && Input.GetAxis("QuickAttack1") == 1)
            {
                Debug.Log("Heavy Attacking");
                performHeavyAttack();
            }
            if (Input.GetAxis("QuickAttack1") == 1)
            {
                performQuickAttack();
            }
           
            else if (Input.GetAxis("QuickAttack1") == 0)
            {
                //curAttack = AttackType.Empty;
                
                animator.SetBool("Chain", false);
            }




            

            if (Input.GetAxis("PowerMove1") == 1)
            {
				if (thisCharacter.CharPowerBar > 0){
					thisCharacter.CharPowerBar = thisCharacter.CharPowerBar - Time.deltaTime/3;
					performPowerMove();
				}
            }
        }
        #endregion
        if (thisCharacterTag == "Player2")
        {

            #region player2
            if (Input.GetAxis("HeavyAttack2") == 1 && Input.GetAxis("QuickAttack2") == 1)
            {
                Debug.Log("Heavy Attacking");
                performHeavyAttack();
            }

            if (Input.GetAxis("QuickAttack2") == 1)
            {
                performQuickAttack();
            }

            if (Input.GetAxis("QuickAttack2") == 0)
            {
                //curAttack = AttackType.Empty;
                animator.SetBool("Chain", false);
            }

        if (Input.GetAxis("PowerMove2") == 1)
        {
			if (thisCharacter.CharPowerBar > 0){
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
					Debug.Log("quickAttackDamage"+quickAttackDamage);
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
        thisCharacter.turnCharToFaceOpponent();
        curAttack = AttackType.Power;
        Vector3 startPoint = transform.root.position;
        Vector3 endPoint = thisCharacter.getOpponentTransform().position;
        Vector3 dir = endPoint - startPoint;
        Rigidbody rb = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Rigidbody>();
        rb.velocity = dir;
   
        
    }
}
