using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleCameraBehav : MonoBehaviour {

    public float cameraMoveSpeed = 5;
    public Animator anim;
    public Image black;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x + cameraMoveSpeed, transform.position.y, transform.position.z);

        if(Input.anyKeyDown == true)
        {
            StartCoroutine(Fade());
            
        }
	}
IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(1);

    }


} 




