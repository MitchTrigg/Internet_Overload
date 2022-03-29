using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed;
    public float rayDist;
    private bool movingRight;
    public Transform groundCheckPos;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckPos.position, Vector2.down, rayDist);

        if(groundCheck.collider == false){
            if(movingRight){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        
    }

}
