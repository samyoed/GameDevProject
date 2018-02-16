using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehav : MonoBehaviour {

    public Animator anim;
	private float startPos;

	private float currentPos;


	// Use this for initialization
	void Start () {
        anim.Play("BlueArrow");
		startPos = transform.position.x;

	}
	
	// Update is called once per frame
	void Update () {
		
		currentPos = transform.position.x;
		if (transform.localScale.x == 1) {
			if ((currentPos - startPos) > 1000)
				Destroy (gameObject);
		} 

//		if (transform.localScale.x == -1)
//		{
//
//			if ((currentPos - startPos) < 1000)
//				Destroy (gameObject);
//		 
//
//		}



	}


   void OnCollisionEnter2D(Collision2D coll)
    {
		Debug.Log ("shot");

        if (coll.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
