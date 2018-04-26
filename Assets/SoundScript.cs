using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    public AudioClip hit12;
    public AudioClip hit3;
    public AudioClip hurt;
    public AudioClip steps;
    public AudioClip ranged;
    public AudioClip powerup;
	public AudioClip land;
	public AudioClip jump;
	public AudioClip respawn;
    public AudioClip dash;


	public AudioClip title;
	public AudioClip overworld;

    public AudioSource audio;
	public AudioSource bg;


    void Start()
    {
         audio = GetComponent<AudioSource>();
		bg.clip = overworld;
		bg.Play ();
		audio.clip = respawn;
		audio.Play ();

    }


    public void Hit12()
    {
        audio.clip = hit12;
        audio.Play();

    }
    public void Hit3()
    {
        audio.clip = hit3;
        audio.Play();

    }
    public void Hurt()
    {
        audio.clip = hurt;
        audio.Play();

    }
    public void Steps()
    {
        audio.clip = steps;
        audio.Play();

    }
    public void Ranged()
    {
        audio.clip = ranged;
        audio.Play();

    }
    public void Powerup()
    {
        audio.clip = powerup;
        audio.Play();

    }
	public void Land()
	{
		audio.clip = land;
		audio.Play();
	}
	public void Dasho()
    {
        audio.clip = dash;
        audio.Play();
    }


}
