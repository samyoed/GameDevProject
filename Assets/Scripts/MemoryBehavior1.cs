using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryBehavior1 : MonoBehaviour {

	public bool isActivated = false; // will activate the booleans in the player behavior


	// Use this for initialization
	void Start () {
		if (isActivated == true) {
			gameObject.SetActive (false);

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") {

			isActivated = true; 

			gameObject.SetActive (false);

		}

	
	}

}
