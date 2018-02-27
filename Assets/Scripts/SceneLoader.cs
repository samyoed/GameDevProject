using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {


    public GameObject blue;
    public GameObject red;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (blue.GetComponent<BluePlayerBehavior>().isDead == true)
        {
            SceneManager.LoadScene("End Game");
        }
        
    }

   public void goBack()
    {
        SceneManager.LoadScene("Main Scene");

    }
}