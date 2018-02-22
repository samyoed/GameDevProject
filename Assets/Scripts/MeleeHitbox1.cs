using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox1 : MonoBehaviour {

	public GameObject meleeHitboxPos;
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Transform>().position = meleeHitboxPos.gameObject.GetComponent<Transform>().position;
	}
}
