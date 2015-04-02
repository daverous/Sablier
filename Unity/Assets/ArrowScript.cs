using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

    private int rotateSpeed = 10;
    Character thisChar;
	// Use this for initialization
	void Start () {
        thisChar = this.transform.root.gameObject.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion targetRotation = Quaternion.LookRotation(thisChar.getOpponentTransform().position - transform.root.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
	}
}
