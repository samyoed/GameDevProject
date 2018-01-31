using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public int speed = 10;
    public float moveX;
    public bool isFacingRight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(moveX > 0.0f && !isFacingRight)
        {
            flipPlayerDirection();
        }

        else if(moveX < 0.0f && isFacingRight)
        {
            flipPlayerDirection();
        }

        moveX = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, 0);
	}

    void flipPlayerDirection()
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
