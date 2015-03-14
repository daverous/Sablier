﻿using UnityEngine;
using System.Collections;

public class Attacks : MonoBehaviour
{

    private Character thisCharacter;
    private string thisCharacterTag;
	private Character thisOpponent;


    public float quickAttackDamage = 5f;
    public float heavyAttackDamage = 10f;
    public float powerMoveSpeed = 1f;

    private AttackType curAttack;
    private Animator animator;
	private Animator opponent_animator;
	private int collision_trigger = 0;
    // Use this for initialization

    public enum AttackType
    {
        Quick, Heavy, Power, Empty
    }
    void Start()
    {
        curAttack = AttackType.Empty;
        //inRange = false;
        thisCharacterTag = transform.root.tag;
        //Debug.Log(transform.name);
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
            if (Input.GetAxis("QuickAttack1") == 1)
            {
				Debug.Log("Quick Attacking");
                curAttack = AttackType.Quick;
                animator.SetBool("Attacking", true);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") ||
                   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
                    animator.SetBool("Chain", true);
            }
            if (Input.GetAxis("QuickAttack1") == 0)
            {
                //curAttack = AttackType.Empty;
                animator.SetBool("Chain", false);
            }




            if (Input.GetAxis("HeavyAttack1") == 1)
            {
				Debug.Log("Heavy Attacking");
                //thisCharacter.incrementHits();
            }

            if (Input.GetAxis("PowerMove1") == 1)
            {
                curAttack = AttackType.Power;
                Vector3 startPoint = transform.root.position;
                Vector3 endPoint = thisCharacter.getOpponentTransform().position;
                //endPoint.x = endPoint.x - 1;
                Vector3 dir = endPoint - startPoint;
                Rigidbody rb = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Rigidbody>();
                //rb.MovePosition(endPoint *  2.5f * Time.time);
                rb.velocity = dir;
            }
        }
        #endregion
        if (thisCharacterTag == "Player2")
        {

            #region player2

            if (Input.GetAxis("QuickAttack2") == 1)
            {
                curAttack = AttackType.Quick;
                animator.SetBool("Attacking", true);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") ||
                   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
                    animator.SetBool("Chain", true);
            }
            if (Input.GetAxis("QuickAttack2") == 0)
            {
                //curAttack = AttackType.Empty;
                animator.SetBool("Chain", false);
            }

        if (Input.GetAxis("HeavyAttack2") == 1)
        {
            //thisCharacter.incrementHits();
        }

        if (Input.GetAxis("PowerMove2") == 1)
        {
            curAttack = AttackType.Power;
            float step = 0.5f * Time.deltaTime;
            Vector3 startPoint = transform.root.position;
            Vector3 endPoint = thisCharacter.getOpponentTransform().position;
            //endPoint.x = endPoint.x - 1;
            Vector3 dir = endPoint - startPoint;
            Rigidbody rb = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Rigidbody>();
            //rb.MovePosition(endPoint *  2.5f * Time.time);
            rb.velocity = dir;
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

		//if ((other.transform.root.name == "Player" || other.transform.root.name == "Player2") && (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide")) ||
          //  (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder")))
		if ((other.transform.root.name == "Player" || other.transform.root.name == "Player2") && (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") || animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder")) && collision_trigger == 0)
        {
            //Debug.Log(curAttack.ToString() + thisCharacter.getPNum().ToString());
			collision_trigger = 1;
            switch (curAttack)
            {
                case AttackType.Empty:
                    break;
                case AttackType.Heavy:
                 
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(heavyAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                case AttackType.Power:
                    break;
                    //GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
                    
                    break;
                case AttackType.Quick:
					Debug.Log("quickAttackDamage"+quickAttackDamage);
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                default:
                    break;

            }
            //curAttack = AttackType.Empty;
        }
    }

    void OnCollisionExit(Collision info)
    {
		Debug.Log("Exit!!! Yeah!");
		collision_trigger = 0;
        if (info.collider.name == thisCharacter.getOpponentName().ToString())
        {
            //inRange = false;
        }
    }
}
