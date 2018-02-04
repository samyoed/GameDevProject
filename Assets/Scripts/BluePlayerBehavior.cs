using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePlayerBehavior : MonoBehaviour {

    public int speed = 10;
    public int jump = 10;
    public int dash = 100;
    public int finaldash = 150;
    public float moveX;
    public bool isFacingRight;
	public bool isBlue = true;
	private int jumpCount = 0;
    private bool isOnGround = true; //to make it so player isnt running in the air
    private bool isAttacking;
    float lockPos = 0;
    public int health = 3;
    public int knockback = 1000;
    public Text healthText;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public bool isHurt = false;
    private float enemyDist;
    private bool enemyRight;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        


        Debug.Log("Health: " + health); //preliminary text
        //if(!isOnGround)
        //    gameObject.GetComponent<Rigidbody2D>().drag = 0;


        switch (health) {

            case 1:
                {
                    heart1.SetActive(true);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    break;
                }
            case 2:
                {
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                    heart3.SetActive(false);
                    break;
                }
            case 3:
                {
                    heart1.SetActive(true);
                    heart2.SetActive(true);
                    heart3.SetActive(true);
                    break;
                }

        }


        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos); //locks rotation

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

		if(isBlue && !isHurt){
        moveX = Input.GetAxis("Horizontal");  //if input is on horizontal axis
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y); //move using velocity
            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveX * speed, 0), ForceMode2D.Impulse); //move using forces
		}

        if(!isOnGround)
            anim.SetInteger("State", 5);


        if (Input.GetButtonDown("Jump") && isBlue && jumpCount < 2) //if jump button is hit and player hasn't done more than 2 jumps
		//if (Input.GetButtonDown("Jump"))
        {
            Jump(); // call jump
            isOnGround = false; //not on the ground anymore
			jumpCount++; // add one to jump count
        }

        if (Input.GetButton("Fire1")) // if fire1 button is called
        {
            isAttacking = true;
            Melee(); // do melee attack
            
        }

        if (isHurt)
        {
            anim.SetInteger("State", 6);
        }
    }

    void Dash() //dash for first two melee attacks
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (isFacingRight)
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * dash, ForceMode2D.Impulse);
        else
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * dash, ForceMode2D.Impulse);

    }
    void FinalDash() //dash for 3rd melee attack
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (isFacingRight)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * finaldash, ForceMode2D.Impulse);
        else
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * finaldash, ForceMode2D.Impulse);

    }

    void Melee() // for melee attacks
    {
        
        anim.SetInteger("State", 2);
        
    }

    void FlipPlayerDirection() // switches player direction
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


	void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log ("working");
        if (coll.gameObject.tag == "Platform") // if 
        {
            isOnGround = true;
            jumpCount = 0;
           // anim.SetInteger("State", 5);
        }

        if (coll.gameObject.tag == "Enemy"){
            enemyDist = this.transform.position.x - coll.transform.position.x;
            StartCoroutine(Injured());
        }
    }

    void Jump()
    {
        
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*jump, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up*jump);
        anim.SetInteger("State", 5);
    }

    IEnumerator Injured()
    {
        isHurt = true;
        
        if (enemyDist > 0)
        {
            enemyRight = true;
        }
        else
        {
            enemyRight = false;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (!isOnGround && !enemyRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
            health--;
        }
        if(isOnGround && !enemyRight)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockback, ForceMode2D.Impulse);
            Debug.Log("injured");
            health--;
        }
        if (!isOnGround && enemyRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
            health--;
        }
        if (isOnGround && enemyRight)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
            Debug.Log("injured");
            health--;
        }









        yield return new WaitForSeconds(.3f);
        isHurt = false;
    }


}
