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
	public float energy = 100;

    public int knockback = 1000;
    public Text healthText;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject face1;
    public GameObject face2;

    public bool isHurt = false;
    private float enemyDist;
    private bool enemyRight;
    private bool isInvincible = false;
    private bool isFalling = false;

    public int[] combo;
    private int comboIndex = 0;
    private float comboTimer;
    public float fireRate = 1;


	public GameObject meleeHitbox;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //combo = new int[] { 2, 3, 4 };
		meleeHitbox.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
        face1.SetActive(true);
        face2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (isHurt)
        {
            StartCoroutine(BlinkSprite());
            StartCoroutine(Invincible());
        }
		
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

		if (Input.GetButtonDown ("Fire3") && energy > 30f) {
			energy = energy - 30;
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

        //if(!isOnGround)
          //  anim.SetInteger("State", 5);

		if (Input.GetButton ("Jump") && GetComponent<Rigidbody2D> ().velocity.y > 0) //
			GetComponent<Rigidbody2D> ().gravityScale = 25;
		else
			GetComponent<Rigidbody2D> ().gravityScale = 55;

        if (Input.GetButtonDown("Jump") && isBlue && jumpCount < 2) //if jump button is hit and player hasn't done more than 2 jumps
		//if (Input.GetButtonDown("Jump"))
        {
            Jump(); // call jump
            isOnGround = false; //not on the ground anymore
			jumpCount++; // add one to jump count
        }

      

        /*  if (Input.GetButton("Fire1")) // if fire1 button is called
          {
              isAttacking = true;
              Melee(); // do melee attack

          } */

		if( 0 < comboTimer && comboTimer< .3f && Input.GetButtonDown("Fire1") && energy > 20)
        {
            CoolCombo();
        }

        //else if (Input.GetButtonDown("Fire1") && comboIndex < combo.Length)
		else if(Input.GetButtonDown("Fire1") && energy > 5)
        {

            //anim.SetInteger("State", combo[comboIndex]);
            comboIndex++;

            comboTimer = 0;
            anim.SetInteger("State", 2);

        }

        if(comboIndex > 0)
        {
            comboTimer += Time.deltaTime;
            if(comboTimer > fireRate)
            {
                anim.SetInteger("State", 0);
                comboIndex = 0;
            }
        }


		if(GetComponent<Rigidbody2D>().velocity.y < 0 && !isOnGround && !Input.GetButton("Fire1"))
        {
            anim.SetInteger("State", 7);
            isFalling = true;
        }
        
        
        if (isFalling && isOnGround)
        {
            anim.SetInteger("State", 8);
            isFalling = false;
        }


        if (isHurt)
        {
            anim.SetInteger("State", 6);
        }

		if (energy < 100)
		{
			energy = energy + .1f;
		}

    }

   

    //void Melee() // for melee attacks
    //{
        
    //    anim.SetInteger("State", 2);
        
    //}

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

        if (coll.gameObject.tag == "Enemy" && !isInvincible){
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
        face1.SetActive(false);
        face2.SetActive(true);
        
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
        face1.SetActive(true);
        face2.SetActive(false);
        isHurt = false;
    }


    IEnumerator BlinkSprite()
    {
        for (int k = 0; k < 8; k++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.125f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.125f);
        }


    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(2f);
        isInvincible = false;

    }

    IEnumerator Dash() //dash for first two melee attacks
    {
		energy = energy - 5.0f;
        if(isFacingRight)
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dash, 0);
        else
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-dash, 0);
        yield return new WaitForSeconds(0.3f);

    }

    IEnumerator FinalDash() //dash for 3rd melee attack
    {
		energy = energy - 10.0f;
        if (isFacingRight)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(finaldash, 0);
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-finaldash, 0);
        yield return new WaitForSeconds(0.3f);
    }


	IEnumerator MeleeHitbox() //enables then disables melee hitbox when melee attack, attached to animations
	{
		meleeHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = true;
		yield return new WaitForSeconds (0.3f);
		meleeHitbox.gameObject.GetComponent<BoxCollider2D> ().enabled = false;

	}

  
    void CoolCombo()
    {
        anim.SetInteger("State", 3);
        
    }


		




}
