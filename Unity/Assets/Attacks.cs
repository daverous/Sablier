using UnityEngine;
using System.Collections;

public class Attacks : MonoBehaviour {
    
    private Character thisCharacter;
    private string thisCharacterTag;


    private float quickAttackDamage = 5;
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
                    GameObject.FindGameObjectWithTag(thisCharacter.opponentName.ToString()).GetComponent<Character>().beenHit(quickAttackDamage);
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
