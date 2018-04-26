using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {


    public GameObject blue;
    public GameObject boss;
	public Animator anim;
	public Image black;
    

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (blue.GetComponent<BluePlayerBehavior>().isDead == true)
        {
			StartCoroutine (FadeLose ());

        }
        if(boss.GetComponent<bossBehaviour>().health <= 0)
        {
            StartCoroutine(FadeWin());
        }
        
    }

   public void goBack()
    {
        SceneManager.LoadScene("Main Scene");

    }

	IEnumerator FadeLose()
	{
		anim.SetBool("Fade", true);
		yield return new WaitUntil(() => black.color.a == 1);
		SceneManager.LoadScene(2);

	}

	IEnumerator FadeWin()
	{
		anim.SetBool("Fade", true);
		yield return new WaitUntil(() => black.color.a == 1);
		SceneManager.LoadScene(3);

	}

} 
	
