using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderBehavior : MonoBehaviour {


    public float speed = 6;
    public float upSpeed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
           // if (Input.GetButtonDown("Vertical"))
            
				coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxis("Vertical") * speed  );
			coll.GetComponent<Rigidbody2D>().AddForce( new Vector2 (0, upSpeed));
            
//            else
//                coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, upSpeed);


            Debug.Log("atladder");

        }
       
    



    }
}
