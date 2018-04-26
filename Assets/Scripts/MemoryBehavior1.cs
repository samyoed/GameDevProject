using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryBehavior1 : MonoBehaviour {

	public bool isActivated = false; // will activate the booleans in the player behavior


	// Use this for initialization
	void Start () {
		if (isActivated == true) {
			gameObject.SetActive (false);

		}

	}
	
	// Update is called once per frame
	void Update () {


		//transform.Rotate(Vector3.right * 1);


		transform.Rotate(Vector3.up * 1);
        
	}


	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") {

            StartCoroutine(PlaySound());
            
            isActivated = true;
            coll.gameObject.GetComponent<BluePlayerBehavior>().powerNum++;


            //gameObject.SetActive (false);



        }

	
	}

    IEnumerator PlaySound()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        gameObject.SetActive(false);

    }

}
