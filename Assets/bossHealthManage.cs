using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealthManage : MonoBehaviour {

	public GameObject boss;
	public float healthPercentage;
	float maxHP = 0;
	float currentHP;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		maxHP = boss.GetComponent<bossBehaviour> ().maxHealth;
		currentHP = boss.GetComponent<bossBehaviour> ().health;
		healthPercentage = currentHP / maxHP;

		this.GetComponent<Image> ().fillAmount = healthPercentage;
	}
}
