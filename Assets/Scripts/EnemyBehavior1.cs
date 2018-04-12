using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior1 : MonoBehaviour {

    public int health;
    public int knockback = 1000;
    public float raycastLength = 1000;
    public float speed = 50;

    public GameObject player;

    private float hitboxDist;
    private bool hitboxRight;
    private bool isOnGround = false;
    public float offset = 50;



	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (health <= 0)
        {
            this.gameObject.SetActive(false); // if health is 0 then destroy creature
            player.GetComponent<BluePlayerBehavior>().enemiesKilled++;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0); // locks rotation

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2 (transform.position.x - offset, transform.position.y), Vector2.left, raycastLength); // for seeing if the player is in range
        Debug.DrawRay(new Vector2 (transform.position.x - offset, transform.position.y), new Vector2(-raycastLength, 0), Color.green);



        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + offset, transform.position.y), Vector2.right, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + offset, transform.position.y), new Vector2(raycastLength, 0), Color.red);

        //if (hitLeft.collider != null)
        //Debug.Log("isInRange");
        //if (hitRight.collider != null)
        //    Debug.Log("isInRange");



        //if (hitLeft.collider.gameObject.tag == "Player" || hitRight.collider.gameObject.tag == "Player") // if in range
        //{
        if (hitLeft.collider.gameObject.tag == "Player")
            { 

            Debug.Log("leftisinrange" + hitLeft.collider.tag);
           float relPos = player.transform.position.x - transform.position.x;
            if (relPos > 0)
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2 (speed, 0), ForceMode2D.Impulse );
            if (relPos < 0)
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2 (-speed, 0), ForceMode2D.Impulse);

        }
        if (hitRight.collider.gameObject.tag == "Player")
        {

            Debug.Log("rightisinrange"  + hitRight.collider.tag);
            float relPos = player.transform.position.x - transform.position.x;
            if (relPos > 0)
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
            if (relPos < 0)
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);

        }


    }



	void FixedUpdate(){
		
    }





    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Attack Hitbox")
        {
            hitboxDist = this.transform.position.x - coll.transform.position.x;
            int damage = coll.gameObject.GetComponent<AttackHitbox>().damage;
            health = health - damage;
            StartCoroutine(Injured());
        }
    }


    IEnumerator Injured()
    {
       

        if (hitboxDist > 0)
        {
            hitboxRight = true;
        }
        else
        {
            hitboxRight = false;
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (!isOnGround && !hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
        }
        if (isOnGround && !hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockback, ForceMode2D.Impulse);
            Debug.Log("injured");
        }
        if (!isOnGround && hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
        }
        if (isOnGround && hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
            Debug.Log("injured");
        }
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.3f);
        this.GetComponent<SpriteRenderer>().color = Color.white;

    }



    }
