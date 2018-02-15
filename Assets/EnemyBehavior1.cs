using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior1 : MonoBehaviour {

    public int health;
    public int knockback = 1000;
    private float hitboxDist;
    private bool hitboxRight;
    private bool isOnGround = false;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
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
            health--;
        }
        if (isOnGround && !hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockback, ForceMode2D.Impulse);
            Debug.Log("injured");
            health--;
        }
        if (!isOnGround && hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * knockback, ForceMode2D.Impulse);
            Debug.Log("air injured");
            health--;
        }
        if (isOnGround && hitboxRight)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockback, ForceMode2D.Impulse);
            Debug.Log("injured");
            health--;
        }
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.3f);
        this.GetComponent<SpriteRenderer>().color = Color.white;

    }



    }
