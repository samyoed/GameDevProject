using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehav : MonoBehaviour {



	

    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



     void OnTriggerStay2D(Collider2D coll)
    {
		Debug.Log ("collided");


        if(coll.gameObject.tag == "Player" && Input.GetButtonDown("Vertical"))
        {
			StartCoroutine (textShow());
			Debug.Log("checkpoint get");



        }
    }

	IEnumerator textShow()
	{
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds (1f);
        transform.GetChild(0).gameObject.SetActive(false);




    }




}
