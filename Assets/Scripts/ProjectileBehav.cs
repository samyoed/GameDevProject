using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehav : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim.Play("BlueArrow");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


   void onCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
