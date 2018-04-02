using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsScript : MonoBehaviour {

    public GameObject player;
    public int health;
	// Update is called once per frame
	void Update () {
        health = player.GetComponent<BluePlayerBehavior>().health;


        for (int k = 0; k < 10; k++)
        {
            if (health > k)
                transform.GetChild(k).gameObject.SetActive(true);
            else
                transform.GetChild(k).gameObject.SetActive(false);


        }

       
    }
}
