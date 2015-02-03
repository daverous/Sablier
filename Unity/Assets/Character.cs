using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


    public int Health;
    public float Damage;
    public float TurnSpeed;
    public float Weight;
    private float moveSpeed = 15;
    private Vector3 moveDirection;


    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }
}