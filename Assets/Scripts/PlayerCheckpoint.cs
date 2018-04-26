using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{

    public static Vector2 checkpointPosition;
    public static bool hasRanged = false;
    public static bool hasDodge = false;
    public static bool hasTripleMelee = false;
    public static bool hasSpeedyRegen = false;
    public static int health = 3;
    public static int powerNum = 0;

    void Awake(){

		//checkpointPosition = transform.position;

	}

	void Start(){

        if(checkpointPosition != new Vector2 ( 0,0))
		transform.position = checkpointPosition;
        Debug.Log("checkpoint Position" + checkpointPosition);

        GetComponent<BluePlayerBehavior>().hasRanged = hasRanged;  // saves progress of the checkpoints
        GetComponent<BluePlayerBehavior>().hasDodge = hasDodge;
        GetComponent<BluePlayerBehavior>().hasTripleMelee = hasTripleMelee;
        GetComponent<BluePlayerBehavior>().hasSpeedyRegen = hasSpeedyRegen;




        GetComponent<BluePlayerBehavior>().health = health; // saves progress of health
        GetComponent<BluePlayerBehavior>().powerNum = powerNum;
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

            hasRanged = GetComponent<BluePlayerBehavior>().hasRanged;
            hasDodge = GetComponent<BluePlayerBehavior>().hasDodge;
            hasTripleMelee = GetComponent<BluePlayerBehavior>().hasTripleMelee;
            hasSpeedyRegen = GetComponent<BluePlayerBehavior>().hasSpeedyRegen;
            health = GetComponent<BluePlayerBehavior>().health;
            powerNum = GetComponent<BluePlayerBehavior>().powerNum;












        }
    }
}
