using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{

    private bool mustTurn;


    [HideInInspector]
    public bool mustPatrol;


    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public float walkSpeed;
    public Collider2D bodyCollider;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        
    }


    // Update is called once per frame
    void Update()
    {
        if(mustPatrol){
            Patrol();
        }
        
    }
/*
    private void FixedUpdate(){
        if(mustPatrol){
            mustTurn = Physics2D.OverlapCircle(groundCheckPos.position, .1f)
        }
    }
*/
    void Patrol(){
        if(bodyCollider.IsTouchingLayers(groundLayer)){
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed*Time.fixedDeltaTime, rb.velocity.y);
    }
    
    void Flip(){
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
