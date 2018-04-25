using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladderBehavior : MonoBehaviour {


    public float speed = 6;
    public float upSpeed = 1;
    public bool isOnLadder = false;
    public GameObject player;

    public bool exitLadder = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

		Debug.Log ("isOnLadder = " + isOnLadder);
		if (isOnLadder) {
			// if (Input.GetButtonDown("Vertical"))

			player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, Input.GetAxis ("Vertical") * speed);
           

			//            else
			//                coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, upSpeed);


			Debug.Log ("atladder");

			player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, upSpeed));
			player.GetComponent<BluePlayerBehavior> ().isOnLadder = true;
			//player.GetComponent<Animator> ().SetInteger ("State", 14);

	} else
			


		Debug.Log (isOnLadder);
    }


    void OnTriggerStay2D(Collider2D coll)
    {
		
		if (coll.tag == "Player" && Input.GetButtonDown ("Vertical")) {
			player.GetComponent<Animator> ().enabled = true;
			isOnLadder = true;
			player = coll.gameObject;
		} else if (coll.tag == "Player" && Input.GetButtonUp ("Vertical")) {
			
		
			//player.GetComponent<Animator> ().enabled = false;
		}


//            if (isOnLadder)
//        {
//           // if (Input.GetButtonDown("Vertical"))
            
//				coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Input.GetAxis("Vertical") * speed  );
//			coll.GetComponent<Rigidbody2D>().AddForce( new Vector2 (0, upSpeed));
            
////            else
////                coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, upSpeed);


//            Debug.Log("atladder");

//            //if (!Input.GetButtonDown("Vertical"))
//            //{
//            //    coll.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
//            //}




//        }
       
    if (coll.tag == "Player" && Input.GetButtonDown("Jump"))
        {
            isOnLadder = false;
			player.GetComponent<BluePlayerBehavior> ().isOnLadder = false;

        }



    }

    void OnTriggerExit2D(Collider2D coll)
    {
        isOnLadder = false;
		player.GetComponent<BluePlayerBehavior> ().isOnLadder = false;

		//player.GetComponent<Animator> ().SetInteger ("State", 7);
        
    }

}
