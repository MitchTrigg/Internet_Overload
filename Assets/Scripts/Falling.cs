using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
  Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
      Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name.Equals("Dino")){
          Debug.Log(coll.gameObject.name);
          Invoke ("DropPlatform", 1f);
          Destroy (gameObject, 1f);

        }
    }

    void DropPlatform()
    {
      rb.isKinematic = false;
    }
}
