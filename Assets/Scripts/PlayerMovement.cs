using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;


    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isjumping = false;
    private bool isgrounded;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        Animate();
    }

    private void FixedUpdate(){
        isgrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);


        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * movespeed, rb.velocity.y);
        if(isjumping){
            rb.AddForce(new Vector2(0f, jumpforce));
        }
        isjumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && isgrounded){
            isjumping = true;
        }
    }

    private void FlipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
