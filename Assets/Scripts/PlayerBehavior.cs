using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public int speed = 10;
    public int jump = 10;
    public float moveX;
    public bool isFacingRight;
    private bool isOnGround = false;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if(moveX > 0.0f && !isFacingRight)
        {
            FlipPlayerDirection();
        }

        else if(moveX < 0.0f && isFacingRight)
        {
            FlipPlayerDirection();
        }
        if (moveX > 0.1 || moveX < -0.1)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 0);
        }


        moveX = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, 0);

       if (Input.GetButtonDown("Jump")&& isOnGround )
        {
            Jump();
            isOnGround = false;
        }
	}

    void FlipPlayerDirection()
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    void OnCollisionEnter()
    {
        isOnGround = true;
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*jump, ForceMode2D.Impulse);
    }
}
