using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public int speed = 10;
    public float moveX;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveX = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, 0);
	}
}
