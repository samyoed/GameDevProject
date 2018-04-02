using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{



 


    void OnTriggerStay2D(Collider2D coll)
    {

        Debug.Log("collided");

        if (coll.gameObject.tag == "Player")
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        transform.GetChild(0).gameObject.SetActive(false);
    }
}


