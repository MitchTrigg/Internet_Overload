using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public int Respawn;
  void Start(){

  }
  
  void Update(){

  }

  void OnTriggerEnter2D(Collider2D other){
      if(other.CompareTag("Player")){
          SceneManager.LoadScene(Respawn);
      }
  }
}
