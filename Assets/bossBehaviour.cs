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
	public float topLimit = 0;
	char targetDirection = 'r';
	Vector3 previousPosition = new Vector3 (0, 0, 0);
	int passOver = 0;
	int passOverLimit = 3;

	public GameObject player;

	public float attackGravity = 0;
	private float hitboxDist;
	private bool hitboxRight;
	private bool isOnGround = false;
	public float offset = 50;

	bool phase2 = false;
	bool moving = true;
	bool attackingDown = false;
	bool attackingUp = false;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody2D> ().gravityScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y >= topLimit) {
			moving = true;
		}


		if (attackingDown) {
			attack ();
			moving = false;
		}
		if (moving) {
			moveLR ();
		}



		previousPosition = this.transform.position;
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
			attackingDown = true;
			moving = false;
			passOver = 0;
			Debug.Log ("attack");
		}
	}

	void attack ()	{
		this.GetComponent<Rigidbody2D> ().gravityScale = attackGravity;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2(0, this.GetComponent<Rigidbody2D> ().velocity.y);
	}

	void p2Attack ()	{
	
	}
}
