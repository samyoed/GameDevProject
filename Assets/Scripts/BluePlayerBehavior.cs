using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerBehavior : MonoBehaviour {

    public int speed = 10;
    public int jump = 10;
    public int dash = 100;
    public int finaldash = 150;
    public float moveX;
    public bool isFacingRight;
	public bool isBlue = true;
	private int jumpCount = 0;
    private bool isOnGround = true;
    private bool isAttacking;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			isBlue = !isBlue;
		}

        if(moveX > 0.0f && !isFacingRight && isBlue) //if not facing right and moving to the right
        {
            FlipPlayerDirection();   //flip
        }

		else if(moveX < 0.0f && isFacingRight && isBlue) //if facing right and moving to left
        {
            FlipPlayerDirection();  //flip
        }
        if (moveX > 0.1 || moveX < -0.1)
        {
            if(isOnGround)
            anim.SetInteger("State", 1); //if moving then go to move animation
        }
        else
        {
            if(isOnGround)
            anim.SetInteger("State", 0);
        }

		if(isBlue){
        moveX = Input.GetAxis("Horizontal");  //if input is on horizontal axis
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y); //move
		}
		if (Input.GetButtonDown("Jump") && isBlue && jumpCount < 2)
		//if (Input.GetButtonDown("Jump"))
        {
            Jump();
            isOnGround = false;
			jumpCount++;
        }

        if (Input.GetButton("Fire1"))
        {
            isAttacking = true;
            Melee();
            Debug.Log("working");
        }


	}

    void Dash()
    {
        if(isFacingRight)
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * dash, ForceMode2D.Impulse);
        else
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * dash, ForceMode2D.Impulse);

    }
    void FinalDash()
    {
        if (isFacingRight)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * finaldash, ForceMode2D.Impulse);
        else
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * finaldash, ForceMode2D.Impulse);

    }

    void Melee()
    {
        
        anim.SetInteger("State", 2);
        Debug.Log("working");
    }

    void FlipPlayerDirection()
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


	void OnCollisionEnter2D(Collision2D coll)
    {
		//Debug.Log ("working");
        isOnGround = true;
		jumpCount = 0;
        anim.SetInteger("State", 5);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up*jump);
        anim.SetInteger("State", 5);
    }
}
