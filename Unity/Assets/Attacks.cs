using UnityEngine;
using System.Collections;

public class Attacks : MonoBehaviour
{

    private Character thisCharacter;
    private string thisCharacterTag;


    public float quickAttackDamage = 5f;
    public float heavyAttackDamage = 10f;

    private bool inRange;
    private AttackType curAttack;
    // Use this for initialization

    public enum AttackType
    {
        Quick, Heavy, Power, Empty
    }
    void Start()
    {
        curAttack = AttackType.Empty;
        inRange = false;
        thisCharacterTag = transform.parent.tag;
        thisCharacter = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {

        if (thisCharacterTag == "Player")
        #region player1
        {
            if (Input.GetAxis("QuickAttack1") == 1)
            {
                GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
                thisCharacter.incrementHits();
            }

            if (Input.GetAxis("HeavyAttack1") == 1)
            {
                GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(heavyAttackDamage);
                thisCharacter.incrementHits();
            }

            if (Input.GetAxis("PowerMove1") == 1)
            {
                //TODO implement
            }
        }
        #endregion
        else if (thisCharacterTag == "Player2")
        {

            #region player2

            if (Input.GetAxis("QuickAttack2") == 1)
            {

            }



            #endregion
        }
    }




    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == thisCharacter.getOpponentName().ToString())
        {
            switch(curAttack) {
                case AttackType.Empty:
                    break;
                case AttackType.Heavy:
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(heavyAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                case AttackType.Power:
                    // TODO implement
                    break;
                case AttackType.Quick:
                    GameObject.FindGameObjectWithTag(thisCharacter.getOpponentName().ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
                    thisCharacter.incrementHits();
                    break;
                default:
                    break;

        }
        curAttack = AttackType.Empty;
    }

    void OnCollisionExit(Collision info)
    {

        if (info.collider.name == thisCharacter.getOpponentName().ToString())
        {
            inRange = false;
        }
    }
}
