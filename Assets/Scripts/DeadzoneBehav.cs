﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadzoneBehav : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter2D (Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<BluePlayerBehavior>().isDead = true;

        }
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.SetActive(false);
        }

    }
}
