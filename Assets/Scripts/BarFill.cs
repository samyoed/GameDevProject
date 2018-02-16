using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFill : MonoBehaviour {

    public GameObject greyBar;
    public GameObject playerObject;
    public float energy;
    private float yPos;

    public Vector3 barPos;

    // Use this for initialization
    void Start () {
         yPos = gameObject.GetComponent<Transform>().position.y;
        barPos = greyBar.GetComponent<Transform>().InverseTransformPoint(0,yPos,0);


    }
	
	// Update is called once per frame
	void Update () {
        float yPosNew = 100- (barPos.y*100);
        energy = playerObject.GetComponent<BluePlayerBehavior>().energy;

        

        if(yPosNew > energy)
        {
            gameObject.GetComponent<Transform>().Translate(0, -.1f, 0);
        }
        else if(yPosNew < energy)
        {
            gameObject.GetComponent<Transform>().Translate(0, .1f, 0);
        }

       // Debug.Log("Energy: " + energy); //preliminary text
        
      //  Debug.Log(gameObject.GetComponent<Transform>().position.y);
    }
}
