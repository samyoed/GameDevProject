using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehaviour : MonoBehaviour {

	public int health;
	public int knockback = 1000;
	public float raycastLength = 1000;
	public float speed = 50;
	public float lLimit = 0;
	public float rLimit = 0;
	float topLimit = 0;
	char targetDirection = 'r';
	Vector3 previousPosition = new Vector3 (0, 0, 0);
	int passOver = 0;
	int passOverLimit = 3;

	public GameObject player;

	public float attackGravity = 0;
	float liftDelay = 0;
	public float liftLimit = 0;

	int phase2Count = 0;
	int phase2Limit = 4;
	public float p2Duration = 0;
	float p2TimeCount = 0;

	private float hitboxDist;
	private bool hitboxRight;
	private bool isOnGround = false;
	public float offset = 50;

	bool phase2 = false;
	bool moving = true;
	bool attackingDown = false;
	bool attackingUp = false;
	bool p2Attacking = false;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
		topLimit = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y >= topLimit) {
			moving = true;
			if (attackingUp) {
				attackingUp = false;
				phase2Count++;
			}
		}
		if (p2Attacking) {
			moveLR ();
			attackingDown = true;
			p2TimeCount += Time.deltaTime;

			if (p2TimeCount >= p2Duration) {
				p2TimeCount = 0;
				p2Attacking = false;
				moving = false;
				attackingDown = false;
				attackingUp = true;
			}
		}

		if (attackingUp) {
			attackUp ();
			attackingDown = false;
		}
		if (attackingDown) {
			attackDown ();
			moving = false;
		}
		if (moving) {
			moveLR ();
		}

		previousPosition = this.transform.position;
		this.transform.rotation = new Quaternion (0, 0, 0, 0);
	}

	void moveLR ()	{
		if (targetDirection == 'r') {
			speed = Vector3.Distance(new Vector3(rLimit, 0, 0), this.transform.position)/25 - 4;
			this.transform.position += new Vector3 (speed, 0, 0);

			if (previousPosition.x <= player.transform.position.x && this.transform.position.x >= player.transform.position.x) {
				passOver++;
			}
			if (this.transform.position.x >= rLimit) {
				targetDirection = 'l';
			}
		}
		if (targetDirection == 'l') {
			speed = Vector3.Distance(new Vector3(lLimit, 0, 0), this.transform.position)/25 - 4;
			this.transform.position -= new Vector3 (speed, 0, 0);

			if (previousPosition.x >= player.transform.position.x && this.transform.position.x <= player.transform.position.x) {
				passOver++;
			}
			if (this.transform.position.x <= lLimit) {
				targetDirection = 'r';
			}
		}
		if (passOver >= passOverLimit) {
			moving = false;
			passOver = 0;
			Debug.Log ("attack");
			if (phase2Count >= phase2Limit) {
				phase2Count = 0;
				p2Attacking = true;
			} 
			  else {
				attackingDown = true;
			  }
			}
	}

	void attackDown ()	{
		this.GetComponent<Rigidbody2D> ().gravityScale = attackGravity;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2(0, this.GetComponent<Rigidbody2D> ().velocity.y);
		liftDelay += Time.deltaTime;

		if (liftDelay >= liftLimit) {
			liftDelay = 0;
			attackingUp = true;
			attackingDown = false;
		}
	}

	void attackUp ()	{
		this.transform.position += new Vector3 (0, 1, 0);
		this.GetComponent<Rigidbody2D> ().gravityScale = 0;
	}

	void p2Attack ()	{
		
	}
}
