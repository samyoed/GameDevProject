using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour {

	public GameObject melee1Hitbox;
	public GameObject melee2Hitbox;
	public GameObject rangedHitbox;




	// Use this for initialization
	void Start () {

        melee1Hitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        melee2Hitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rangedHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;



    }
	
	// Update is called once per frame
	void Update () {



		




	}

IEnumerator DashHit()

{
		melee1Hitbox.gameObject.GetComponent<BoxCollider2D>().enabled = true;
	yield return new WaitForSeconds(0.3f);
		melee1Hitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
} 

IEnumerator FinalDashHit()

{
		melee2Hitbox.gameObject.GetComponent<BoxCollider2D>().enabled = true;
	yield return new WaitForSeconds(0.2f);
		melee2Hitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
}

IEnumerator RangedHit()

{
		rangedHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = true;
	yield return new WaitForSeconds(0.2f);
		rangedHitbox.gameObject.GetComponent<BoxCollider2D>().enabled = false;
}
}
