using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarSoundsController : MonoBehaviour
{
    [SerializeField]
    AudioSource accelerateSound; 
    [SerializeField]
    AudioSource standingSound; 
    bool accelerateSoundTriggered;
    bool standingSoundTriggered;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        standingSound.volume = 1f -accelerateSound.volume;
        if(standingSound.volume <= 0f )
        {
            standingSoundTriggered = false;
            standingSound.Stop();
        }
    }

    public void ReproduceMovingSound(float volume)
    {
        accelerateSound.volume = volume;
        if(accelerateSound.volume <= 0 )
        {
            accelerateSound.Stop();
            accelerateSoundTriggered=false;
        }
        if(!accelerateSoundTriggered)
        {
            accelerateSound.Play();
            accelerateSoundTriggered=true;
        }
    }

    public void ReproduceStandingSound()
    {
        if(!standingSoundTriggered)
        {
            standingSound.volume = standingSound.volume/2;
            standingSound.Play();
            standingSoundTriggered = true;
        }
    }

    internal void StopAllSounds()
    {
        standingSoundTriggered = false;
        standingSound.Stop();
        accelerateSound.Stop();
        accelerateSoundTriggered=false;
    }
}
