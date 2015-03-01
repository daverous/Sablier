using UnityEngine;
using System.Collections;

public class Attacks : MonoBehaviour {
    
    private Character thisCharacter;
    private string thisCharacterTag;


    public float quickAttackDamage = 5;
    public float heavyAttackDamage = 10; 
	// Use this for initialization
	void Start () {
        thisCharacterTag = transform.parent.tag;
        thisCharacter = GameObject.FindGameObjectWithTag(thisCharacterTag).GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        if (thisCharacter.isInRange())
        {
            if (thisCharacterTag == "Player") 
        #region player1
            {
                if (Input.GetAxis("QuickAttack1") == 1) {
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
	}
}
