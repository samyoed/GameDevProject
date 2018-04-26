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
    public bool isOnLadder = false;
    private int jumpCount = 0;
    private bool isOnGround = true; //to make it so player isnt running in the air
    private bool isAttacking;
    float lockPos = 0;

    public int health = 3;
    public float energy = 100;

    public int dodgeSpeed = 10000;
    public int knockback = 1000;
    public int powerNum = 0;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject face1;
    public GameObject face2;
    public GameObject arrow;

    public GameObject memory1;
    public GameObject memory2;
    public GameObject memory3;
    public GameObject memory4;
    public GameObject memory5;

    public bool isHurt = false;
    private float enemyDist;
    private bool enemyRight;
    private bool isInvincible = false;
    private bool isFalling = false;
    private bool hasDodged = false;
    public bool canMove = true;
    private bool canDash = true;

    public int[] combo;
    //private int comboIndex = 0;
    private float comboTimer;
    public float fireRate = 1;

    public bool hasRanged = false;
    public bool hasDodge = false;
    public bool hasTripleMelee = false;
    public bool hasSpeedyRegen = false;

    private float dashStartTime;
    private float dashEndTime;
    private bool isDashing = false;


    //    private float timeForMelee = 0;
    //    public float meleeEndTime = .2f;
    //    private bool canAttack = false;

    public bool isDead = false;
    public bool bossDoor1 = false;
    public bool bossFightStart = false;
    public bool bossHealthBar = false;

    public int enemiesKilled; // for health regen

    public GameObject bossHealthBarBar;
    public GameObject meleeHitbox;
    public Image energyBarFill;
    public Image lifeStealFill;
    public Image powerUpFill;


    public AudioSource audio;
    public AudioSource bg;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip boss;


    private Vector2 point;
    private Vector2 start; // This is defined to be some arbitrary point far away from the collider.
    private Vector2 goal;  // This is the point we want to determine whether or not is inside or outside the collider.
    private Vector2 direction;


    Animator anim;

    RaycastHit2D[] hits;


	//private static BluePlayerBehavior instance;




	// Use this for initialization

	void Awake(){


        //if (instance !=null && instance != this){
        //	Destroy (this.gameObject);
        //}
        //else
        //{
        //	instance = this;

        //}
        //DontDestroyOnLoad (this);

        //DontDestroyOnLoad(this);

        //if (FindObjectsOfType(GetType()).Length > 1)
        //{
        //    Destroy(gameObject);
        //}
    }

	void Start () {
        anim = GetComponent<Animator>();
       // combo = new int[] { 2, 3, 4 };
		//meleeHitbox.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
        face1.SetActive(true);
        face2.SetActive(false);
		canMove = false;

		StartCoroutine (Woke());
        enemiesKilled = 0;








    }
	
	// Update is called once per frame
	void Update () {


		if (!Input.GetButtonDown ("Fire1") && anim.GetInteger ("State") == 2 )
			anim.SetInteger ("State", 0);

		if (bossHealthBar == true){
			bossHealthBarBar.SetActive(true);

		}



        // changes fill color based on amount
        energyBarFill.fillAmount = energy / 100f;
        if(energy < 50)
        energyBarFill.GetComponent<Image>().color = new Color32(255, 90, 0, 255);
        else
            energyBarFill.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        if (energy < 25)
            energyBarFill.GetComponent<Image>().color = new Color32(255, 0, 0, 255);

        lifeStealFill.fillAmount = enemiesKilled / 4f;
        powerUpFill.fillAmount = powerNum / 4f;

        if (enemiesKilled == 4)
        {
            health++;
            enemiesKilled = 0;
        }


		if (health == 0) {
			isDead = true;

		}

        if (isOnGround && !(moveX > 0.1 || moveX < -0.1))
        anim.SetInteger ("State", 0);
        

        int energyint = Mathf.RoundToInt(energy);

       

        if (isHurt)
        {
            StartCoroutine(BlinkSprite());
            StartCoroutine(Invincible());
        }
		
        //if(!isOnGround)
        //    gameObject.GetComponent<Rigidbody2D>().drag = 0;


        



        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos); //locks rotation

		if (Input.GetButtonDown ("Switch") && energy > 30f && canMove == true) {
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


		if (isOnGround)
			
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

//		if (anim.GetInteger("State") == 0 && (moveX > 0.1 || moveX < -0.1))
//			{
//			anim.SetInteger("State", 1);
//			}

		if(isBlue && !isHurt && !isAttacking  && canMove == true){
        moveX = Input.GetAxis("Horizontal");  //if input is on horizontal axis
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y); //move using velocity
            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(moveX * speed, 0), ForceMode2D.Impulse); //move using forces
		}

        //if(!isOnGround)
          //  anim.SetInteger("State", 5);

		if (Input.GetButton ("Jump") && GetComponent<Rigidbody2D> ().velocity.y > 0) //
			GetComponent<Rigidbody2D> ().gravityScale = 35;
		else
			GetComponent<Rigidbody2D> ().gravityScale = 70;

		if (Input.GetButtonDown("Jump") && isBlue && jumpCount < 2  && canMove == true) //if jump button is hit and player hasn't done more than 2 jumps
		//if (Input.GetButtonDown("Jump"))
        {
			audio.clip = jumpSound;
			audio.Play ();
            Jump(); // call jump
            isOnGround = false; //not on the ground anymore
			jumpCount++; // add one to jump count
        }



        /*  if (Input.GetButton("Fire1")) // if fire1 button is called
          {
              isAttacking = true;
              Melee(); // do melee attack

          } */

        //		if( 0 < comboTimer && comboTimer< .3f && Input.GetButtonDown("Fire1") && energy > 20)
        //        {
        //            CoolCombo();
        //        }

        //else if (Input.GetButtonDown("Fire1") && comboIndex < combo.Length)



//        if (comboIndex > 2)
//            meleeEndTime = .9f;
//        else if (comboIndex != 2)
//            meleeEndTime = .3f;
//
//		timeForMelee += Time.deltaTime;
//		if (meleeEndTime < timeForMelee) 
//		{ 
//			canAttack = true;
//		} 
//		else
//		{
//			canAttack = false;
//		}



//		if (Input.GetButtonDown ("Fire1") && energy > 5 && hasTripleMelee && canAttack == true) {
//
//			
//				timeForMelee = 0;
//			anim.SetInteger ("State", combo[comboIndex]);
//			comboIndex++;
//			
//                
//
//			
//
//			if (comboIndex > 2)
//				comboIndex = 0;
////            comboTimer = 0;
////            anim.SetInteger("State", 2);
			
		if(Input.GetButtonDown ("Fire2") && energy > 15 && hasTripleMelee && isOnGround  && canMove == true)
			{
				CoolCombo();
			isOnGround = true;
		}




		if (Input.GetButtonDown ("Fire1") && energy > 15  && canMove == true) {
			
				anim.SetInteger ("State", 2);


		}



//        if(comboIndex > 0)
//        {
//            comboTimer += Time.deltaTime;
//            if(comboTimer > fireRate)
//            {
//                anim.SetInteger("State", 0);
//                comboIndex = 0;
//            }
//        }


		if(GetComponent<Rigidbody2D>().velocity.y < -1 && !isOnGround && !Input.GetButton("Fire1"))
            {
            anim.SetInteger("State", 7);
            isFalling = true;
        }
        
        if(GetComponent<Rigidbody2D>().velocity.y < -1 && anim.GetInteger("State") == 1)
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
			
            anim.SetInteger("State", 6); //change anim state to hurt
        }

        
/////////////////////////ENERGY REGEN////////////////////////////////////////////////////////////////////////////
		if (energy < 100 && !hasSpeedyRegen)
		{
			energy += .60f;
		}

		if (energy < 100 && hasSpeedyRegen) 
		{
			energy += 1.20f;
		}

		if(Input.GetButtonDown("Fire3") && hasRanged && energy > 15  && canMove == true) // if fire2 is pressed and ranged is enabled then ranged attack
        {
            
            StartCoroutine(RangedAttack());

			

			




        }





















        //if (isFacingRight)
        //{
        //    goal = new Vector2(transform.position.x + 120f, transform.position.y);
            
        //}
        //else
        //{
        //    goal = new Vector2(transform.position.x - 20f, transform.position.y);
            
        //}
        //start = new Vector2(0, 1000);


        //Debug.Log("goal = " + goal);


        //direction = goal - start; // This is the direction from start to goal.
        //direction.Normalize();
        //int iterations = 0; // If we know how many times the raycast has hit faces on its way to the target and back, we can tell through logic whether or not it is inside.
        //point = start;

        //while (point != goal) // Try to reach the point starting from the far off point.  This will pass through faces to reach its objective.
        //    {
        //    RaycastHit2D hit;
        //        if (Physics2D.Linecast(point, goal)) // Progressively move the point forward, stopping everytime we see a new plane in the way.
        //        {
        //        hit = Physics2D.Linecast(point,goal);
        //            iterations++;
        //            point = new Vector2(hit.point.x, hit.point.y) + (direction / 100.0f); // Move the point to hit.point and push it forward just a touch to move it through the skin of the mesh (if you don't push it, it will read that same point indefinately).
        //        }
        //        else
        //        {
        //            point = goal; // If there is no obstruction to our goal, then we can reach it in one step.
        //        }
        //    }
        //    while (point != start) // Try to return to where we came from, this will make sure we see all the back faces too.
        //    {
        //        RaycastHit2D hit;
        //        if (Physics2D.Linecast(point, start))
        //        {
        //        hit = Physics2D.Linecast(point, goal);
        //        iterations++;
        //            point = new Vector2(hit.point.x, hit.point.y) + (-direction / 100.0f);
        //        }
        //        else
        //        {
        //            point = start;
        //        }
        //    }
        //Debug.Log("iter = " + iterations);
        //if (iterations % 2 == 0)
        //    {
        //        Debug.Log("point is Outside");
        //    canDash = true;
            
        //}
        //    if (iterations % 2 == 1)
        //    {
        //    Debug.Log("point is Inside");
        //    canDash = false;
            
        //}
        


































		if(Input.GetButtonDown("Fire4") && hasDodge && !isOnGround && !hasDodged && energy > 15  && canMove == true && canDash == true) {
            energy -= 20;
			dashStartTime = Time.timeSinceLevelLoad + 0.2f;
			dashEndTime = Time.timeSinceLevelLoad + 0.3f;
			hasDodged = true;
			isDashing = true;
			anim.SetInteger("State", 11);
		}
        // isOnGround = false; // will be false until collision with ground



        if (memory1.GetComponent<MemoryBehavior1>().isActivated == true)
        {

        }

        if (memory2.GetComponent<MemoryBehavior1>().isActivated == true)
        {
            hasRanged = true;
            
        }

        if (hasRanged)
        {
            memory2.GetComponent<MemoryBehavior1>().isActivated = true;
        }

        if (memory3.GetComponent<MemoryBehavior1>().isActivated == true)
        {
            hasDodge = true;
        }

        if (hasDodge)
        {
            memory3.GetComponent<MemoryBehavior1>().isActivated = true;
        }

        if (memory4.GetComponent<MemoryBehavior1>().isActivated == true)
        {
            hasTripleMelee = true;
        }

        if (hasTripleMelee)
        {
            memory4.GetComponent<MemoryBehavior1>().isActivated = true;
        }

        if (memory5.GetComponent<MemoryBehavior1>().isActivated == true)
        {
            hasSpeedyRegen = true;
        }

        if (hasSpeedyRegen)
        {
            memory5.GetComponent<MemoryBehavior1>().isActivated = true;
        }

        if (!(this.GetComponent<Rigidbody2D>().velocity.y < 0.1 || this.GetComponent<Rigidbody2D>().velocity.y > -0.1))
        {
            isOnGround = false;
        }



        









        if(hasRanged && hasDodge && hasTripleMelee && hasSpeedyRegen && isOnGround)
        {

            bossDoor1 = true;

        }
		Debug.LogWarning ("isonladder = " + isOnLadder);
		if(isOnLadder){
			anim.SetInteger ("State", 14);


		}

        if(health == 0)
        {
            canMove = false;
            anim.SetInteger("State", 6);

        }

    }






    void FixedUpdate(){

       



        if (!isDashing) {


			return;

		}
		if (Time.timeSinceLevelLoad < dashStartTime) { // wait time  //smaller than dash time 
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0); 
			isInvincible = true;
			GetComponent<BoxCollider2D>().enabled =  false;
		} else if (Time.timeSinceLevelLoad < dashEndTime) {  //but if greater than dashEndTime

			if (isFacingRight)  //direction check
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (dodgeSpeed, 0);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-dodgeSpeed, 0);

		} else {
			GetComponent<BoxCollider2D>().enabled = true;
			isInvincible = false;
			isDashing = false;

		}


        
    }


   
   // ____________________________________________________________________________________________________________________________________________________________________________________________________________

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
        //if (coll.gameObject.tag == "Platform" && (this.GetComponent<Rigidbody2D>().velocity.y < 0.05 || this.GetComponent<Rigidbody2D>().velocity.y > -0.05)) // if 
        if (coll.gameObject.tag == "Platform" && coll.contacts[0].point.y < transform.position.y)
        {
            isOnGround = true;
            jumpCount = 0;
            hasDodged = false;
            // anim.SetInteger("State", 5);
        }
        

        if (coll.gameObject.tag == "Enemy" && !isInvincible){  // if its enemy and not invincible then take damage
            enemyDist = this.transform.position.x - coll.transform.position.x;
            StartCoroutine(Injured());
			audio.clip = hurtSound;
			audio.Play ();
        }
        else if(coll.gameObject.tag =="Enemy" && isInvincible)
        {
            StartCoroutine(IgnoreEnemy(coll));
        }
        if (coll.gameObject.tag == "Enemy" && isAttacking)
        {
            StartCoroutine(AttackInvicibility(coll));
            
        }

		if (coll.gameObject.tag == "Memory") { // if player collides with memory, gain power


		}




    }

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.name == "bossMusicCue"){

			bg.clip = boss;
			bg.Play ();
			bossFightStart = true;
			bossHealthBar = true;
		
			Destroy (coll.gameObject);
		}


	}

    void OnCollisionExit2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Platform")
        isOnGround = false;
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
        if (!enemyRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
            health--;
        }
        //if(isOnGround && !enemyRight)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockback, ForceMode2D.Impulse);
        //    Debug.Log("injured");
        //    health--;
        //}
        if (enemyRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
            health--;
        }
        //if (isOnGround && enemyRight)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
        //    Debug.Log("injured");
        //    health--;
        //}


        yield return new WaitForSeconds(.3f);
        
        isHurt = false;
        
    }

    IEnumerator BlinkSprite()
    {
        for (int k = 0; k < 10; k++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            face1.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            face2.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            face1.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            face2.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }


    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        face1.SetActive(false);
        face2.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        isInvincible = false;
        face1.SetActive(true);
        face2.SetActive(false);

    }

    IEnumerator IgnoreEnemy(Collision2D coll)
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>(), false);
    }

    IEnumerator AttackInvicibility (Collision2D coll)
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>(), true);
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), coll.gameObject.GetComponent<Collider2D>(), false);
    }


    IEnumerator Dash() //dash for first two melee attacks
    {
        isAttacking = true;
		energy -= 28.0f;
        if (isFacingRight)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(dash, 0);
        }
        if (!isFacingRight)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-dash, 0);
        }
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;

    }

    IEnumerator FinalDash() //dash for 3rd melee attack
    {
		isAttacking = true;


        if (isFacingRight)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(finaldash, 0);
        else
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-finaldash, 0);
        yield return new WaitForSeconds(0.2f);
		isAttacking = false;
    }


    //IEnumerator MeleeHitbox() //enables then disables melee hitbox when melee attack, attached to animations
    //{
    //    meleeHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    //    yield return new WaitForSeconds(0.3f);
    //    meleeHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;

    //}

        IEnumerator RangedAttack() //ranged attack then switches back to idle
    {
        if (isOnGround)
            anim.SetInteger("State", 9);
        else
            anim.SetInteger("State", 10);
        yield return new WaitForSeconds(0.3f);
        anim.SetInteger("State", 0);
    
    }

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(arrow, new Vector3(this.transform.position.x, this.transform.position.y - 1f , 1f), Quaternion.identity);

        if (isFacingRight)
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(1000f, 0);
        else if (!isFacingRight)
        {

            Vector2 scale = projectile.GetComponent<Transform>().localScale;
            scale.x *= -1;
            projectile.GetComponent<Transform>().localScale = scale;

            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-1000f, 0);
        }
    }


    IEnumerator Dodge()
    {
        isInvincible = true;
        
            anim.SetInteger("State", 11);
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.2f); // maybe change to waitforframes, is waitforframes more accurate??
		 //waitforframes doesnt exist? // ask why the distance changes every time this is called
		isInvincible = false;
        if (isFacingRight)
        GetComponent<Rigidbody2D>().velocity = new Vector2(dodgeSpeed, 0);
        else
        GetComponent<Rigidbody2D>().velocity = new Vector2(-dodgeSpeed, 0);

        yield return new WaitForSeconds(0.3f);
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
        GetComponent<BoxCollider2D>().enabled = true;

        
    }

	IEnumerator Woke()
	{
		
		GetComponent<Animator>().Play("Blue Respawn");
		yield return new WaitForSeconds (1.5f);
		canMove = true;

	}







    void CoolCombo()
    {
        anim.SetInteger("State", 3);
        
    }


		




}
