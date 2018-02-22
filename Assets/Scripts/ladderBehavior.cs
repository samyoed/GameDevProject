using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderBehavior : MonoBehaviour {


    public float speed = 6;

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
            coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxis("Vertical") * speed);
            Debug.Log("atladder");

        }
       
        else
        {

            coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);

        }



    }
}
