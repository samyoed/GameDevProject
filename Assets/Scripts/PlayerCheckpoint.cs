using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

    public Vector2 checkpointPosition;


	void Awake(){

		checkpointPosition = transform.position;

	}

	void Start(){
		transform.position = checkpointPosition;

	}
		
    private void OnTriggerEnter2D(Collider2D coll)
    {


        if (coll.gameObject.tag == "Checkpoint")
        {
            checkpointPosition = coll.gameObject.transform.position;
            




        }
    }
}
