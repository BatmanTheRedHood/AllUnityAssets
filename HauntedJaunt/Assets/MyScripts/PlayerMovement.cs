using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    private Animator animator;
    private Quaternion rotation;
    private Rigidbody rigidBody;

    public float turnSpeed;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        this.rotation = Quaternion.identity;
        this.turnSpeed = 20f;
        this.rigidBody = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        this.movement.Set(horizontalMovement, 0f, verticalMovement);
        this.movement.Normalize();

        bool hasHorizontalMovement = !Mathf.Approximately(horizontalMovement, 0f);
        bool hasVerticalalMovement = !Mathf.Approximately(verticalMovement, 0f);

        bool isWalking = hasHorizontalMovement || hasVerticalalMovement;

        // Sorry to be that late, hope you fixed it. If anyone else has this error, you probably copy/pasted this line : m_Animator.SetBool("IsWalking", isWalking);
        // However, you're previously asked to call that variable in the animator "isWalking", with no upper i. Fix it in your code : m_Animator.SetBool("isWalking", isWalking);
        this.animator.SetBool("isWalking", isWalking);

        // transform.forward is a shortcut to access the Transform component and get its forward vector. 
        Vector3 desiredRotation = Vector3.RotateTowards(transform.forward, this.movement, turnSpeed * Time.deltaTime, 0f);

        this.rotation = Quaternion.LookRotation(desiredRotation);
    }

    void OnAnimatorMove()
    {
        // The Animator’s deltaPosition is the change in position due to root motion that would have been applied to this frame.  
        // You’re taking the magnitude of that (the length of it) and multiplying by the movement vector 
        // which is in the actual direction we want the character to move.  
        // Bug Fix: Time.delta time required to see motion with time
        this.rigidBody.MovePosition(this.rigidBody.position + this.movement * this.animator.deltaPosition.normalized.magnitude * this.speed * Time.deltaTime);
        this.rigidBody.MoveRotation(this.rotation);
    }
}
