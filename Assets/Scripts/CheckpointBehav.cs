using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehav : MonoBehaviour {



	public GameObject gamesaveText;

    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



     void OnTriggerEnter2D(Collider2D coll)
    {
		Debug.Log ("collided");


        if(coll.gameObject.tag == "Player")
        {
			StartCoroutine (textShow());
			Debug.Log("checkpoint get");



        }
    }

	IEnumerator textShow()
	{
		gamesaveText.SetActive (true);
		yield return new WaitForSeconds (1f);
		gamesaveText.SetActive (false);




	}




}
