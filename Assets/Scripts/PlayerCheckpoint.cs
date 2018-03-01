using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

    static public Vector2 checkpointPosition;
    

	void Awake(){

		//checkpointPosition = transform.position;

	}

	void Start(){

        if(checkpointPosition != new Vector2 ( 0,0))
		transform.position = checkpointPosition;
        Debug.Log("checkpoint Position" + checkpointPosition);
        

	}

    private void Update()
    {
        Debug.Log("checkpoint Position" + checkpointPosition);
        Debug.Log(Input.GetAxis("Vertical"));
    }

    private void OnTriggerStay2D(Collider2D coll)
    {


        if (coll.gameObject.tag == "Checkpoint" && Input.GetButtonDown("Vertical"))
        {
            
            checkpointPosition = coll.gameObject.transform.position;

            Debug.Log("checkpoint Position" + checkpointPosition);



        }
    }
}
