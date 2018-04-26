using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCameraBehav : MonoBehaviour {


    public float cameraMoveSpeed = 5;
    public Animator anim;
    public Image black;
    private bool canPress = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CanPress());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + cameraMoveSpeed, transform.position.y, transform.position.z);

        if (Input.anyKeyDown == true && canPress)
        {
            StartCoroutine(Fade());

        }
    }
    IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        Application.Quit();

    }

    IEnumerator CanPress()
    {

        yield return new WaitForSeconds(1);
        canPress = true;
    }
}




