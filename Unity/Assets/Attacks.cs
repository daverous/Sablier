using UnityEngine;
using System.Collections;

public class Attacks : MonoBehaviour
{

    private Character thisCharacter;
    private string thisCharacterTag;


    public float quickAttackDamage = 5f;
    public float heavyAttackDamage = 10f;

    private AttackType curAttack;
    private Animator animator;
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
        thisCharacter = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Character>();
        animator = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (thisCharacterTag == "Player")
        #region player1
        {
            if (Input.GetAxis("QuickAttack1") == 1)
            {
                thisCharacter.incrementHits();
                curAttack = AttackType.Quick;
                animator.SetBool("Attacking", true);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide") ||
                   animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder"))
                    animator.SetBool("Chain", true);
            }
            if (Input.GetAxis("QuickAttack1") == 0)
            {
                curAttack = AttackType.Empty;
                animator.SetBool("Chain", false);
            }


        }

        if (Input.GetAxis("HeavyAttack1") == 1)
        {
            thisCharacter.incrementHits();
        }

        if (Input.GetAxis("PowerMove1") == 1)
        {
            //TODO implement
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
                curAttack = AttackType.Empty;
                animator.SetBool("Chain", false);
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
        //Debug.Log(other.transform.root.name);
        if (other.transform.root.name == thisCharacter.getOpponentName().ToString() && (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_FromSide")) ||
            (animator.GetCurrentAnimatorStateInfo(0).IsName("SkyBlade|Quick_OverShoulder")))
        {

            switch (curAttack)
            {
                case AttackType.Empty:
                    break;
                case AttackType.Heavy:
                    Debug.Log("heavy");
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(heavyAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                case AttackType.Power:
                    // TODO implement
                    break;
                case AttackType.Quick:
                    Debug.Log("quick attack");
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                default:
                    break;

            }
            curAttack = AttackType.Empty;
        }
    }

    void OnCollisionExit(Collision info)
    {

        if (info.collider.name == thisCharacter.getOpponentName().ToString())
        {
            //inRange = false;
        }
    }
}
