using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreathingAudio : MonoBehaviour
{
    //Private Variables
    private bool isDelayRunning = false;
    private int inhaleLength;
    private int exhaleLength;
    
    //Public Variables
    [Tooltip("Assign audio for breathing in.")]
    public AudioClip[] inhale;
    [Tooltip("Assign audio for breathing out.")]
    public AudioClip[] exhale;
    [Tooltip("Where to play the audio.")]
    public AudioSource audioPlayer;
    [Tooltip("Inward breath delay. Expected 8.")]
    public float delayInhale;
    [Tooltip("Outward breath delay. Expected 10.")]
    public float delayExhale;

    private void Start()
    {
        inhaleLength = inhale.Length;
        exhaleLength = exhale.Length;
    }

    private void Update()
    {
        if (isDelayRunning == false)
        {
            StartCoroutine(BreathDelay());
        }
    }

    IEnumerator BreathDelay()
    {
        isDelayRunning = true;
        audioPlayer.clip = inhale[Random.Range(0, inhaleLength)];
        audioPlayer.Play();
        yield return new WaitForSeconds(delayInhale);
        audioPlayer.clip = exhale[Random.Range(0, exhaleLength)];
        audioPlayer.Play();
        yield return new WaitForSeconds(delayExhale);
        isDelayRunning = false;
    }
}
