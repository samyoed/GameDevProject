using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossdoor1behav : MonoBehaviour {

    public GameObject player;
    public float liftSpeed = 2;
    private Vector2 origPos;
    public float maxHeight = 500;
	// Use this for initialization
	void Start () {
        origPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {


        if ((transform.position.y - origPos.y) < maxHeight)
        {

            if (player.GetComponent<BluePlayerBehavior>().bossDoor1 == true)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + liftSpeed);

            }
        }

	}
}
