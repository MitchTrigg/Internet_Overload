using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform playerSprite;
   
   void FixedUpdate(){
       transform.position = new Vector3(playerSprite.position.x + 5, playerSprite.position.y, transform.position.z);
   }
}
