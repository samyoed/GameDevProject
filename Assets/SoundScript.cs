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

    public AudioSource audio;

    void Start()
    {
         audio = GetComponent<AudioSource>();

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


}
