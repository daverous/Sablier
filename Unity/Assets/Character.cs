using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


    public int Health;
    public float Damage;
    public float TurnSpeed;
    public float Weight;
    public float jumpForce = 500f;
    private float moveSpeed = 15;
    private Vector3 moveDirection;
    private bool isGrounded = false;




    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rigidbody.AddForce(0, jumpForce,0);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "planet")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision info)
    {
        if (info.collider.name == "planet")
        {
            isGrounded = false;
        }
    }
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }
}